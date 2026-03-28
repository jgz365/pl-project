<<<<<<< HEAD
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
        private readonly TableLayoutPanel layout;
        private readonly System.Windows.Forms.Timer backspaceHoldTimer;
        private readonly List<Guna2Button> characterKeys = new();
        private Guna2Button? capsLockKey;
        private Guna2Button? backspaceKey;
        private bool isCapsLockEnabled;
        private bool isBackspacePressed;
        private int backspaceHoldTicks;

        private const int BackspaceInitialDelayTicks = 2;

        public event EventHandler? CloseRequested;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Guna2TextBox? TargetTextBox { get; set; }

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
                ColumnCount = 11,
                RowCount = 6,
                BackColor = Color.Transparent
            };

            for (int col = 0; col < 11; col++)
            {
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 11f));
            }

            for (int row = 0; row < 6; row++)
            {
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / 6f));
            }

            Controls.Add(layout);

            backspaceHoldTimer = new System.Windows.Forms.Timer { Interval = 50 };
            backspaceHoldTimer.Tick += BackspaceHoldTimer_Tick;

            BuildKeys();
        }

        private void BuildKeys()
        {
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
            layout.SetRowSpan(done, 4);

            UpdateCharacterKeyLabels();
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
                Margin = new Padding(4),
                BorderRadius = 8,
                FillColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
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
                    key.Text = $"{keyData.Item2}\n{keyData.Item1}";
                    continue;
                }

                string value = isCapsLockEnabled ? keyData.Item2 : keyData.Item1;
                char character = value[0];
                if (!char.IsLetter(character))
                {
                    key.Text = value;
                    continue;
                }

                key.Text = isCapsLockEnabled
                    ? value.ToUpperInvariant()
                    : value.ToLowerInvariant();
            }

            if (capsLockKey != null)
            {
                capsLockKey.FillColor = isCapsLockEnabled
                    ? Color.FromArgb(59, 130, 246)
                    : Color.FromArgb(30, 41, 59);
            }
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
=======
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
        private readonly TableLayoutPanel layout;
        private readonly System.Windows.Forms.Timer backspaceHoldTimer;
        private readonly List<Guna2Button> characterKeys = new();
        private Guna2Button? capsLockKey;
        private Guna2Button? backspaceKey;
        private bool isCapsLockEnabled;
        private bool isBackspacePressed;
        private int backspaceHoldTicks;

        private const int BackspaceInitialDelayTicks = 2;

        public event EventHandler? CloseRequested;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Guna2TextBox? TargetTextBox { get; set; }

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
                ColumnCount = 11,
                RowCount = 6,
                BackColor = Color.Transparent
            };

            for (int col = 0; col < 11; col++)
            {
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 11f));
            }

            for (int row = 0; row < 6; row++)
            {
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / 6f));
            }

            Controls.Add(layout);

            backspaceHoldTimer = new System.Windows.Forms.Timer { Interval = 50 };
            backspaceHoldTimer.Tick += BackspaceHoldTimer_Tick;

            BuildKeys();
        }

        private void BuildKeys()
        {
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
            layout.SetRowSpan(done, 4);

            UpdateCharacterKeyLabels();
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
                Margin = new Padding(4),
                BorderRadius = 8,
                FillColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
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
                    key.Text = $"{keyData.Item2}\n{keyData.Item1}";
                    continue;
                }

                string value = isCapsLockEnabled ? keyData.Item2 : keyData.Item1;
                char character = value[0];
                if (!char.IsLetter(character))
                {
                    key.Text = value;
                    continue;
                }

                key.Text = isCapsLockEnabled
                    ? value.ToUpperInvariant()
                    : value.ToLowerInvariant();
            }

            if (capsLockKey != null)
            {
                capsLockKey.FillColor = isCapsLockEnabled
                    ? Color.FromArgb(59, 130, 246)
                    : Color.FromArgb(30, 41, 59);
            }
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
>>>>>>> 8103f024413860c46c3315782101728e2c33bdf1
