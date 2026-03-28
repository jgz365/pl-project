namespace customer_kiosk
{
    public partial class main_menu : Form
    {
        // Web catalog host.
        private Microsoft.Web.WebView2.WinForms.WebView2? webCatalog;
        private redirect_application? redirectApplicationView;

        // One-time guards.
        private bool webCatalogInitialized;
        private bool resolutionScaled;

        // Baseline design resolution used by manual scaling.
        private const float BaseWidth = 1920f;
        private const float BaseHeight = 1080f;

        // Shared UI constants.
        private const int WebCatalogCornerRadius = 8;
        private const string ClockFormat = "dddd, MMMM dd, yyyy 'at' hh:mm tt";

        public main_menu()
        {
            InitializeComponent();
            ConfigureFormRenderingStyles();
        }

        #region Form lifecycle

        // Runs once when the form loads.
        private void main_menu_Load(object sender, EventArgs e)
        {
            ApplyResolutionScaling();
            UpdateClock();

            var startOverlay = AddStartOverlay();

            // Preload WebView2 behind the start overlay for a seamless reveal.
            _ = InitializeWebCatalogAsync(startOverlay);
        }

        // Runs every tick of the clock timer.
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateClock();
        }

        #endregion

        #region Startup overlay

        private start_screen AddStartOverlay()
        {
            var startOverlay = new start_screen
            {
                Dock = DockStyle.Fill
            };

            main_menu_gunapanel.Controls.Add(startOverlay);
            startOverlay.BringToFront();

            startOverlay.StartClicked += async (s, ev) =>
            {
                await startOverlay.SlideOutAsync();
                main_menu_gunapanel.Controls.Remove(startOverlay);
                startOverlay.Dispose();
            };

            return startOverlay;
        }

        #endregion

        #region General UI helpers

        private void ConfigureFormRenderingStyles()
        {
            // Reduce flicker by enabling double-buffered painting.
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        // Keeps the header clock text in one place.
        private void UpdateClock()
        {
            main_menu_gclock_label.Text = DateTime.Now.ToString(ClockFormat);
        }

        #endregion

        #region Resolution scaling

        private void ApplyResolutionScaling()
        {
            if (resolutionScaled) return;

            var screenBounds = Screen.FromControl(this).Bounds;
            float scaleX = screenBounds.Width / BaseWidth;
            float scaleY = screenBounds.Height / BaseHeight;

            if (Math.Abs(scaleX - 1f) < 0.01f && Math.Abs(scaleY - 1f) < 0.01f)
            {
                resolutionScaled = true;
                return;
            }

            SuspendLayout();
            ScaleControlsRecursive(main_menu_gunapanel, scaleX, scaleY);
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

        #endregion

        #region Web catalog setup

        private async Task InitializeWebCatalogAsync(start_screen? overlay = null)
        {
            if (webCatalogInitialized) return;

            webCatalog = CreateWebCatalogControl();
            webCatalog.Resize += (s, e) => ApplyWebCatalogRoundedCorners();

            doubleBufferedFlowLayoutPanel1.Visible = false;
            main_menu_gunapanel.Controls.Add(webCatalog);
            webCatalog.BringToFront();
            main_form_gSsidebar.BringToFront();
            main_menu_gunagradpanel.BringToFront();

            // Keep the start overlay on top while preloading happens in the background.
            overlay?.BringToFront();

            await webCatalog.EnsureCoreWebView2Async();
            ConfigureWebCatalogSettings();
            webCatalog.NavigateToString(BuildCatalogHtml());
            ApplyWebCatalogRoundedCorners();

            webCatalogInitialized = true;
        }

        private Microsoft.Web.WebView2.WinForms.WebView2 CreateWebCatalogControl()
        {
            return new Microsoft.Web.WebView2.WinForms.WebView2
            {
                DefaultBackgroundColor = Color.White,
                Name = "main_menu_webview_catalog",

                // Replace only the old product area (flow panel) while preserving header/sidebar.
                Location = doubleBufferedFlowLayoutPanel1.Location,
                Size = doubleBufferedFlowLayoutPanel1.Size,
                Anchor = doubleBufferedFlowLayoutPanel1.Anchor
            };
        }

        private void ConfigureWebCatalogSettings()
        {
            if (webCatalog?.CoreWebView2 == null) return;

            webCatalog.CoreWebView2.Settings.IsStatusBarEnabled = false;
            webCatalog.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
            webCatalog.CoreWebView2.WebMessageReceived -= WebCatalog_WebMessageReceived;
            webCatalog.CoreWebView2.WebMessageReceived += WebCatalog_WebMessageReceived;
        }

        private void WebCatalog_WebMessageReceived(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            if (!string.Equals(e.TryGetWebMessageAsString(), "open_redirect_application", StringComparison.Ordinal))
            {
                return;
            }

            ShowRedirectApplication();
        }

        private void ShowRedirectApplication()
        {
            if (redirectApplicationView == null || redirectApplicationView.IsDisposed)
            {
                redirectApplicationView = new redirect_application
                {
                    Dock = DockStyle.Fill
                };

                main_menu_gunapanel.Controls.Add(redirectApplicationView);
            }

            redirectApplicationView.BringToFront();
        }

        private void ApplyWebCatalogRoundedCorners()
        {
            if (webCatalog == null) return;
            if (webCatalog.Width <= 0 || webCatalog.Height <= 0) return;

            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.StartFigure();
            path.AddArc(0, 0, WebCatalogCornerRadius, WebCatalogCornerRadius, 180, 90);
            path.AddArc(webCatalog.Width - WebCatalogCornerRadius, 0, WebCatalogCornerRadius, WebCatalogCornerRadius, 270, 90);
            path.AddArc(webCatalog.Width - WebCatalogCornerRadius, webCatalog.Height - WebCatalogCornerRadius, WebCatalogCornerRadius, WebCatalogCornerRadius, 0, 90);
            path.AddArc(0, webCatalog.Height - WebCatalogCornerRadius, WebCatalogCornerRadius, WebCatalogCornerRadius, 90, 90);
            path.CloseFigure();

            var oldRegion = webCatalog.Region;
            webCatalog.Region = new Region(path);
            oldRegion?.Dispose();
            path.Dispose();
        }

        #endregion

        #region Embedded HTML catalog template

        private static string BuildCatalogHtml()
        {
            return CatalogHtmlProvider.GetHtml();
        }

        #endregion
    }
}
