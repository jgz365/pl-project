using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace customer_kiosk
{
    internal sealed class ResponsiveScaler
    {
        private const float DefaultMinimumFontSize = 9f;

        private readonly Control root;
        private readonly Size designSize;
        private readonly Dictionary<Control, Snapshot> snapshots = new();
        private readonly float minimumFontSize;

        private readonly record struct Snapshot(
            Rectangle Bounds,
            Padding Margin,
            Padding Padding,
            float FontSize,
            FontStyle FontStyle,
            string FontFamily,
            DockStyle DockStyle);

        public ResponsiveScaler(Control root, Size designSize, float minimumFontSize = DefaultMinimumFontSize)
        {
            this.root = root;
            this.designSize = new Size(Math.Max(1, designSize.Width), Math.Max(1, designSize.Height));
            this.minimumFontSize = Math.Max(6f, minimumFontSize);
            CaptureChildren(root);
        }

        public void Apply(Size currentSize)
        {
            if (currentSize.Width <= 0 || currentSize.Height <= 0)
            {
                return;
            }

            float scaleX = currentSize.Width / (float)designSize.Width;
            float scaleY = currentSize.Height / (float)designSize.Height;
            float fontScale = Math.Min(scaleX, scaleY);

            root.SuspendLayout();
            foreach (var entry in snapshots)
            {
                Control control = entry.Key;
                if (control.IsDisposed)
                {
                    continue;
                }

                Snapshot snapshot = entry.Value;

                if (snapshot.DockStyle == DockStyle.None)
                {
                    control.Left = (int)Math.Round(snapshot.Bounds.Left * scaleX);
                    control.Top = (int)Math.Round(snapshot.Bounds.Top * scaleY);
                    control.Width = Math.Max(1, (int)Math.Round(snapshot.Bounds.Width * scaleX));
                    control.Height = Math.Max(1, (int)Math.Round(snapshot.Bounds.Height * scaleY));
                }

                control.Margin = new Padding(
                    (int)Math.Round(snapshot.Margin.Left * scaleX),
                    (int)Math.Round(snapshot.Margin.Top * scaleY),
                    (int)Math.Round(snapshot.Margin.Right * scaleX),
                    (int)Math.Round(snapshot.Margin.Bottom * scaleY));

                control.Padding = new Padding(
                    (int)Math.Round(snapshot.Padding.Left * scaleX),
                    (int)Math.Round(snapshot.Padding.Top * scaleY),
                    (int)Math.Round(snapshot.Padding.Right * scaleX),
                    (int)Math.Round(snapshot.Padding.Bottom * scaleY));

                float newFontSize = Math.Max(minimumFontSize, snapshot.FontSize * fontScale);
                if (Math.Abs(control.Font.Size - newFontSize) > 0.1f)
                {
                    control.Font = new Font(snapshot.FontFamily, newFontSize, snapshot.FontStyle);
                }
            }

            root.ResumeLayout(true);
        }

        private void CaptureChildren(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                if (!snapshots.ContainsKey(child))
                {
                    snapshots[child] = new Snapshot(
                        child.Bounds,
                        child.Margin,
                        child.Padding,
                        child.Font.Size,
                        child.Font.Style,
                        child.Font.FontFamily.Name,
                        child.Dock);
                }

                if (child.HasChildren)
                {
                    CaptureChildren(child);
                }
            }
        }
    }
}
