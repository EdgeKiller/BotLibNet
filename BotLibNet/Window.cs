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

    public class BotWindow
    {
        private IntPtr process;

        public BotWindow(IntPtr proc)
        {
            this.process = proc;
        }

        #region GetPosition
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);
        public Point GetPosition()
        {
            Rect WinRect = new Rect();
            GetWindowRect(process, ref WinRect);
            Point position = new Point(WinRect.Left, WinRect.Top);
            return position;
        }
        #endregion

        #region SetPosition
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        public bool SetPosition(int x, int y)
        {
            return SetWindowPos(process, IntPtr.Zero, x, y, 0, 0, 5);
        }
        #endregion

        #region GetSize
        public Size GetSize()
        {
            Rect WinRect = new Rect();
            GetWindowRect(process, ref WinRect);
            int height = WinRect.Bottom - WinRect.Top;
            int width = WinRect.Right - WinRect.Left;
            Size size = new Size(width, height);
            return size;
        }
        #endregion

        #region SetSize
        [DllImport("user32.dll")]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        public bool SetSize(int width, int height)
        {
            return MoveWindow(process, GetPosition().X, GetPosition().Y, width, height, true);
        }
        #endregion

    }
}
