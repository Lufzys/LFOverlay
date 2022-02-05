using LFOverlay.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LFOverlay
{
    internal static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            WinAPI.SetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
