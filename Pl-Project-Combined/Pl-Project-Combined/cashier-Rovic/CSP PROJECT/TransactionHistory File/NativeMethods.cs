// NativeMethods.cs — Win32 P/Invoke helpers shared across POS forms
namespace POSCashierSystem
{
    internal static class NativeMethods
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern System.IntPtr SendMessage(System.IntPtr hWnd, int Msg, int wParam, System.IntPtr lParam);
    }
}