// ═══════════════════════════════════════════════════════════════════════════
//  Form_TransactionHistory.cs  — FIXED VERSION
//
//  KEY CHANGES:
//  • No hardcoded sample data.  Data comes from TransactionStore.All (a
//    shared static list populated when sub-forms complete a transaction).
//  • Rounded-corner form fixed: form BackColor = Magenta (TransparencyKey).
//  • Column header row added to match reference screenshot.
//  • Row layout matches screenshot: ID | Time | Type badge | Customer/Model
//    | Amount | Status badge, all aligned to the column headers.
//  • Scroll, search, type-filter, status-filter all fully functional.
//  • Empty-state panel shown when there are no transactions to display.
// ═══════════════════════════════════════════════════════════════════════════
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace POSCashierSystem
{
    // ════════════════════════════════════════════════════════════════════════
    //  SHARED TRANSACTION STORE  (replaces hardcoded mock data)
    //  Sub-forms call  TransactionStore.Add(tx)  when a payment is confirmed.
    // ════════════════════════════════════════════════════════════════════════
    public static class TransactionStore
    {
        private static readonly List<Transaction> _store = new List<Transaction>();

        /// <summary>All transactions recorded this session (newest first).</summary>
        public static IReadOnlyList<Transaction> All => _store.AsReadOnly();

        /// <summary>Add a completed transaction (call from payment sub-forms).</summary>
        public static void Add(Transaction tx)
        {
            if (tx != null) _store.Insert(0, tx);  // newest at top
        }

        /// <summary>Clear — useful for unit-tests / session reset.</summary>
        public static void Clear() => _store.Clear();
    }

    // ════════════════════════════════════════════════════════════════════════
    //  FORM
    // ════════════════════════════════════════════════════════════════════════
    public partial class Form_TransactionHistory : Form
    {
        // ── Colour palette ────────────────────────────────────────────────
        private static readonly Color ClrCard = Color.White;
        private static readonly Color ClrBorder = Color.FromArgb(232, 236, 241);
        private static readonly Color ClrTextPrimary = Color.FromArgb(30, 40, 55);
        private static readonly Color ClrTextSecondary = Color.FromArgb(120, 132, 152);
        private static readonly Color ClrGreen = Color.FromArgb(52, 199, 160);
        private static readonly Color ClrGreenLight = Color.FromArgb(230, 250, 244);
        private static readonly Color ClrOrange = Color.FromArgb(255, 166, 77);
        private static readonly Color ClrOrangeLight = Color.FromArgb(255, 243, 229);
        private static readonly Color ClrRowHover = Color.FromArgb(245, 248, 252);
        private static readonly Color ClrPillActive = Color.FromArgb(30, 40, 55);
        private static readonly Color ClrPillInactive = Color.FromArgb(241, 243, 247);

        // ── State ─────────────────────────────────────────────────────────
        private List<Transaction> _all;
        private List<Transaction> _filtered;
        private string _activeType = "All";
        private string _activeStatus = "All";

        private readonly List<Guna2Button> _typePills = new List<Guna2Button>();
        private readonly List<Guna2Button> _statusPills = new List<Guna2Button>();

        // ═════════════════════════════════════════════════════════════════
        //  CONSTRUCTOR
        // ═════════════════════════════════════════════════════════════════
        public Form_TransactionHistory()
        {
            InitializeComponent();

            // Pull from the shared store (no hardcoded data)
            _all = new List<Transaction>(TransactionStore.All);
            _filtered = new List<Transaction>(_all);

            BuildPillButtons();
            RenderRows();
            UpdateFooter();
        }

        // ═════════════════════════════════════════════════════════════════
        //  EVENT HANDLERS  (referenced by Designer.cs)
        // ═════════════════════════════════════════════════════════════════
        private void BtnClose_Click(object sender, EventArgs e) => Close();

        private void Form_TransactionHistory_KeyDown(object sender, KeyEventArgs e)
        { if (e.KeyCode == Keys.Escape) Close(); }

        private void BtnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.FillColor = Color.FromArgb(255, 235, 235);
            btnClose.ForeColor = Color.FromArgb(220, 60, 60);
        }
        private void BtnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.FillColor = Color.FromArgb(242, 244, 248);
            btnClose.ForeColor = Color.FromArgb(130, 140, 160);
        }

        private void PnlFooter_Paint(object sender, PaintEventArgs e)
        {
            using (var pen = new Pen(Color.FromArgb(232, 235, 245)))
                e.Graphics.DrawLine(pen, 0, 0, pnlFooter.Width, 0);
        }

        private void PnlTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                NativeMethods.ReleaseCapture();
                NativeMethods.SendMessage(Handle, 0x0112, 0xF010 + 0x0002, IntPtr.Zero);
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e) => ApplyFilters();

        // ═════════════════════════════════════════════════════════════════
        //  PILL BUTTONS
        // ═════════════════════════════════════════════════════════════════
        private void BuildPillButtons()
        {
            string[] types = { "All", "Down Payment", "Full Cash", "Monthly Payment",
                                   "Advance Payment", "Full Settlement", "Other Services" };
            string[] statuses = { "All", "Paid", "Pending" };

            int typeX = 0;
            foreach (string t in types)
            {
                var btn = CreatePill(t);
                string cap = t;
                btn.Left = typeX;
                btn.Click += (s, e) => { _activeType = cap; RefreshPillStates(_typePills, btn); ApplyFilters(); };
                _typePills.Add(btn);
                pnlTypePills.Controls.Add(btn);
                typeX += btn.Width + 6;
            }
            SetPillActive(_typePills[0]);

            int statusX = 0;
            foreach (string st in statuses)
            {
                var btn = CreatePill(st);
                string cap = st;
                btn.Left = statusX;
                btn.Click += (s, e) => { _activeStatus = cap; RefreshPillStates(_statusPills, btn); ApplyFilters(); };
                _statusPills.Add(btn);
                pnlStatusPills.Controls.Add(btn);
                statusX += btn.Width + 6;
            }
            SetPillActive(_statusPills[0]);
        }

        private Guna2Button CreatePill(string text)
        {
            // Measure width first so we can lay them out manually (FlowLayout
            // inside a fixed-height Panel can clip; manual is safer).
            var btn = new Guna2Button
            {
                Text = text,
                Height = 30,
                Width = TextRenderer.MeasureText(text, new Font("Segoe UI", 8.5f)).Width + 28,
                Top = 0,
                BorderRadius = 15,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Regular),
                FillColor = ClrPillInactive,
                ForeColor = ClrTextSecondary,
                BorderColor = Color.Transparent,
                BorderThickness = 0,
                Animated = true,
                Cursor = Cursors.Hand,
            };
            return btn;
        }

        private void SetPillActive(Guna2Button btn)
        {
            btn.FillColor = ClrPillActive;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
        }
        private void SetPillInactive(Guna2Button btn)
        {
            btn.FillColor = ClrPillInactive;
            btn.ForeColor = ClrTextSecondary;
            btn.Font = new Font("Segoe UI", 8.5f, FontStyle.Regular);
        }
        private void RefreshPillStates(List<Guna2Button> pills, Guna2Button active)
        {
            foreach (var p in pills) SetPillInactive(p);
            SetPillActive(active);
        }

        // ═════════════════════════════════════════════════════════════════
        //  FILTER PIPELINE
        // ═════════════════════════════════════════════════════════════════
        private void ApplyFilters()
        {
            string q = txtSearch.Text.Trim().ToLowerInvariant();

            _filtered = _all.Where(t =>
            {
                bool typeOk = _activeType == "All" || t.PaymentType.Equals(_activeType, StringComparison.OrdinalIgnoreCase);
                bool statusOk = _activeStatus == "All" || t.Status.Equals(_activeStatus, StringComparison.OrdinalIgnoreCase);
                bool searchOk = string.IsNullOrEmpty(q)
                                || t.TransactionId.ToLowerInvariant().Contains(q)
                                || t.CustomerName.ToLowerInvariant().Contains(q)
                                || t.UnitModel.ToLowerInvariant().Contains(q)
                                || t.PaymentType.ToLowerInvariant().Contains(q);
                return typeOk && statusOk && searchOk;
            }).ToList();

            RenderRows();
            UpdateFooter();
        }

        // ═════════════════════════════════════════════════════════════════
        //  ROW RENDERING
        // ═════════════════════════════════════════════════════════════════
        private void RenderRows()
        {
            flowRows.SuspendLayout();
            flowRows.Controls.Clear();

            if (_filtered.Count == 0)
            {
                flowRows.Controls.Add(BuildEmptyState());
            }
            else
            {
                bool shaded = false;
                foreach (var tx in _filtered)
                {
                    flowRows.Controls.Add(BuildRow(tx, shaded));
                    shaded = !shaded;
                }
            }

            flowRows.ResumeLayout(true);
        }

        // ── Empty-state placeholder ────────────────────────────────────────
        private Panel BuildEmptyState()
        {
            int w = flowRows.Width - (flowRows.VerticalScroll.Visible ? 20 : 4);
            var pnl = new Panel { Width = w, Height = flowRows.Height - 10, BackColor = Color.White };

            var icon = new Label
            {
                Text = "💵",
                Font = new Font("Segoe UI", 28f),
                ForeColor = Color.FromArgb(210, 215, 230),
                AutoSize = false,
                Width = w,
                Height = 60,
                Top = 70,
                TextAlign = ContentAlignment.MiddleCenter,
            };
            var msg = new Label
            {
                Text = "No transactions yet today.",
                Font = new Font("Segoe UI Semibold", 10f, FontStyle.Bold),
                ForeColor = Color.FromArgb(160, 168, 190),
                AutoSize = false,
                Width = w,
                Height = 30,
                Top = 135,
                TextAlign = ContentAlignment.MiddleCenter,
            };
            var sub = new Label
            {
                Text = "Transactions will appear here once processed.",
                Font = new Font("Segoe UI", 9f),
                ForeColor = Color.FromArgb(190, 196, 212),
                AutoSize = false,
                Width = w,
                Height = 24,
                Top = 165,
                TextAlign = ContentAlignment.MiddleCenter,
            };

            pnl.Controls.AddRange(new Control[] { icon, msg, sub });
            return pnl;
        }

        // ── Single transaction row ─────────────────────────────────────────
        //  Column positions must mirror the column headers in the Designer:
        //  ID@24  |  Time@118  |  TypeBadge@192  |  Customer@390  |  Amount@660  |  Status@806
        private Panel BuildRow(Transaction tx, bool shaded)
        {
            int w = flowRows.Width - (flowRows.VerticalScroll.Visible ? 17 : 0);

            var row = new Panel
            {
                Width = w,
                Height = 56,
                BackColor = shaded ? Color.FromArgb(250, 251, 253) : ClrCard,
                Cursor = Cursors.Hand,
            };

            // Left accent stripe
            var accent = new Panel
            {
                Width = 4,
                Height = 38,
                Left = 0,
                Top = 9,
                BackColor = TypeAccentColor(tx.PaymentType),
            };

            // ID
            var lblId = new Label
            {
                Text = tx.TransactionId,
                Font = new Font("Segoe UI Semibold", 8.5f, FontStyle.Bold),
                ForeColor = ClrTextPrimary,
                AutoSize = false,
                Width = 88,
                Height = 56,
                Left = 12,
                TextAlign = ContentAlignment.MiddleLeft,
            };

            // Time
            var lblTime = new Label
            {
                Text = tx.DateTime.ToString("hh:mm tt"),
                Font = new Font("Segoe UI", 7.5f),
                ForeColor = ClrTextSecondary,
                AutoSize = false,
                Width = 66,
                Height = 56,
                Left = 118,
                TextAlign = ContentAlignment.MiddleLeft,
            };

            // Type badge (pill)
            var typeBadge = CreateTypeBadge(tx.PaymentType);
            typeBadge.Left = 192;
            typeBadge.Top = (56 - typeBadge.Height) / 2;

            // Customer + model stacked
            var pnlCustomer = new Panel { Width = 220, Height = 56, Left = 390, BackColor = Color.Transparent };
            pnlCustomer.Controls.Add(new Label
            {
                Text = tx.CustomerName,
                Font = new Font("Segoe UI Semibold", 8.5f, FontStyle.Bold),
                ForeColor = ClrTextPrimary,
                AutoSize = false,
                Width = 220,
                Height = 28,
                Top = 6,
                TextAlign = ContentAlignment.BottomLeft,
            });
            pnlCustomer.Controls.Add(new Label
            {
                Text = tx.UnitModel,
                Font = new Font("Segoe UI", 7.5f),
                ForeColor = ClrTextSecondary,
                AutoSize = false,
                Width = 220,
                Height = 22,
                Top = 30,
                TextAlign = ContentAlignment.TopLeft,
            });

            // Amount
            var lblAmount = new Label
            {
                Text = "₱" + tx.Amount.ToString("N2"),
                Font = new Font("Segoe UI Semibold", 9f, FontStyle.Bold),
                ForeColor = ClrTextPrimary,
                AutoSize = false,
                Width = 110,
                Height = 56,
                Left = 660,
                TextAlign = ContentAlignment.MiddleLeft,
            };

            // Status badge
            var statusBadge = CreateStatusBadge(tx.Status);
            statusBadge.Left = 806;
            statusBadge.Top = (56 - statusBadge.Height) / 2;

            // Thin bottom divider
            var divider = new Panel
            {
                Height = 1,
                Width = w - 20,
                Left = 10,
                Top = 55,
                BackColor = ClrBorder,
            };

            row.Controls.AddRange(new Control[]
            {
                accent, lblId, lblTime, typeBadge, pnlCustomer,
                lblAmount, statusBadge, divider
            });

            AttachRowHover(row, shaded);
            return row;
        }

        // ── Badge factories ────────────────────────────────────────────────
        private Label CreateTypeBadge(string paymentType)
        {
            (Color bg, Color fg) = TypeBadgeColors(paymentType);
            // shorten text to fit 180 px badge
            string text = paymentType.ToUpperInvariant();
            if (text == "MONTHLY PAYMENT") text = "MONTHLY PMT";
            if (text == "ADVANCE PAYMENT") text = "ADVANCE PMT";
            if (text == "FULL SETTLEMENT") text = "FULL SETTLE";
            if (text == "DOWN PAYMENT") text = "DOWN PAYMENT";
            if (text == "OTHER SERVICES") text = "OTHER SVCS";

            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 6.8f, FontStyle.Bold),
                ForeColor = fg,
                BackColor = bg,
                Width = 138,
                Height = 22,
                Padding = new Padding(6, 0, 6, 0),
                TextAlign = ContentAlignment.MiddleCenter,
            };
        }

        private Label CreateStatusBadge(string status)
        {
            bool paid = status.Equals("Paid", StringComparison.OrdinalIgnoreCase);
            return new Label
            {
                Text = status.ToUpperInvariant(),
                Font = new Font("Segoe UI", 7f, FontStyle.Bold),
                ForeColor = paid ? ClrGreen : ClrOrange,
                BackColor = paid ? ClrGreenLight : ClrOrangeLight,
                Width = 60,
                Height = 22,
                TextAlign = ContentAlignment.MiddleCenter,
            };
        }

        private void AttachRowHover(Panel row, bool shaded)
        {
            Color normal = shaded ? Color.FromArgb(250, 251, 253) : ClrCard;
            row.MouseEnter += (s, e) => row.BackColor = ClrRowHover;
            row.MouseLeave += (s, e) => row.BackColor = normal;
            foreach (Control c in row.Controls)
            {
                c.MouseEnter += (s, e) => row.BackColor = ClrRowHover;
                c.MouseLeave += (s, e) => row.BackColor = normal;
            }
        }

        // ═════════════════════════════════════════════════════════════════
        //  FOOTER
        // ═════════════════════════════════════════════════════════════════
        private void UpdateFooter()
        {
            int count = _filtered.Count;
            lblTxCount.Text = $"Showing {count} transaction{(count != 1 ? "s" : "")}";
            lblTotalAmt.Text = "₱" + _filtered.Sum(t => t.Amount).ToString("N2");
        }

        // ═════════════════════════════════════════════════════════════════
        //  COLOUR HELPERS
        // ═════════════════════════════════════════════════════════════════
        private static Color TypeAccentColor(string type) => type switch
        {
            "Down Payment" => Color.FromArgb(102, 217, 189),
            "Full Cash" => Color.FromArgb(188, 140, 255),
            "Monthly Payment" => Color.FromArgb(138, 171, 255),
            "Advance Payment" => Color.FromArgb(102, 217, 189),
            "Full Settlement" => Color.FromArgb(255, 138, 138),
            "Other Services" => Color.FromArgb(255, 186, 115),
            _ => Color.FromArgb(180, 180, 180),
        };

        private static (Color bg, Color fg) TypeBadgeColors(string type) => type switch
        {
            "Down Payment" => (Color.FromArgb(230, 250, 244), Color.FromArgb(22, 163, 122)),
            "Full Cash" => (Color.FromArgb(243, 237, 255), Color.FromArgb(130, 80, 220)),
            "Monthly Payment" => (Color.FromArgb(235, 240, 255), Color.FromArgb(70, 105, 230)),
            "Advance Payment" => (Color.FromArgb(230, 250, 244), Color.FromArgb(22, 163, 122)),
            "Full Settlement" => (Color.FromArgb(255, 238, 238), Color.FromArgb(200, 60, 60)),
            "Other Services" => (Color.FromArgb(255, 243, 229), Color.FromArgb(200, 130, 30)),
            _ => (Color.FromArgb(241, 243, 247), Color.FromArgb(120, 132, 152)),
        };
    }
}