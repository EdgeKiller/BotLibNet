using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace BotLibNet
{
    public class Mouse
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
                        mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, x, y, 0, 0);
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

        

    }
}
