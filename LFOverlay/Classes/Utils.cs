using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFOverlay.Classes
{
    class Utils
    {
        public static void SetDoubleBuffering(System.Windows.Forms.Control control, bool value)
        {
            System.Reflection.PropertyInfo controlProperty = typeof(System.Windows.Forms.Control)
                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            controlProperty.SetValue(control, value, null);
        }

        public static Process ForegroundProcess()
        {
            uint processID = 0;
            IntPtr hWnd = WinAPI.GetForegroundWindow(); // Get foreground window handle
            uint threadID = WinAPI.GetWindowThreadProcessId(hWnd, out processID); // Get PID from window handle
            Process fgProc = Process.GetProcessById(Convert.ToInt32(processID)); // Get it as a C# obj.
            // NOTE: In some rare cases ProcessID will be NULL. Handle this how you want. 
            return fgProc;
        }

        public static void FlushMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                WinAPI.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
    }
}
