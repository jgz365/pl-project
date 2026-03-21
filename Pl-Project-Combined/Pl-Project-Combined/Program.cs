using inventory_ni_Percie;

namespace Pl_Project_Combined
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}