using System.Drawing;
using System.Windows.Forms;

namespace inventory_ni_Percie
{
    /// <summary>
    /// Clean white color table for ContextMenuStrip used in UC_Users filter dropdown.
    /// Kept in its own file so it is visible to the entire namespace without
    /// needing extra using directives.
    /// </summary>
    internal class CleanMenuColors : ProfessionalColorTable
    {
        public override Color MenuItemSelected => Color.FromArgb(239, 246, 255);
        public override Color MenuItemBorder => Color.FromArgb(226, 232, 240);
        public override Color MenuBorder => Color.FromArgb(226, 232, 240);
        public override Color ToolStripDropDownBackground => Color.White;
        public override Color ImageMarginGradientBegin => Color.White;
        public override Color ImageMarginGradientMiddle => Color.White;
        public override Color ImageMarginGradientEnd => Color.White;
        public override Color SeparatorDark => Color.FromArgb(226, 232, 240);
        public override Color SeparatorLight => Color.White;
        public override Color MenuItemSelectedGradientBegin => Color.FromArgb(239, 246, 255);
        public override Color MenuItemSelectedGradientEnd => Color.FromArgb(239, 246, 255);
    }
}