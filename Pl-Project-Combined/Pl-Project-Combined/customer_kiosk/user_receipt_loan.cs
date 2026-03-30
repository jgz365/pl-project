using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace customer_kiosk
{
    public partial class user_receipt_loan : UserControl
    {
        private const int AutoReturnSeconds = 15;
        private const string QueueCharacters = "0123456789";
        private const float BaseWidth = 1920f;
        private const float BaseHeight = 1080f;

        private readonly System.Windows.Forms.Timer returnTimer = new();
        private readonly string? providedQueueNumber;
        private ResponsiveScaler? responsiveScaler;
        private int secondsRemaining;

        public user_receipt_loan() : this(null)
        {
        }

        public user_receipt_loan(string? queueNumber)
        {
            InitializeComponent();

            providedQueueNumber = string.IsNullOrWhiteSpace(queueNumber) ? null : queueNumber.Trim();

            Load += UserReceiptLoan_Load;
            SizeChanged += UserReceiptLoan_SizeChanged;
            back_to_menu.Click += Back_to_menu_Click;

            returnTimer.Interval = 1000;
            returnTimer.Tick += ReturnTimer_Tick;
            Disposed += UserReceiptLoan_Disposed;
        }

        private void UserReceiptLoan_Load(object? sender, EventArgs e)
        {
            guna2HtmlLabel2.Visible = false;
            ApplyResponsiveScaling();
            SetQueueCode();
            StartAutoReturnCountdown();
            CenterReceiptComponents();
        }

        private void UserReceiptLoan_SizeChanged(object? sender, EventArgs e)
        {
            ApplyResponsiveScaling();
            CenterReceiptComponents();
        }

        private void ApplyResponsiveScaling()
        {
            responsiveScaler ??= new ResponsiveScaler(this, new Size((int)BaseWidth, (int)BaseHeight));
            responsiveScaler.Apply(this.ClientSize);
        }

        private void SetQueueCode()
        {
            q_random.Text = providedQueueNumber ?? GenerateQueueCode();
            CenterControlHorizontally(q_random, guna2ShadowPanel2);
        }

        private static string GenerateQueueCode()
        {
            Span<char> code = stackalloc char[4];

            for (int i = 0; i < code.Length; i++)
            {
                code[i] = QueueCharacters[Random.Shared.Next(QueueCharacters.Length)];
            }

            return new string(code);
        }

        private void StartAutoReturnCountdown()
        {
            secondsRemaining = AutoReturnSeconds;
            UpdateCounterLabel();
            returnTimer.Start();
        }

        private void ReturnTimer_Tick(object? sender, EventArgs e)
        {
            if (secondsRemaining <= 0)
            {
                returnTimer.Stop();
                ReturnToStartScreen();
                return;
            }

            secondsRemaining--;
            UpdateCounterLabel();
        }

        private void UpdateCounterLabel()
        {
            q_counter.Text = $"Returning to Menu in {secondsRemaining}s";
            CenterControlHorizontally(q_counter, guna2ShadowPanel1);
        }

        private void CenterReceiptComponents()
        {
            CenterControl(guna2ShadowPanel1, this);

            CenterControlHorizontally(pictureBox2, guna2ShadowPanel1);
            CenterControlHorizontally(label1, guna2ShadowPanel1);
            CenterControlHorizontally(label2, guna2ShadowPanel1);
            CenterControlHorizontally(guna2ShadowPanel2, guna2ShadowPanel1);
            CenterControlHorizontally(label3, guna2ShadowPanel1);
            CenterControlHorizontally(label4, guna2ShadowPanel1);
            CenterControlHorizontally(label5, guna2ShadowPanel1);
            CenterControlHorizontally(back_to_menu, guna2ShadowPanel1);
            CenterControlHorizontally(q_counter, guna2ShadowPanel1);
            CenterControlHorizontally(q_random, guna2ShadowPanel2);
        }

        private static void CenterControl(Control control, Control container)
        {
            control.Location = new Point(
                (container.ClientSize.Width - control.Width) / 2,
                (container.ClientSize.Height - control.Height) / 2);
        }

        private static void CenterControlHorizontally(Control control, Control container)
        {
            control.Left = (container.ClientSize.Width - control.Width) / 2;
        }

        private void Back_to_menu_Click(object? sender, EventArgs e)
        {
            ReturnToStartScreen();
        }

        private void ReturnToStartScreen()
        {
            returnTimer.Stop();

            var host = Parent;
            if (host == null)
            {
                return;
            }

            host.Controls.Remove(this);

            var startOverlay = new global::customer_kiosk.start_screen
            {
                Dock = DockStyle.Fill
            };

            startOverlay.StartClicked += async (s, ev) =>
            {
                await startOverlay.SlideOutAsync();

                if (startOverlay.Parent != null)
                {
                    startOverlay.Parent.Controls.Remove(startOverlay);
                }

                startOverlay.Dispose();
            };

            host.Controls.Add(startOverlay);
            startOverlay.BringToFront();

            Dispose();
        }

        private void UserReceiptLoan_Disposed(object? sender, EventArgs e)
        {
            returnTimer.Stop();
            returnTimer.Tick -= ReturnTimer_Tick;
        }
    }
}