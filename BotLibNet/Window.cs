using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BotLibNet
{
    public struct Rect
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }
    }

    public class Window
    {

        #region GetPosition
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);
        public static Rect GetPosition(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            Process lol = processes[0];
            IntPtr ptr = lol.MainWindowHandle;
            Rect WinRect = new Rect();
            GetWindowRect(ptr, ref WinRect);
            return WinRect;
        }
        #endregion

        #region SetPosition
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        public static bool SetPosition(string processName, int x, int y)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            Process lol = processes[0];
            IntPtr ptr = lol.MainWindowHandle;
            return SetWindowPos(ptr, IntPtr.Zero, x, y, 0, 0, 5);
        }
        #endregion



    }
}
