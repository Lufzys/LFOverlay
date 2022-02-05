using LFOverlay.Classes.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LFOverlay.Classes
{
    class WinAPI
    {
        [DllImport("user32.dll")]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetProcessDPIAware();

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hwnd, out Structs.RECT lpRect);

        [DllImport("kernel32.dll")]
        public static extern bool SetProcessWorkingSetSize(IntPtr proc, int min, int max);
    }
}
