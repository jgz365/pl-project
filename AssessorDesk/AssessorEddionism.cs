using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace AssessorDesk
{
    public partial class AssessorEddionism : Form
    {
        public AssessorEddionism()
        {
            InitializeComponent();

            PreparePanels();

            // Wire click events (toggle behavior)
            CustomerProfileButton.Click -= CustomerProfileButton_Click;
            CustomerProfileButton.Click += CustomerProfileButton_Click;

            FinancialAnalysisButton.Click -= FinancialAnalysisButton_Click;
            FinancialAnalysisButton.Click += FinancialAnalysisButton_Click;

            UnitDetailsButton.Click -= UnitDetailsButton_Click;
            UnitDetailsButton.Click += UnitDetailsButton_Click;

            VerificationButton.Click -= VerificationButton_Click;
            VerificationButton.Click += VerificationButton_Click;

            AssessmentNotesButton.Click -= AssessmentNotesButton_Click;
            AssessmentNotesButton.Click += AssessmentNotesButton_Click;

            AuditTrailButton.Click -= AuditTrailButton_Click;
            AuditTrailButton.Click += AuditTrailButton_Click;

            // Start with all panels hidden and all buttons unchecked
            ShowOnlyPanel(null);
        }

        // --- Click handlers (toggle using boolean Checked property) ---
        private void CustomerProfileButton_Click(object sender, EventArgs e)
        {
            bool willShow = !CustomerProfilePanel.Visible;
            ShowOnlyPanel(willShow ? CustomerProfilePanel : null);
            SetChecked(CustomerProfileButton, willShow);
        }

        private void FinancialAnalysisButton_Click(object sender, EventArgs e)
        {
            bool willShow = !FinancialAnalysisPanel.Visible;
            ShowOnlyPanel(willShow ? FinancialAnalysisPanel : null);
            SetChecked(FinancialAnalysisButton, willShow);
        }

        private void UnitDetailsButton_Click(object sender, EventArgs e)
        {
            bool willShow = !UnitDetailsPanel.Visible;
            ShowOnlyPanel(willShow ? UnitDetailsPanel : null);
            SetChecked(UnitDetailsButton, willShow);
        }

        private void VerificationButton_Click(object sender, EventArgs e)
        {
            bool willShow = !VerificationPanel.Visible;
            ShowOnlyPanel(willShow ? VerificationPanel : null);
            SetChecked(VerificationButton, willShow);
        }

        private void AssessmentNotesButton_Click(object sender, EventArgs e)
        {
            bool willShow = !AssessmentNotesPanel.Visible;
            ShowOnlyPanel(willShow ? AssessmentNotesPanel : null);
            SetChecked(AssessmentNotesButton, willShow);
        }

        private void AuditTrailButton_Click(object sender, EventArgs e)
        {
            bool willShow = !AuditTrailPanel.Visible;
            ShowOnlyPanel(willShow ? AuditTrailPanel : null);
            SetChecked(AuditTrailButton, willShow);
        }

        // --- Prepare panels so they share PanelSpace and don't affect each other ---
        private void PreparePanels()
        {
            var panels = new Control[]
            {
                CustomerProfilePanel,
                FinancialAnalysisPanel,
                UnitDetailsPanel,
                VerificationPanel,
                AssessmentNotesPanel,
                AuditTrailPanel
            };

            if (PanelSpace == null) throw new InvalidOperationException("PanelSpace is null. Check Designer.");

            foreach (var p in panels)
            {
                if (p == null) continue;

                if (p.Parent != PanelSpace)
                {
                    if (p.Parent != null) p.Parent.Controls.Remove(p);
                    PanelSpace.Controls.Add(p);
                }

                p.Dock = DockStyle.Fill;
                p.Visible = false;
            }

            if (PanelSpace.Dock == DockStyle.None)
                PanelSpace.Dock = DockStyle.Fill;
        }

        // --- ShowOnlyPanel: show exactly one panel (or none if null) ---
        private void ShowOnlyPanel(Control panelToShow)
        {
            // Hide all panels first
            CustomerProfilePanel.Visible = false;
            FinancialAnalysisPanel.Visible = false;
            UnitDetailsPanel.Visible = false;
            VerificationPanel.Visible = false;
            AssessmentNotesPanel.Visible = false;
            AuditTrailPanel.Visible = false;

            // If null, clear all button Checked states
            if (panelToShow == null)
            {
                SetChecked(CustomerProfileButton, false);
                SetChecked(FinancialAnalysisButton, false);
                SetChecked(UnitDetailsButton, false);
                SetChecked(VerificationButton, false);
                SetChecked(AssessmentNotesButton, false);
                SetChecked(AuditTrailButton, false);
                return;
            }

            // Ensure panel is parented to PanelSpace and fills the area
            if (panelToShow.Parent != PanelSpace)
            {
                if (panelToShow.Parent != null) panelToShow.Parent.Controls.Remove(panelToShow);
                PanelSpace.Controls.Add(panelToShow);
            }

            panelToShow.Dock = DockStyle.Fill;
            panelToShow.Visible = true;
            panelToShow.BringToFront();

            // Set Checked true only for the active button, false for others
            SetChecked(CustomerProfileButton, panelToShow == CustomerProfilePanel);
            SetChecked(FinancialAnalysisButton, panelToShow == FinancialAnalysisPanel);
            SetChecked(UnitDetailsButton, panelToShow == UnitDetailsPanel);
            SetChecked(VerificationButton, panelToShow == VerificationPanel);
            SetChecked(AssessmentNotesButton, panelToShow == AssessmentNotesPanel);
            SetChecked(AuditTrailButton, panelToShow == AuditTrailPanel);
        }

        // --- Simple helpers that operate on a boolean "Checked" property only ---
        private void SetChecked(Control btn, bool value)
        {
            if (btn == null) return;
            var prop = btn.GetType().GetProperty("Checked", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (prop == null || prop.PropertyType != typeof(bool) || !prop.CanWrite) return;
            try { prop.SetValue(btn, value); }
            catch { /* non-fatal */ }
        }

        private bool GetChecked(Control btn)
        {
            if (btn == null) return false;
            var prop = btn.GetType().GetProperty("Checked", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (prop == null || prop.PropertyType != typeof(bool) || !prop.CanRead) return false;
            try
            {
                var val = prop.GetValue(btn);
                return val is bool b && b;
            }
            catch { return false; }
        }

        // Keep your existing paint handlers
        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e) { }
        private void tableLayoutPanel7_Paint(object sender, PaintEventArgs e) { }

        private void guna2HtmlLabel18_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel59_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel54_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel43_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
