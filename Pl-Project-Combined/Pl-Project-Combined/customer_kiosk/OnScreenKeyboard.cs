using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace customer_kiosk
{
    internal sealed class OnScreenKeyboard : Guna2Panel
    {
        private enum KeyboardMode
        {
            Full,
            Numeric
        }

        private readonly TableLayoutPanel layout;
        private readonly System.Windows.Forms.Timer backspaceHoldTimer;
        private readonly List<Guna2Button> characterKeys = new();
        private Guna2TextBox? targetTextBox;
        private Guna2Button? capsLockKey;
        private Guna2Button? backspaceKey;
        private bool isCapsLockEnabled;
        private bool isBackspacePressed;
        private int backspaceHoldTicks;
        private KeyboardMode currentMode = KeyboardMode.Full;

        private const int BackspaceInitialDelayTicks = 2;

        public event EventHandler? CloseRequested;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Guna2TextBox? TargetTextBox
        {
            get => targetTextBox;
            set
            {
                targetTextBox = value;

                var nextMode = ShouldUseNumericMode(value)
                    ? KeyboardMode.Numeric
                    : KeyboardMode.Full;

                if (nextMode == currentMode)
                {
                    return;
                }

                currentMode = nextMode;
                BuildKeys();
            }
        }

        public OnScreenKeyboard()
        {
            BorderRadius = 12;
            FillColor = Color.FromArgb(15, 23, 42);
            BorderColor = Color.FromArgb(30, 41, 59);
            BorderThickness = 1;
            Padding = new Padding(10);

            layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            Controls.Add(layout);

            backspaceHoldTimer = new System.Windows.Forms.Timer { Interval = 50 };
            backspaceHoldTimer.Tick += BackspaceHoldTimer_Tick;

            BuildKeys();
        }

        private void BuildKeys()
        {
            layout.SuspendLayout();
            layout.Controls.Clear();
            characterKeys.Clear();
            capsLockKey = null;
            backspaceKey = null;
            isCapsLockEnabled = false;

            if (currentMode == KeyboardMode.Numeric)
            {
                BuildNumericKeys();
            }
            else
            {
                BuildFullKeys();
            }

            layout.ResumeLayout();
            UpdateCharacterKeyLabels();
        }

        private void BuildFullKeys()
        {
            ConfigureGrid(11, 6);

            AddCharacterRow("!@#$%^&*()", 0);
            AddNumberRow(1);

            var backspaceTop = CreateBaseButton();
            backspaceTop.Text = "⌫";
            backspaceTop.MouseDown += BackspaceTop_MouseDown;
            backspaceTop.MouseUp += BackspaceTop_MouseUp;
            backspaceTop.MouseCaptureChanged += BackspaceTop_MouseCaptureChanged;
            backspaceKey = backspaceTop;
            layout.Controls.Add(backspaceTop, 10, 0);

            AddCharacterRow("QWERTYUIOP", 2);
            AddCharacterRow("ASDFGHJKL", 3, 1);
            AddCharacterRow("ZXCVBNM", 4, 1);

            var commaKey = CreateCharacterKey(",", ",", "<");
            layout.Controls.Add(commaKey, 8, 4);

            var periodKey = CreateCharacterKey(".", ".", ">");
            layout.Controls.Add(periodKey, 9, 4);

            var capsLock = CreateActionKey("Caps", ToggleCapsLock);
            layout.Controls.Add(capsLock, 0, 3);
            capsLockKey = capsLock;

            var clear = CreateActionKey("Clear", ClearText);
            layout.Controls.Add(clear, 10, 1);

            var space = CreateActionKey("Space", InsertSpace);
            layout.Controls.Add(space, 2, 5);
            layout.SetColumnSpan(space, 6);

            var done = CreateActionKey("Done", RequestClose);
            done.FillColor = Color.FromArgb(59, 130, 246);
            layout.Controls.Add(done, 10, 2);
        }

        private void BuildNumericKeys()
        {
            ConfigureGrid(4, 4);

            AddNumericKey("7", 0, 0);
            AddNumericKey("8", 1, 0);
            AddNumericKey("9", 2, 0);

            var backspace = CreateActionKey("⌫", Backspace);
            backspace.MouseDown += BackspaceTop_MouseDown;
            backspace.MouseUp += BackspaceTop_MouseUp;
            backspace.MouseCaptureChanged += BackspaceTop_MouseCaptureChanged;
            backspaceKey = backspace;
            layout.Controls.Add(backspace, 3, 0);

            AddNumericKey("4", 0, 1);
            AddNumericKey("5", 1, 1);
            AddNumericKey("6", 2, 1);

            var clear = CreateActionKey("Clear", ClearText);
            layout.Controls.Add(clear, 3, 1);

            AddNumericKey("1", 0, 2);
            AddNumericKey("2", 1, 2);
            AddNumericKey("3", 2, 2);

            var done = CreateActionKey("Done", RequestClose);
            done.FillColor = Color.FromArgb(59, 130, 246);
            layout.Controls.Add(done, 3, 2);

            var zero = CreateCharacterKey("0");
            layout.Controls.Add(zero, 0, 3);
            layout.SetColumnSpan(zero, 3);

            var close = CreateActionKey("Close", RequestClose);
            layout.Controls.Add(close, 3, 3);
        }

        private void ConfigureGrid(int columnCount, int rowCount)
        {
            layout.ColumnStyles.Clear();
            layout.RowStyles.Clear();
            layout.ColumnCount = columnCount;
            layout.RowCount = rowCount;

            for (int col = 0; col < columnCount; col++)
            {
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / columnCount));
            }

            for (int row = 0; row < rowCount; row++)
            {
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / rowCount));
            }
        }

        private void AddNumericKey(string text, int column, int row)
        {
            var key = CreateCharacterKey(text);
            layout.Controls.Add(key, column, row);
        }

        private static bool ShouldUseNumericMode(Guna2TextBox? textBox)
        {
            if (textBox == null)
            {
                return false;
            }

            if (textBox.Tag is string tag)
            {
                if (string.Equals(tag, "numeric", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                if (string.Equals(tag, "text", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            string name = textBox.Name?.ToLowerInvariant() ?? string.Empty;
            string[] numericHints =
            {
                "mobile", "phone", "pin", "otp", "number", "years",
                "income", "amount", "price", "percent", "qty", "quantity"
            };

            foreach (var hint in numericHints)
            {
                if (name.Contains(hint, StringComparison.Ordinal))
                {
                    return true;
                }
            }

            string sample = !string.IsNullOrWhiteSpace(textBox.PlaceholderText)
                ? textBox.PlaceholderText
                : textBox.Text;

            if (string.IsNullOrWhiteSpace(sample))
            {
                return false;
            }

            foreach (char c in sample)
            {
                if (char.IsDigit(c))
                {
                    continue;
                }

                if (" +-().,/%".Contains(c, StringComparison.Ordinal))
                {
                    continue;
                }

                return false;
            }

            return true;
        }

        private void AddNumberRow(int row)
        {
            for (int i = 0; i < 10; i++)
            {
                string number = (i + 1).ToString();
                if (i == 9)
                {
                    number = "0";
                }

                var key = CreateCharacterKey(number);
                layout.Controls.Add(key, i, row);
            }
        }

        private void AddCharacterRow(string characters, int row, int startColumn = 0)
        {
            for (int i = 0; i < characters.Length; i++)
            {
                string keyValue = characters[i].ToString();
                var key = CreateCharacterKey(keyValue);
                layout.Controls.Add(key, i + startColumn, row);
            }
        }

        private Guna2Button CreateCharacterKey(string text)
        {
            return CreateCharacterKey(text, text, text);
        }

        private Guna2Button CreateCharacterKey(string displayText, string defaultValue, string capsValue)
        {
            var button = CreateBaseButton();
            button.Text = displayText;
            button.Tag = Tuple.Create(defaultValue, capsValue);
            button.Click += CharacterKey_Click;
            characterKeys.Add(button);
            return button;
        }

        private Guna2Button CreateActionKey(string text, Action onClick)
        {
            var button = CreateBaseButton();
            button.Text = text;
            button.Click += (s, e) => onClick();
            return button;
        }

        private Guna2Button CreateBaseButton()
        {
            return new Guna2Button
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(2),
                BorderRadius = 8,
                FillColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                TabStop = false
            };
        }

        private void CharacterKey_Click(object? sender, EventArgs e)
        {
            if (sender is not Guna2Button button || button.Tag is not Tuple<string, string> keyData)
            {
                return;
            }

            if (TargetTextBox == null)
            {
                return;
            }

            string value = isCapsLockEnabled ? keyData.Item2 : keyData.Item1;

            if (value.Length == 1 && char.IsLetter(value[0]))
            {
                value = isCapsLockEnabled ? value.ToUpperInvariant() : value.ToLowerInvariant();
            }

            int start = TargetTextBox.SelectionStart;
            TargetTextBox.Text = TargetTextBox.Text.Insert(start, value);
            TargetTextBox.SelectionStart = start + value.Length;
            TargetTextBox.Focus();
        }

        private void ToggleCapsLock()
        {
            isCapsLockEnabled = !isCapsLockEnabled;
            UpdateCharacterKeyLabels();
        }

        private void UpdateCharacterKeyLabels()
        {
            foreach (var key in characterKeys)
            {
                if (key.Tag is not Tuple<string, string> keyData)
                {
                    continue;
                }

                if (keyData.Item1.Length == 1 && keyData.Item2.Length == 1 &&
                    char.IsDigit(keyData.Item1[0]) && !char.IsLetterOrDigit(keyData.Item2[0]))
                {
                    key.Text = $"{EscapeMnemonic(keyData.Item2)}\n{EscapeMnemonic(keyData.Item1)}";
                    continue;
                }

                string value = isCapsLockEnabled ? keyData.Item2 : keyData.Item1;
                char character = value[0];
                if (!char.IsLetter(character))
                {
                    key.Text = EscapeMnemonic(value);
                    continue;
                }

                key.Text = EscapeMnemonic(isCapsLockEnabled
                    ? value.ToUpperInvariant()
                    : value.ToLowerInvariant());
            }

            if (capsLockKey != null)
            {
                capsLockKey.FillColor = isCapsLockEnabled
                    ? Color.FromArgb(59, 130, 246)
                    : Color.FromArgb(30, 41, 59);
            }
            }

        private static string EscapeMnemonic(string value)
        {
            return value.Replace("&", "&&");
        }

        private void InsertSpace()
        {
            if (TargetTextBox == null)
            {
                return;
            }

            int start = TargetTextBox.SelectionStart;
            TargetTextBox.Text = TargetTextBox.Text.Insert(start, " ");
            TargetTextBox.SelectionStart = start + 1;
            TargetTextBox.Focus();
        }

        private void Backspace()
        {
            if (TargetTextBox == null || TargetTextBox.TextLength == 0)
            {
                return;
            }

            int start = TargetTextBox.SelectionStart;
            if (start <= 0)
            {
                return;
            }

            TargetTextBox.Text = TargetTextBox.Text.Remove(start - 1, 1);
            TargetTextBox.SelectionStart = start - 1;
            TargetTextBox.Focus();
        }

        private void ClearText()
        {
            if (TargetTextBox == null)
            {
                return;
            }

            TargetTextBox.Clear();
            TargetTextBox.Focus();
        }

        private void BackspaceTop_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            isBackspacePressed = true;
            if (backspaceKey != null)
            {
                backspaceKey.Capture = true;
            }

            Backspace();
            backspaceHoldTicks = 0;
            backspaceHoldTimer.Start();
        }

        private void BackspaceTop_MouseUp(object? sender, EventArgs e)
        {
            isBackspacePressed = false;
            if (backspaceKey != null)
            {
                backspaceKey.Capture = false;
            }

            backspaceHoldTimer.Stop();
            backspaceHoldTicks = 0;
        }

        private void BackspaceTop_MouseCaptureChanged(object? sender, EventArgs e)
        {
            if (Control.MouseButtons != MouseButtons.Left)
            {
                BackspaceTop_MouseUp(sender, e);
            }
        }

        private void BackspaceHoldTimer_Tick(object? sender, EventArgs e)
        {
            if (!isBackspacePressed)
            {
                return;
            }

            backspaceHoldTicks++;
            if (backspaceHoldTicks < BackspaceInitialDelayTicks)
            {
                return;
            }

            Backspace();
        }

        private void RequestClose()
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
