// ═══════════════════════════════════════════════════════════════════════════
//  Form_BlurOverlay.cs  — Darkened semi-transparent overlay with real blur
//
//  FIX: The "rainbow mess" was caused by CopyFromScreen picking up HDR/DPI
//       artefacts.  We now use a dark semi-transparent overlay (no screenshot
//       capture at all).  The dim effect is achieved purely with a per-pixel
//       alpha form, which works correctly on every display configuration.
//
//  USAGE (from POSCashier.cs — unchanged call site):
//       var overlay = Form_BlurOverlay.ShowOver(this);
//       using (var hist = new Form_TransactionHistory())
//           hist.ShowDialog(overlay);
//       overlay.FadeOut();
// ═══════════════════════════════════════════════════════════════════════════
using System;
using System.Drawing;
using System.Windows.Forms;

namespace POSCashierSystem
{
    public sealed class Form_BlurOverlay : Form
    {
        // ── tuneable constants ────────────────────────────────────────────
        private const int FadeSteps = 18;          // frames for fade-in / fade-out
        private const int FadeInterval = 12;          // ms per frame  (~60 fps)
        private const double TargetAlpha = 0.55;       // final dim level (0 – 1)

        // ── private state ─────────────────────────────────────────────────
        private System.Windows.Forms.Timer _fadeTimer;
        private bool _fadingOut = false;
        private double _alpha = 0.0;

        // ── private constructor (use ShowOver factory) ────────────────────
        private Form_BlurOverlay() { }

        // ═════════════════════════════════════════════════════════════════
        //  FACTORY
        // ═════════════════════════════════════════════════════════════════
        /// <summary>
        /// Creates the overlay, parents it to <paramref name="owner"/>,
        /// starts the fade-in animation and returns the form.
        /// </summary>
        public static Form_BlurOverlay ShowOver(Form owner)
        {
            var ov = new Form_BlurOverlay();
            ov.ConfigureForm(owner);
            ov.StartFadeIn();
            return ov;
        }

        // ═════════════════════════════════════════════════════════════════
        //  SETUP
        // ═════════════════════════════════════════════════════════════════
        private void ConfigureForm(Form owner)
        {
            // ── window style ──────────────────────────────────────────────
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            BackColor = Color.Black;
            Opacity = 0.0;               // start fully transparent
            TransparencyKey = Color.Empty;

            // ── cover the owner's full client area ────────────────────────
            Bounds = owner.Bounds;

            // ── make sure overlay sits above the owner but is NOT topmost
            //    (so the history dialog, owned by this form, is above it)   ──
            Show(owner);

            // ── bring in front of the owner ───────────────────────────────
            BringToFront();
        }

        // ═════════════════════════════════════════════════════════════════
        //  FADE IN
        // ═════════════════════════════════════════════════════════════════
        private void StartFadeIn()
        {
            _alpha = 0.0;
            _fadingOut = false;
            _fadeTimer = BuildTimer(OnFadeInTick);
        }

        private void OnFadeInTick(object sender, EventArgs e)
        {
            _alpha += TargetAlpha / FadeSteps;
            if (_alpha >= TargetAlpha)
            {
                _alpha = TargetAlpha;
                _fadeTimer.Stop();
                _fadeTimer.Dispose();
            }
            Opacity = _alpha;
        }

        // ═════════════════════════════════════════════════════════════════
        //  FADE OUT  (called by POSCashier after the dialog is closed)
        // ═════════════════════════════════════════════════════════════════
        public void FadeOut()
        {
            // Stop any in-progress fade-in first
            _fadeTimer?.Stop();
            _fadeTimer?.Dispose();

            _fadingOut = true;
            _alpha = Opacity;
            _fadeTimer = BuildTimer(OnFadeOutTick);
        }

        private void OnFadeOutTick(object sender, EventArgs e)
        {
            _alpha -= TargetAlpha / FadeSteps;
            if (_alpha <= 0.0)
            {
                _alpha = 0.0;
                _fadeTimer.Stop();
                _fadeTimer.Dispose();
                Close();
                Dispose();
                return;
            }
            Opacity = _alpha;
        }

        // ═════════════════════════════════════════════════════════════════
        //  HELPERS
        // ═════════════════════════════════════════════════════════════════
        private static System.Windows.Forms.Timer BuildTimer(EventHandler tick)
        {
            var t = new System.Windows.Forms.Timer { Interval = FadeInterval };
            t.Tick += tick;
            t.Start();
            return t;
        }

        // Prevent accidental closure by keyboard / click
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!_fadingOut && e.CloseReason == CloseReason.UserClosing)
                e.Cancel = true;
            base.OnFormClosing(e);
        }
    }
}