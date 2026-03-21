using System.Windows.Forms;

namespace inventory_ni_Percie
{
    /// <summary>
    /// A borderless, semi-transparent black form that sits over Form1
    /// to produce the dim/blur effect behind modal dialogs.
    /// Usage:
    ///   using (var overlay = new FormBlurOverlay(ownerForm))
    ///   {
    ///       overlay.Show(ownerForm);
    ///       // show your modal here
    ///   } // overlay auto-disposed + closed
    /// </summary>
    public class FormBlurOverlay : Form
    {
        public FormBlurOverlay(Form ownerForm)
        {
            // ── Window chrome ────────────────────────────────────────────────
            FormBorderStyle = FormBorderStyle.None;
            BackColor = System.Drawing.Color.Black;
            Opacity = 0.45;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;

            // ── Size + position exactly over the owner form ──────────────────
            Bounds = ownerForm.Bounds;

            // Keep overlay on top but behind the modal
            TopMost = false;

            // ── Click-through: clicks pass to modal above, not to this form ──
            // We do NOT set TopMost = true here; the modal .ShowDialog() handles z-order.
        }

        // Prevent the overlay itself from receiving focus / click events
        protected override void WndProc(ref Message m)
        {
            const int WM_MOUSEACTIVATE = 0x0021;
            const int MA_NOACTIVATE = 3;

            if (m.Msg == WM_MOUSEACTIVATE)
            {
                m.Result = (IntPtr)MA_NOACTIVATE;
                return;
            }
            base.WndProc(ref m);
        }
    }
}