using System;
using System.Windows.Forms;

namespace AssessorDesk
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Change "AttachedFile" to whichever form you want to start first
            Application.Run(new AssessorEddionism());
        }
    }
}
