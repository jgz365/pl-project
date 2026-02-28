// Program.cs
using Assessor_Eddion;
using System;
using System.Windows.Forms;

namespace YourProjectNamespace
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new staffportaleddion()); // <-- use exact form class name
        }
    }
}