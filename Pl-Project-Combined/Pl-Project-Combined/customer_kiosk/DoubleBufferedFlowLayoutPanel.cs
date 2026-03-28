using System;
using System.Drawing;
using System.Windows.Forms;

namespace customer_kiosk
{
    // FlowLayoutPanel with double buffering and simple BeginUpdate/EndUpdate helpers
    public class DoubleBufferedFlowLayoutPanel : FlowLayoutPanel
    {
        private bool isDragging = false;
        private bool isMouseDown = false;
        private Point lastPoint;
        private const int DragThreshold = 6;
        public DoubleBufferedFlowLayoutPanel()
        {
            // enable double buffering to reduce flicker
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();
            this.DoubleBuffered = true;

            // basic drag-to-scroll support
            this.MouseDown += (s, e) => OnPanelMouseDown(e.Location);
            this.MouseMove += (s, e) => OnPanelMouseMove(e.Location);
            this.MouseUp += (s, e) => OnPanelMouseUp(e.Location);

            this.ControlAdded += (s, e) => AttachChildHandlers(e.Control);
            this.ControlRemoved += (s, e) => DetachChildHandlers(e.Control);
        }

        // Simple helpers to suspend/resume layout when adding many controls
        public void BeginUpdate()
        {
            this.SuspendLayout();
        }

        public void EndUpdate()
        {
            this.ResumeLayout();
            this.Refresh();
        }

        private void AttachChildHandlers(Control? c)
        {
            if (c == null) return;
            try
            {
                c.MouseDown += Child_MouseDown;
                c.MouseMove += Child_MouseMove;
                c.MouseUp += Child_MouseUp;
                c.ControlAdded += Child_ControlAdded;
                c.ControlRemoved += Child_ControlRemoved;
            }
            catch { }

            // attach recursively to descendants so touches on inner controls are captured
            foreach (Control child in c.Controls)
            {
                AttachChildHandlers(child);
            }
        }

        private void DetachChildHandlers(Control? c)
        {
            if (c == null) return;
            try
            {
                c.MouseDown -= Child_MouseDown;
                c.MouseMove -= Child_MouseMove;
                c.MouseUp -= Child_MouseUp;
                c.ControlAdded -= Child_ControlAdded;
                c.ControlRemoved -= Child_ControlRemoved;
            }
            catch { }

            foreach (Control child in c.Controls)
            {
                DetachChildHandlers(child);
            }
        }

        private void Child_ControlAdded(object? sender, ControlEventArgs e)
        {
            if (e?.Control != null) AttachChildHandlers(e.Control);
        }

        private void Child_ControlRemoved(object? sender, ControlEventArgs e)
        {
            if (e?.Control != null) DetachChildHandlers(e.Control);
        }

        private void Child_MouseDown(object? sender, MouseEventArgs e)
        {
            if (sender is Control c)
            {
                var pt = this.PointToClient(c.PointToScreen(e.Location));
                OnPanelMouseDown(pt);
            }
        }

        private void Child_MouseMove(object? sender, MouseEventArgs e)
        {
            if (sender is Control c)
            {
                var pt = this.PointToClient(c.PointToScreen(e.Location));
                OnPanelMouseMove(pt);
            }
        }

        private void Child_MouseUp(object? sender, MouseEventArgs e)
        {
            if (sender is Control c)
            {
                var pt = this.PointToClient(c.PointToScreen(e.Location));
                OnPanelMouseUp(pt);
            }
        }

        private void OnPanelMouseDown(Point location)
        {
            isMouseDown = true;
            isDragging = false;
            lastPoint = location;
            try { this.Capture = true; } catch { }
        }

        private void OnPanelMouseMove(Point location)
        {
            if (!isMouseDown) return;

            var dxTotal = location.X - lastPoint.X;
            var dyTotal = location.Y - lastPoint.Y;

            if (!isDragging)
            {
                if (Math.Abs(dxTotal) >= DragThreshold || Math.Abs(dyTotal) >= DragThreshold)
                {
                    isDragging = true;
                }
                else
                {
                    return;
                }
            }

            var dx = location.X - lastPoint.X;
            var dy = location.Y - lastPoint.Y;
            // invert because AutoScrollPosition uses negative coordinates
            var newX = -this.AutoScrollPosition.X - dx;
            var newY = -this.AutoScrollPosition.Y - dy;
            this.AutoScrollPosition = new Point(newX, newY);
            lastPoint = location;
        }

        private void OnPanelMouseUp(Point location)
        {
            isMouseDown = false;
            isDragging = false;
            try { this.Capture = false; } catch { }
        }
    }
}
