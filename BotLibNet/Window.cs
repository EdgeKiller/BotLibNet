using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
        public static Point GetPosition(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length > 0)
            {
                Process process = processes[0];
                IntPtr ptr = process.MainWindowHandle;
                Rect WinRect = new Rect();
                GetWindowRect(ptr, ref WinRect);
                Point position = new Point(WinRect.Left, WinRect.Top);
                return position;
            }
            return new Point(-1, -1);
        }
        #endregion

        #region SetPosition
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        public static bool SetPosition(string processName, int x, int y)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length > 0)
            {
                Process process = processes[0];
                IntPtr ptr = process.MainWindowHandle;
                return SetWindowPos(ptr, IntPtr.Zero, x, y, 0, 0, 5);
            }
            return false;

        }
        #endregion

        #region GetSize
        public static Size GetSize(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length > 0)
            {
                Process process = processes[0];
                IntPtr ptr = process.MainWindowHandle;
                Rect WinRect = new Rect();
                GetWindowRect(ptr, ref WinRect);
                int height = WinRect.Bottom - WinRect.Top;
                int width = WinRect.Right - WinRect.Left;
                Size size = new Size(width, height);
                return size;
            }
            return new Size(-1, -1);
        }
        #endregion

        #region SetSize
        [DllImport("user32.dll")]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        public static bool SetSize(string processName, int width, int height)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length > 0){
                Process process = processes[0];
                IntPtr ptr = process.MainWindowHandle;
                return MoveWindow(ptr, GetPosition(processName).X, GetPosition(processName).Y, width, height, true);
            }
            return false;
        }
        #endregion

        #region Exist
        public static bool Exist(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length > 0 ? true : false;
        }
        #endregion

    }
}
