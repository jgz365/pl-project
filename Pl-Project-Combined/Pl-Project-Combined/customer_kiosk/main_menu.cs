using System.Text.Json;

namespace customer_kiosk
{
    public partial class main_menu : Form
    {
        // Web catalog host.
        private Microsoft.Web.WebView2.WinForms.WebView2? webCatalog;
        private redirect_application? redirectApplicationView;
        private Button? hiddenAssessorSwitchButton;

        // One-time guards.
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

        public main_menu()
        {
            InitializeComponent();
            ConfigureFormRenderingStyles();
            EnsureHiddenAssessorSwitchButton();
            Resize += MainMenu_ResizeForResponsiveScaling;
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

        private void EnsureHiddenAssessorSwitchButton()
        {
            if (hiddenAssessorSwitchButton != null)
            {
                return;
            }

            hiddenAssessorSwitchButton = new Button
            {
                Name = "hidden_assessor_switch_button",
                Size = new Size(18, 18),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.Transparent,
                Text = string.Empty,
                TabStop = false,
                UseVisualStyleBackColor = false,
                Cursor = Cursors.Default
            };

            hiddenAssessorSwitchButton.FlatAppearance.BorderSize = 0;
            hiddenAssessorSwitchButton.Click += HiddenAssessorSwitchButton_Click;

            Controls.Add(hiddenAssessorSwitchButton);
            PositionHiddenAssessorSwitchButton();
            hiddenAssessorSwitchButton.BringToFront();

            Resize -= MainMenu_ResizeForHiddenSwitch;
            Resize += MainMenu_ResizeForHiddenSwitch;
        }

        private void MainMenu_ResizeForHiddenSwitch(object? sender, EventArgs e)
        {
            PositionHiddenAssessorSwitchButton();
        }

        private void PositionHiddenAssessorSwitchButton()
        {
            if (hiddenAssessorSwitchButton == null)
            {
                return;
            }

            hiddenAssessorSwitchButton.Location = new Point(ClientSize.Width - hiddenAssessorSwitchButton.Width - 8, 8);
            hiddenAssessorSwitchButton.BringToFront();
        }

        private void HiddenAssessorSwitchButton_Click(object? sender, EventArgs e)
        {
            var assessorLogin = new Pl_Project_Combined.Assessor_Eddion.LoginUiForm
            {
                StartPosition = FormStartPosition.CenterScreen
            };

            assessorLogin.FormClosed += (_, __) =>
            {
                if (!IsDisposed)
                {
                    Show();
                    BringToFront();
                }
            };

            Hide();
            assessorLogin.Show();
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
            responsiveScaler ??= new ResponsiveScaler(main_menu_gunapanel, new Size((int)BaseWidth, (int)BaseHeight));
            responsiveScaler.Apply(main_menu_gunapanel.ClientSize);
        }

        private void MainMenu_ResizeForResponsiveScaling(object? sender, EventArgs e)
        {
            responsiveScaler?.Apply(main_menu_gunapanel.ClientSize);
        }

        #endregion

        #region Web catalog setup

        private async Task InitializeWebCatalogAsync(start_screen? overlay = null)
        {
            if (webCatalogInitialized) return;

            webCatalog = CreateWebCatalogControl();
            webCatalog.Resize += (s, e) => ApplyWebCatalogRoundedCorners();

            doubleBufferedFlowLayoutPanel1.Visible = false;
            main_form_gSsidebar.Visible = false;
            main_menu_gunapanel.Controls.Add(webCatalog);
            webCatalog.BringToFront();
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
            int left = main_form_gSsidebar.Left;
            int top = doubleBufferedFlowLayoutPanel1.Top;
            int right = doubleBufferedFlowLayoutPanel1.Right;
            int width = Math.Max(1, right - left);

            return new Microsoft.Web.WebView2.WinForms.WebView2
            {
                DefaultBackgroundColor = Color.White,
                Name = "main_menu_webview_catalog",

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

        private void WebCatalog_WebMessageReceived(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            string? message = null;
            string? jsonMessage = null;

            try
            {
                message = e.TryGetWebMessageAsString();
            }
            catch
            {
                // Non-string payloads are handled via JSON branch below.
            }

            try
            {
                jsonMessage = e.WebMessageAsJson;
            }
            catch
            {
                // Keep null when payload is not exposed as JSON.
            }

            if (string.Equals(message, "open_redirect_application", StringComparison.Ordinal))
            {
                ShowRedirectApplication();
                return;
            }

            if (!TryParseOpenRedirectPayload(message, jsonMessage, out SelectedProductPayload? selectedProduct))
            {
                return;
            }

            ShowRedirectApplication(selectedProduct);
        }

        private static bool TryParseOpenRedirectPayload(string? message, string? jsonMessage, out SelectedProductPayload? payload)
        {
            payload = null;

            string? effectiveJson = null;

            if (!string.IsNullOrWhiteSpace(message) && message.TrimStart().StartsWith('{'))
            {
                effectiveJson = message;
            }
            else if (!string.IsNullOrWhiteSpace(jsonMessage) && jsonMessage.TrimStart().StartsWith('{'))
            {
                effectiveJson = jsonMessage;
            }
            else
            {
                return false;
            }

            try
            {
                using var json = JsonDocument.Parse(effectiveJson);
                var root = json.RootElement;

                if (!root.TryGetProperty("action", out var actionElement) ||
                    !string.Equals(actionElement.GetString(), "open_redirect_application", StringComparison.Ordinal))
                {
                    return false;
                }

                if (!root.TryGetProperty("product", out var productElement) ||
                    productElement.ValueKind != JsonValueKind.Object)
                {
                    payload = new SelectedProductPayload();
                    return true;
                }

                payload = new SelectedProductPayload
                {
                    Title = productElement.TryGetProperty("title", out var title) ? title.GetString() ?? string.Empty : string.Empty,
                    Sub = productElement.TryGetProperty("sub", out var sub) ? sub.GetString() ?? string.Empty : string.Empty,
                    Price = productElement.TryGetProperty("price", out var price) ? price.GetString() ?? string.Empty : string.Empty,
                    ImageUrl = productElement.TryGetProperty("imageUrl", out var imageUrl) ? imageUrl.GetString() ?? string.Empty : string.Empty
                };

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void ShowRedirectApplication(SelectedProductPayload? selectedProduct = null)
        {
            if (redirectApplicationView == null || redirectApplicationView.IsDisposed)
            {
                redirectApplicationView = new redirect_application
                {
                    Dock = DockStyle.Fill
                };

                main_menu_gunapanel.Controls.Add(redirectApplicationView);
            }

            if (selectedProduct != null)
            {
                redirectApplicationView.SetSelectedProduct(
                    selectedProduct.Title,
                    selectedProduct.Sub,
                    selectedProduct.Price,
                    selectedProduct.ImageUrl);
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
