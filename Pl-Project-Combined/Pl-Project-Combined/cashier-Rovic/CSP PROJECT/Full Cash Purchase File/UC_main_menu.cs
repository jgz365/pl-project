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

        // ─────────────────────────────────────────────────────────────────────
        // WebView2 message handler — VERSION 2
        //
        // ROOT CAUSE OF THE ORIGINAL BUG:
        //   CatalogHtmlProvider.cs line 383 sends a plain JS *object*:
        //       window.chrome?.webview?.postMessage(payload)
        //   not a string. When JS sends an object, WebView2's
        //   TryGetWebMessageAsString() throws InvalidOperationException, which
        //   was silently swallowed in the catch block → nothing happened.
        //
        // FIX:
        //   Read e.WebMessageAsJson instead. WebMessageAsJson always gives a
        //   JSON string regardless of whether JS sent a string or an object.
        //   This does NOT touch original main_menu.cs at all.
        // ─────────────────────────────────────────────────────────────────────
        private void WebCatalog_WebMessageReceived(
            object? sender,
            Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            // WebMessageAsJson works for BOTH postMessage(object) and
            // postMessage(JSON.stringify(object)) — it always returns a JSON string.
            string rawJson = e.WebMessageAsJson;

            void Handle()
            {
                if (IsDisposed || string.IsNullOrWhiteSpace(rawJson)) return;

                // If JS sent a plain string message (e.g. "open_redirect_application"),
                // WebMessageAsJson wraps it in quotes: "\"open_redirect_application\""
                // Unwrap and handle that case first.
                if (rawJson.Length >= 2 && rawJson[0] == '"' && rawJson[^1] == '"')
                {
                    string plainString = JsonSerializer.Deserialize<string>(rawJson) ?? string.Empty;
                    if (string.Equals(plainString, "open_redirect_application", StringComparison.Ordinal))
                    {
                        ShowConfirmPayment();
                        return;
                    }
                    // Plain string but not a recognised command — ignore.
                    return;
                }

                // JS sent an object → parse the JSON directly.
                if (!TryParseApplyPayload(rawJson, out SelectedProductPayload? product))
                    return;

                ShowConfirmPayment(product);
            }

            if (InvokeRequired)
                BeginInvoke(Handle);
            else
                Handle();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Parse the JSON object that the Apply button posts:
        //   { action: "open_redirect_application", product: { title, sub, price, imageUrl } }
        // ─────────────────────────────────────────────────────────────────────
        private static bool TryParseApplyPayload(string json, out SelectedProductPayload? payload)
        {
            payload = null;
            if (string.IsNullOrWhiteSpace(json)) return false;

            try
            {
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                // Must be an object with action == "open_redirect_application"
                if (root.ValueKind != JsonValueKind.Object) return false;

                if (!root.TryGetProperty("action", out var actionEl) ||
                    !string.Equals(actionEl.GetString(), "open_redirect_application",
                                   StringComparison.Ordinal))
                    return false;

                // product may be null → open confirm with no pre-filled data
                if (!root.TryGetProperty("product", out var productEl) ||
                    productEl.ValueKind != JsonValueKind.Object)
                {
                    payload = new SelectedProductPayload();
                    return true;
                }

                payload = new SelectedProductPayload
                {
                    Title = productEl.TryGetProperty("title", out var t) ? t.GetString() ?? string.Empty : string.Empty,
                    Sub = productEl.TryGetProperty("sub", out var s) ? s.GetString() ?? string.Empty : string.Empty,
                    Price = productEl.TryGetProperty("price", out var p) ? p.GetString() ?? string.Empty : string.Empty,
                    ImageUrl = productEl.TryGetProperty("imageUrl", out var i) ? i.GetString() ?? string.Empty : string.Empty
                };

                return true;
            }
            catch
            {
                return false;
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // Navigate to ck_confirm_payment inside this UC's own panel.
        // Keeps the navigation entirely self-contained — no dependency on
        // whatever parent POSCashier happens to have at the moment.
        // ─────────────────────────────────────────────────────────────────────
        private void ShowConfirmPayment(SelectedProductPayload? selectedProduct = null)
        {
            // Remove any stale instance (e.g. user double-taps Apply)
            for (int i = main_menu_gunapanel.Controls.Count - 1; i >= 0; i--)
            {
                if (main_menu_gunapanel.Controls[i] is CSP_PROJECT.ck_confirm_payment stale)
                {
                    main_menu_gunapanel.Controls.RemoveAt(i);
                    stale.Dispose();
                }
            }

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

            // Back button inside ck_confirm_payment → remove it and reveal the
            // web catalog that is still sitting underneath in gunapanel.
            confirmPayment.CloseRequested += (s, args) =>
            {
                main_menu_gunapanel.Controls.Remove(confirmPayment);
                confirmPayment.Dispose();

                // Restore z-order: catalog visible, header + back btn on top.
                webCatalog?.BringToFront();
                main_menu_gunagradpanel.BringToFront();
                uc_btn_back.BringToFront();
            };

            main_menu_gunapanel.Controls.Add(confirmPayment);
            confirmPayment.BringToFront();
            // Keep the AsensoMoto header and Back button always visible on top.
            main_menu_gunagradpanel.BringToFront();
            uc_btn_back.BringToFront();
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
            return UC_CatalogHtmlProvider.GetHtml();
        }
    }
}