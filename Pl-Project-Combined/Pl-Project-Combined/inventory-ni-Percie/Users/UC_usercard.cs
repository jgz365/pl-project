using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace inventory_ni_Percie
{
    public partial class UC_UserCard : UserControl
    {
        public event EventHandler? OnEditClick;
        public event EventHandler? OnArchiveClick;
        public event EventHandler? OnDeleteClick;

        public string UserID { get; private set; } = "";
        public string FullName { get; private set; } = "";
        public string Username { get; private set; } = "";
        public string Role { get; private set; } = "";
        public string UserStatus { get; private set; } = "";
        public string LastLogin { get; private set; } = "";

        public UC_UserCard()
        {
            InitializeComponent();
        }

        public void SetUserData(string id, string fullName, string username,
                                string role, string status, string lastLogin)
        {
            UserID = id;
            FullName = fullName;
            Username = username;
            Role = role;
            UserStatus = status;
            LastLogin = lastLogin;

            lblFullName.Text = fullName;
            lblUsername.Text = "@" + username.TrimStart('@');

            string roleIcon = role switch
            {
                "Super Admin" => "👑 ",
                "Assessor" => "○ ",
                "POS Cashier" => "◇ ",
                "Inventory" => "○ ",
                _ => "• "
            };
            lblRoleIcon.Text = roleIcon;
            lblRoleText.Text = role;
            ApplyRoleStyle(role);

            bool active = status.Equals("Active", StringComparison.OrdinalIgnoreCase);
            pnlStatusDot.BackColor = active ? Color.FromArgb(34, 197, 94) : Color.FromArgb(239, 68, 68);
            pnlStatusPill.BackColor = active ? Color.FromArgb(220, 252, 231) : Color.FromArgb(254, 226, 226);
            lblStatusText.Text = status;
            lblStatusText.ForeColor = active ? Color.FromArgb(22, 163, 74) : Color.FromArgb(220, 38, 38);
            pnlStatusPill.Invalidate();
            pnlStatusDot.Invalidate();

            lblLastLogin.Text = lastLogin;
        }

        public void SetArchiveMode(bool isArchiveView)
        {
            if (isArchiveView)
            {
                btnEdit.Text = "↩  Restore";
                btnEdit.FillColor = Color.FromArgb(220, 252, 231);
                btnEdit.ForeColor = Color.FromArgb(22, 163, 74);
                btnEdit.BorderColor = Color.FromArgb(187, 247, 208);
                btnEdit.HoverState.FillColor = Color.FromArgb(187, 247, 208);
                btnEdit.HoverState.ForeColor = Color.FromArgb(15, 118, 60);

                btnArchive.Text = "🗑  Delete";
                btnArchive.FillColor = Color.FromArgb(254, 226, 226);
                btnArchive.ForeColor = Color.FromArgb(220, 38, 38);
                btnArchive.BorderColor = Color.FromArgb(252, 165, 165);
                btnArchive.HoverState.FillColor = Color.FromArgb(252, 165, 165);
                btnArchive.HoverState.ForeColor = Color.FromArgb(185, 28, 28);
                btnArchive.Visible = true;
            }
            else
            {
                btnEdit.Text = "✎  Edit";
                btnEdit.FillColor = Color.FromArgb(239, 246, 255);
                btnEdit.ForeColor = Color.FromArgb(37, 99, 235);
                btnEdit.BorderColor = Color.FromArgb(219, 234, 254);
                btnEdit.HoverState.FillColor = Color.FromArgb(214, 226, 255);
                btnEdit.HoverState.ForeColor = Color.FromArgb(29, 78, 216);

                btnArchive.Text = "🗄  Archive";
                btnArchive.FillColor = Color.FromArgb(255, 247, 237);
                btnArchive.ForeColor = Color.FromArgb(194, 65, 12);
                btnArchive.BorderColor = Color.FromArgb(254, 215, 170);
                btnArchive.HoverState.FillColor = Color.FromArgb(254, 200, 130);
                btnArchive.HoverState.ForeColor = Color.FromArgb(154, 52, 18);
                btnArchive.Visible = true;
            }
        }

        private void ApplyRoleStyle(string role)
        {
            switch (role)
            {
                case "Super Admin":
                    lblRoleIcon.ForeColor = Color.FromArgb(245, 158, 11);
                    lblRoleText.ForeColor = Color.FromArgb(71, 85, 105);
                    break;
                case "Assessor":
                    lblRoleIcon.ForeColor = Color.FromArgb(59, 130, 246);
                    lblRoleText.ForeColor = Color.FromArgb(71, 85, 105);
                    break;
                case "POS Cashier":
                    lblRoleIcon.ForeColor = Color.FromArgb(20, 184, 166);
                    lblRoleText.ForeColor = Color.FromArgb(71, 85, 105);
                    break;
                default:
                    lblRoleIcon.ForeColor = Color.FromArgb(148, 163, 184);
                    lblRoleText.ForeColor = Color.FromArgb(71, 85, 105);
                    break;
            }
        }

        // ── Edit button — opens FormEditUser with blur overlay ────────────────
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Fire the event so UC_Users can handle it with overlay
            OnEditClick?.Invoke(this, EventArgs.Empty);
        }

        private void btnArchive_Click(object sender, EventArgs e)
        {
            OnArchiveClick?.Invoke(this, EventArgs.Empty);
            OnDeleteClick?.Invoke(this, EventArgs.Empty);
        }

        private void cardPanel_MouseEnter(object sender, EventArgs e)
            => this.BackColor = Color.FromArgb(249, 250, 252);

        private void cardPanel_MouseLeave(object sender, EventArgs e)
        {
            if (!this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
                this.BackColor = Color.White;
        }

        private void pnlStatusDot_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var br = new SolidBrush(pnlStatusDot.BackColor);
            e.Graphics.FillEllipse(br, 0, 0, pnlStatusDot.Width, pnlStatusDot.Height);
        }

        private void pnlStatusPill_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var gp = RoundedRect(0, 0, pnlStatusPill.Width - 1, pnlStatusPill.Height - 1, 12);
            using var br = new SolidBrush(pnlStatusPill.BackColor);
            e.Graphics.FillPath(br, gp);
        }

        private static System.Drawing.Drawing2D.GraphicsPath RoundedRect(int x, int y, int w, int h, int r)
        {
            var gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddArc(x, y, r * 2, r * 2, 180, 90);
            gp.AddArc(x + w - r * 2, y, r * 2, r * 2, 270, 90);
            gp.AddArc(x + w - r * 2, y + h - r * 2, r * 2, r * 2, 0, 90);
            gp.AddArc(x, y + h - r * 2, r * 2, r * 2, 90, 90);
            gp.CloseFigure();
            return gp;
        }
    }
}