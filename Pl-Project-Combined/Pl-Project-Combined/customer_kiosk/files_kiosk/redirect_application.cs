<<<<<<< HEAD
﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace customer_kiosk
{
    public partial class redirect_application : UserControl
    {
        private readonly OnScreenKeyboard onScreenKeyboard;

        private bool resolutionScaled;
        private const float BaseWidth = 1920f;
        private const float BaseHeight = 1080f;

        public redirect_application()
        {
            InitializeComponent();


            onScreenKeyboard = new OnScreenKeyboard
            {
                Size = new Size(1880, 410),
                Location = new Point(20, 650),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                Visible = false
            };

            onScreenKeyboard.CloseRequested += OnScreenKeyboard_CloseRequested;
            mainContainer.Controls.Add(onScreenKeyboard);

            RegisterTextBoxHandlers(this);
            btnBack.Click += BtnBack_Click;
        }

        private void RegisterTextBoxHandlers(Control root)
        {
            foreach (Control control in root.Controls)
            {
                if (control is Guna2TextBox textBox)
                {
                    textBox.Enter += TextBox_Enter;
                    textBox.Click += TextBox_Enter;
                }

                if (control.HasChildren)
                {
                    RegisterTextBoxHandlers(control);
                }
            }
        }

        private void TextBox_Enter(object? sender, EventArgs e)
        {
            if (sender is not Guna2TextBox textBox)
            {
                return;
            }

            onScreenKeyboard.TargetTextBox = textBox;
            onScreenKeyboard.Visible = true;
            onScreenKeyboard.BringToFront();
        }

        private void OnScreenKeyboard_CloseRequested(object? sender, EventArgs e)
        {
            onScreenKeyboard.Visible = false;
            onScreenKeyboard.TargetTextBox = null;
        }

        private void BtnBack_Click(object? sender, EventArgs e)
        {
            onScreenKeyboard.Visible = false;

            var host = Parent;
            if (host == null)
            {
                return;
            }

            host.Controls.Remove(this);
            Dispose();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyResolutionScaling();
        }

        private void ApplyResolutionScaling()
        {
            if (resolutionScaled) return;

            // When hosted inside a form, get screen from parent, or fallback to primary.
            Rectangle screenBounds;
            if (this.ParentForm != null)
                screenBounds = Screen.FromControl(this.ParentForm).Bounds;
            else
                screenBounds = Screen.PrimaryScreen?.Bounds ?? new Rectangle(0, 0, 1920, 1080);

            float scaleX = screenBounds.Width / BaseWidth;
            float scaleY = screenBounds.Height / BaseHeight;

            if (Math.Abs(scaleX - 1f) < 0.01f && Math.Abs(scaleY - 1f) < 0.01f)
            {
                resolutionScaled = true;
                return;
            }

            SuspendLayout();
            ScaleControlsRecursive(this, scaleX, scaleY);
            ResumeLayout(true);

            resolutionScaled = true;
        }

        private void ScaleControlsRecursive(Control parent, float scaleX, float scaleY)
        {
            foreach (Control control in parent.Controls)
            {
                if (control.Dock == DockStyle.None)
                {
                    control.Left = (int)Math.Round(control.Left * scaleX);
                    control.Top = (int)Math.Round(control.Top * scaleY);
                    control.Width = Math.Max(1, (int)Math.Round(control.Width * scaleX));
                    control.Height = Math.Max(1, (int)Math.Round(control.Height * scaleY));
                }

                var scaledMargin = new Padding(
                    (int)Math.Round(control.Margin.Left * scaleX),
                    (int)Math.Round(control.Margin.Top * scaleY),
                    (int)Math.Round(control.Margin.Right * scaleX),
                    (int)Math.Round(control.Margin.Bottom * scaleY));
                control.Margin = scaledMargin;

                var scaledPadding = new Padding(
                    (int)Math.Round(control.Padding.Left * scaleX),
                    (int)Math.Round(control.Padding.Top * scaleY),
                    (int)Math.Round(control.Padding.Right * scaleX),
                    (int)Math.Round(control.Padding.Bottom * scaleY));
                control.Padding = scaledPadding;

                if (control.Font != null)
                {
                    float fontScale = Math.Min(scaleX, scaleY);
                    float newSize = Math.Max(6f, control.Font.Size * fontScale);
                    control.Font = new Font(control.Font.FontFamily, newSize, control.Font.Style);
                }

                if (control.HasChildren)
                {
                    ScaleControlsRecursive(control, scaleX, scaleY);
                }
            }
        }
    }
}
=======
﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace customer_kiosk
{
    public partial class redirect_application : UserControl
    {
        private readonly OnScreenKeyboard onScreenKeyboard;

        private bool resolutionScaled;
        private const float BaseWidth = 1920f;
        private const float BaseHeight = 1080f;

        public redirect_application()
        {
            InitializeComponent();


            onScreenKeyboard = new OnScreenKeyboard
            {
                Size = new Size(1880, 410),
                Location = new Point(20, 650),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                Visible = false
            };

            onScreenKeyboard.CloseRequested += OnScreenKeyboard_CloseRequested;
            mainContainer.Controls.Add(onScreenKeyboard);

            RegisterTextBoxHandlers(this);
            btnBack.Click += BtnBack_Click;
        }

        private void RegisterTextBoxHandlers(Control root)
        {
            foreach (Control control in root.Controls)
            {
                if (control is Guna2TextBox textBox)
                {
                    textBox.Enter += TextBox_Enter;
                    textBox.Click += TextBox_Enter;
                }

                if (control.HasChildren)
                {
                    RegisterTextBoxHandlers(control);
                }
            }
        }

        private void TextBox_Enter(object? sender, EventArgs e)
        {
            if (sender is not Guna2TextBox textBox)
            {
                return;
            }

            onScreenKeyboard.TargetTextBox = textBox;
            onScreenKeyboard.Visible = true;
            onScreenKeyboard.BringToFront();
        }

        private void OnScreenKeyboard_CloseRequested(object? sender, EventArgs e)
        {
            onScreenKeyboard.Visible = false;
            onScreenKeyboard.TargetTextBox = null;
        }

        private void BtnBack_Click(object? sender, EventArgs e)
        {
            onScreenKeyboard.Visible = false;

            var host = Parent;
            if (host == null)
            {
                return;
            }

            host.Controls.Remove(this);
            Dispose();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyResolutionScaling();
        }

        private void ApplyResolutionScaling()
        {
            if (resolutionScaled) return;

            // When hosted inside a form, get screen from parent, or fallback to primary.
            Rectangle screenBounds;
            if (this.ParentForm != null)
                screenBounds = Screen.FromControl(this.ParentForm).Bounds;
            else
                screenBounds = Screen.PrimaryScreen?.Bounds ?? new Rectangle(0, 0, 1920, 1080);

            float scaleX = screenBounds.Width / BaseWidth;
            float scaleY = screenBounds.Height / BaseHeight;

            if (Math.Abs(scaleX - 1f) < 0.01f && Math.Abs(scaleY - 1f) < 0.01f)
            {
                resolutionScaled = true;
                return;
            }

            SuspendLayout();
            ScaleControlsRecursive(this, scaleX, scaleY);
            ResumeLayout(true);

            resolutionScaled = true;
        }

        private void ScaleControlsRecursive(Control parent, float scaleX, float scaleY)
        {
            foreach (Control control in parent.Controls)
            {
                if (control.Dock == DockStyle.None)
                {
                    control.Left = (int)Math.Round(control.Left * scaleX);
                    control.Top = (int)Math.Round(control.Top * scaleY);
                    control.Width = Math.Max(1, (int)Math.Round(control.Width * scaleX));
                    control.Height = Math.Max(1, (int)Math.Round(control.Height * scaleY));
                }

                var scaledMargin = new Padding(
                    (int)Math.Round(control.Margin.Left * scaleX),
                    (int)Math.Round(control.Margin.Top * scaleY),
                    (int)Math.Round(control.Margin.Right * scaleX),
                    (int)Math.Round(control.Margin.Bottom * scaleY));
                control.Margin = scaledMargin;

                var scaledPadding = new Padding(
                    (int)Math.Round(control.Padding.Left * scaleX),
                    (int)Math.Round(control.Padding.Top * scaleY),
                    (int)Math.Round(control.Padding.Right * scaleX),
                    (int)Math.Round(control.Padding.Bottom * scaleY));
                control.Padding = scaledPadding;

                if (control.Font != null)
                {
                    float fontScale = Math.Min(scaleX, scaleY);
                    float newSize = Math.Max(6f, control.Font.Size * fontScale);
                    control.Font = new Font(control.Font.FontFamily, newSize, control.Font.Style);
                }

                if (control.HasChildren)
                {
                    ScaleControlsRecursive(control, scaleX, scaleY);
                }
            }
        }
    }
}
>>>>>>> 8103f024413860c46c3315782101728e2c33bdf1
