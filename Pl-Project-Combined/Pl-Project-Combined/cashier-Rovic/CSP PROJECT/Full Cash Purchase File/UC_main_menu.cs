using System.Text.Json;

namespace customer_kiosk
{
    public partial class UC_main_menu : UserControl
    {
        // ── Navigation event — fires when the user wants to leave back to POSCashier ──
        public event EventHandler CloseRequested;

        // Web catalog host.
        private Microsoft.Web.WebView2.WinForms.WebView2? webCatalog;

        // One-time guard.
        private bool webCatalogInitialized;
        private ResponsiveScaler? responsiveScaler;

        // Baseline design resolution used by manual scaling.
        private const float BaseWidth = 1920f;
        private const float BaseHeight = 1080f;

        // Shared UI constants.
        private const int WebCatalogCornerRadius = 8;
        private const string ClockFormat = "dddd, MMMM dd, yyyy 'at' hh:mm tt";

        private sealed class SelectedProductPayload
        {
            public string Title { get; init; } = string.Empty;
            public string Sub { get; init; } = string.Empty;
            public string Price { get; init; } = string.Empty;
            public string ImageUrl { get; init; } = string.Empty;
        }

        public UC_main_menu()
        {
            InitializeComponent();
            ConfigureRenderingStyles();
            Resize += UC_main_menu_ResizeForResponsiveScaling;
        }

        // ─────────────────────────────────────────────────────────────────────
        // Lifecycle
        // ─────────────────────────────────────────────────────────────────────
        private void UC_main_menu_Load(object sender, EventArgs e)
        {
            ApplyResolutionScaling();
            UpdateClock();
            _ = InitializeWebCatalogAsync();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateClock();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Back button — returns to POSCashier main view
        // ─────────────────────────────────────────────────────────────────────
        private void UcBtnBack_Click(object sender, EventArgs e)
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
            this.Parent?.Controls.Remove(this);
        }

        // ─────────────────────────────────────────────────────────────────────
        // General UI helpers
        // ─────────────────────────────────────────────────────────────────────
        private void ConfigureRenderingStyles()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        private void UpdateClock()
        {
            main_menu_gclock_label.Text = DateTime.Now.ToString(ClockFormat);
        }

        // ─────────────────────────────────────────────────────────────────────
        // Resolution scaling
        // ─────────────────────────────────────────────────────────────────────
        private void ApplyResolutionScaling()
        {
            responsiveScaler ??= new ResponsiveScaler(
                main_menu_gunapanel,
                new Size((int)BaseWidth, (int)BaseHeight));

            responsiveScaler.Apply(main_menu_gunapanel.ClientSize);
        }

        private void UC_main_menu_ResizeForResponsiveScaling(object? sender, EventArgs e)
        {
            responsiveScaler?.Apply(main_menu_gunapanel.ClientSize);
        }

        // ─────────────────────────────────────────────────────────────────────
        // Web catalog setup
        // ─────────────────────────────────────────────────────────────────────
        private async Task InitializeWebCatalogAsync()
        {
            if (webCatalogInitialized) return;

            webCatalog = CreateWebCatalogControl();
            webCatalog.Resize += (s, e) => ApplyWebCatalogRoundedCorners();

            doubleBufferedFlowLayoutPanel1.Visible = false;
            main_form_gSsidebar.Visible = false;
            main_menu_gunapanel.Controls.Add(webCatalog);
            webCatalog.BringToFront();
            main_menu_gunagradpanel.BringToFront();
            uc_btn_back.BringToFront();

            await webCatalog.EnsureCoreWebView2Async();
            ConfigureWebCatalogSettings();
            webCatalog.NavigateToString(BuildCatalogHtml());
            ApplyWebCatalogRoundedCorners();

            webCatalogInitialized = true;
        }

        private Microsoft.Web.WebView2.WinForms.WebView2 CreateWebCatalogControl()
        {
            int left = main_form_gSsidebar.Left;
            int top = doubleBufferedFlowLayoutPanel1.Top;
            int right = doubleBufferedFlowLayoutPanel1.Right;
            int width = Math.Max(1, right - left);

            return new Microsoft.Web.WebView2.WinForms.WebView2
            {
                DefaultBackgroundColor = Color.White,
                Name = "uc_main_menu_webview_catalog",
                Location = new Point(left, top),
                Size = new Size(width, doubleBufferedFlowLayoutPanel1.Height),
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

        private void WebCatalog_WebMessageReceived(
            object? sender,
            Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            string message = e.TryGetWebMessageAsString();

            // Plain string signal — open confirm payment with no product data
            if (string.Equals(message, "open_redirect_application", StringComparison.Ordinal))
            {
                ShowConfirmPayment();
                return;
            }

            // JSON payload with product details
            if (!TryParseOpenRedirectPayload(message, out SelectedProductPayload? selectedProduct))
                return;

            ShowConfirmPayment(selectedProduct);
        }

        private static bool TryParseOpenRedirectPayload(string message, out SelectedProductPayload? payload)
        {
            payload = null;

            if (string.IsNullOrWhiteSpace(message) || !message.TrimStart().StartsWith('{'))
                return false;

            try
            {
                using var json = JsonDocument.Parse(message);
                var root = json.RootElement;

                if (!root.TryGetProperty("action", out var actionElement) ||
                    !string.Equals(actionElement.GetString(), "open_redirect_application", StringComparison.Ordinal))
                    return false;

                if (!root.TryGetProperty("product", out var productElement) ||
                    productElement.ValueKind != JsonValueKind.Object)
                {
                    payload = new SelectedProductPayload();
                    return true;
                }

                payload = new SelectedProductPayload
                {
                    Title = productElement.TryGetProperty("title", out var t) ? t.GetString() ?? string.Empty : string.Empty,
                    Sub = productElement.TryGetProperty("sub", out var s) ? s.GetString() ?? string.Empty : string.Empty,
                    Price = productElement.TryGetProperty("price", out var p) ? p.GetString() ?? string.Empty : string.Empty,
                    ImageUrl = productElement.TryGetProperty("imageUrl", out var i) ? i.GetString() ?? string.Empty : string.Empty
                };

                return true;
            }
            catch
            {
                return false;
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // Navigate to ck_confirm_payment (replaces ShowRedirectApplication)
        // ─────────────────────────────────────────────────────────────────────
        private void ShowConfirmPayment(SelectedProductPayload? selectedProduct = null)
        {
            if (this.Parent == null) return;
            var parent = this.Parent;

            // Remove any stale instance
            for (int i = parent.Controls.Count - 1; i >= 0; i--)
                if (parent.Controls[i] is CSP_PROJECT.ck_confirm_payment)
                    parent.Controls.RemoveAt(i);

            var confirmPayment = new CSP_PROJECT.ck_confirm_payment
            {
                Dock = DockStyle.Fill,
                Margin = Padding.Empty
            };

            if (selectedProduct != null)
            {
                confirmPayment.SetProduct(
                    selectedProduct.Title,
                    selectedProduct.Sub,
                    selectedProduct.Price,
                    selectedProduct.ImageUrl);
            }

            // When ck_confirm_payment back button fires, restore this UC
            confirmPayment.CloseRequested += (s, args) =>
            {
                parent.Controls.Remove(confirmPayment);
                confirmPayment.Dispose();
                this.Visible = true;
                this.BringToFront();
                this.Focus();
            };

            this.Visible = false;
            parent.Controls.Add(confirmPayment);
            confirmPayment.BringToFront();
            confirmPayment.Focus();
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

        // ─────────────────────────────────────────────────────────────────────
        // HTML catalog
        // ─────────────────────────────────────────────────────────────────────
        private static string BuildCatalogHtml()
        {
            return CatalogHtmlProvider.GetHtml();
        }
    }
}