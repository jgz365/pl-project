using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace customer_kiosk
{
    public partial class start_screen : UserControl
    {
        public event EventHandler? StartClicked;
        private bool isAnimating = false;

        private bool resolutionScaled;
        private const float BaseWidth = 1920f;
        private const float BaseHeight = 1080f;

        public start_screen()
        {
            InitializeComponent();

            // Enable double buffering to stop flickering
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint, true);
            this.UpdateStyles();

            BindClickEvents(this);
        }

        private void BindClickEvents(Control? parent)
        {
            if (parent == null)
            {
                return;
            }

            parent.Click += (s, e) =>
            {
                if (!isAnimating)
                {
                    isAnimating = true;
                    StartClicked?.Invoke(this, EventArgs.Empty);
                }
            };

            foreach (Control control in parent.Controls)
            {
                BindClickEvents(control);
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            BindClickEvents(e.Control);
        }

        public async Task SlideOutAsync()
        {
            if (this.Dock != DockStyle.None)
            {
                var rect = this.Bounds;
                this.Dock = DockStyle.None;
                this.Bounds = rect;
            }

            int step = 85; // speed of animation
            int targetTop = -this.Height;

            while (this.Top > targetTop)
            {
                this.Top -= step;
                if (this.Top < targetTop)
                    this.Top = targetTop;

                await Task.Delay(16); // ~60 FPS
            }

            this.Visible = false;
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
            System.Drawing.Rectangle screenBounds;
            if (this.ParentForm != null)
                screenBounds = Screen.FromControl(this.ParentForm).Bounds;
            else
                screenBounds = Screen.PrimaryScreen?.Bounds ?? new System.Drawing.Rectangle(0, 0, 1920, 1080);

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
                    control.Font = new System.Drawing.Font(control.Font.FontFamily, newSize, control.Font.Style);
                }

                if (control.HasChildren)
                {
                    ScaleControlsRecursive(control, scaleX, scaleY);
                }
            }
        }
    }
}
