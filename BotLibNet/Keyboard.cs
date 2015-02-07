using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BotLibNet
{
    public class BotKeyboard
    {
        #region Keys
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public static void SendKeyStroke(string processName, Keys key)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length > 0)
            {
                Process process = processes[0];
                IntPtr ptr = process.MainWindowHandle;
                const uint WM_KEYDOWN = 0x100;
                const uint WM_KEYUP = 0x101;
                int k = (int)key;
                SendMessage(ptr, WM_KEYDOWN, ((IntPtr)k), (IntPtr)0);
                SendMessage(ptr, WM_KEYUP, ((IntPtr)k), (IntPtr)0);
            }

        }
        #endregion

    }
}
