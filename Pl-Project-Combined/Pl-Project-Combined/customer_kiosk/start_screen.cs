using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace customer_kiosk
{
    public partial class start_screen : UserControl
    {
        public event EventHandler? StartClicked;
        private bool isAnimating = false;

        private ResponsiveScaler? responsiveScaler;
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

                await Task.Delay(25); // ~60 FPS
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
            responsiveScaler ??= new ResponsiveScaler(this, new System.Drawing.Size((int)BaseWidth, (int)BaseHeight));
            responsiveScaler.Apply(this.ClientSize);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ApplyResolutionScaling();
        }
    }
}
