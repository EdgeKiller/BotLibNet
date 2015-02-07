using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;

namespace BotLibNet
{

    public enum WButton
    {
        Left,
        Right,
        Middle
    }

    public class BotMouse
    {
        #region GetPosition
        public static Point GetPosition()
        {
            Point mousePosition = Cursor.Position;
            return mousePosition;
        }
        #endregion

        #region SetPosition
        public static bool SetPosition(int x, int y)
        {
            try
            {
                Cursor.Position = new Point(x, y);
            }
            catch 
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Click
        public class Click
        {
            [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            private static extern void mouse_event(uint dwFlags, int dx, int dy, uint cButtons, uint dwExtraInfo);
            private const int MOUSEEVENTF_LEFTDOWN = 0x02;
            private const int MOUSEEVENTF_LEFTUP = 0x04;
            private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
            private const int MOUSEEVENTF_RIGHTUP = 0x10;
            private const int MOUSEEVENTF_MIDDLEDOWN = 0x20;
            private const int MOUSEEVENTF_MIDDLEUP = 0x40;

            public class Left
            {
                public static bool Single()
                {
                    try
                    {
                        
                        int x = GetPosition().X;
                        int y = GetPosition().Y;
                        mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
                        mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);
                        
                    }
                    catch
                    {
                        return false;
                    }
                    return true;
                }

                public static bool Double(int millisecond = 100)
                {
                    try
                    {
                        int x = GetPosition().X;
                        int y = GetPosition().Y;
                        mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, x, y, 0, 0);
                        Thread.Sleep(millisecond);
                        mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, x, y, 0, 0);
                    }
                    catch
                    {
                        return false;
                    }
                    return true;
                }

                public static bool Stay(int millisecond)
                {
                    try
                    {
                        int x = GetPosition().X;
                        int y = GetPosition().Y;
                        mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
                        Thread.Sleep(millisecond);
                        mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);
                    }
                    catch
                    {
                        return false;
                    }
                    return true;
                }
            }

            public class Right
            {
                public static bool Single()
                {
                    try
                    {
                        int x = GetPosition().X;
                        int y = GetPosition().Y;
                        mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, x, y, 0, 0);
                    }
                    catch
                    {
                        return false;
                    }
                    return true;
                }

                public static bool Double(int millisecond = 100)
                {
                    try
                    {
                        int x = GetPosition().X;
                        int y = GetPosition().Y;
                        mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, x, y, 0, 0);
                        Thread.Sleep(millisecond);
                        mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, x, y, 0, 0);
                    }
                    catch
                    {
                        return false;
                    }
                    return true;
                }

                public static bool Stay(int millisecond)
                {
                    try
                    {
                        int x = GetPosition().X;
                        int y = GetPosition().Y;
                        mouse_event(MOUSEEVENTF_RIGHTDOWN, x, y, 0, 0);
                        Thread.Sleep(millisecond);
                        mouse_event(MOUSEEVENTF_RIGHTUP, x, y, 0, 0);
                    }
                    catch
                    {
                        return false;
                    }
                    return true;
                }
            }

        }
        
        #endregion

        #region SendClick
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private enum WMessages : int
        {
            WM_LBUTTONDOWN = 0x201, //Left mousebutton down
            WM_LBUTTONUP = 0x202,  //Left mousebutton up
            WM_LBUTTONDBLCLK = 0x203, //Left mousebutton doubleclick
            WM_RBUTTONDOWN = 0x204, //Right mousebutton down
            WM_RBUTTONUP = 0x205,   //Right mousebutton up
            WM_RBUTTONDBLCLK = 0x206, //Right mousebutton doubleclick
            WM_MBUTTONDOWN = 0x207, //Middle mousebutton down
            WM_MBUTTONUP = 0x208, //Middle mousebutton up
            WM_MBUTTONDBLCLK = 0x209, //Middle mousebutton doubleclick
        }

        private static void _SendMessage(IntPtr handle, int Msg, int wParam, int lParam)
        {
            SendMessage(handle, Msg, wParam, lParam);
        }

        private static int MakeLParam(int LoWord, int HiWord)
        {
            return ((HiWord << 16) | (LoWord & 0xffff));
        }

        public static void SendClick(string processName, WButton button, Point pos, bool doubleclick)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            Process process = processes[0];
            IntPtr hWnd = process.MainWindowHandle;
            int LParam = MakeLParam(pos.X, pos.Y), btnDown = 0, btnUp = 0;
            switch (button)
            {
                case WButton.Left:
                    btnDown = (int)WMessages.WM_LBUTTONDOWN;
                    btnUp = (int)WMessages.WM_LBUTTONUP;
                    break;
                case WButton.Right:
                    btnDown = (int)WMessages.WM_RBUTTONDOWN;
                    btnUp = (int)WMessages.WM_RBUTTONUP;
                    break;
                case WButton.Middle:
                    btnDown = (int)WMessages.WM_MBUTTONDOWN;
                    btnUp = (int)WMessages.WM_MBUTTONUP;
                    break;
            }
            if (doubleclick)
            {
                _SendMessage(hWnd, btnDown, 0, LParam);
                _SendMessage(hWnd, btnUp, 0, LParam);
                _SendMessage(hWnd, btnDown, 0, LParam);
                _SendMessage(hWnd, btnUp, 0, LParam);
            }
            else
            {
                _SendMessage(hWnd, btnDown, 0, LParam);
                _SendMessage(hWnd, btnUp, 0, LParam);
            }

        }
        #endregion

    }
}
