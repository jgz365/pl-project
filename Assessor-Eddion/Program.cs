csharp ..\AssessorDesk\Program.cs
using System;
using System.Windows.Forms;

namespace AssessorDesk
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
-            Application.Run(new AssessorEddionDerek());
+            Application.Run(new Form1());
        }
    }
}