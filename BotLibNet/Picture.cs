﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace BotLibNet
{
    public class BotImage
    {
        private IntPtr process;

        public BotImage(IntPtr proc)
        {
            this.process = proc;
        }

        public Color GetPixelColor(Point pos)
        {
            Color pixelColor = new Color();
            Bitmap image = new Bitmap(1, 1);
            RECT rc;
            GetWindowRect(process, out rc);
            Bitmap bmp = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
            Graphics gfxBmp = Graphics.FromImage(bmp);
            IntPtr hdcBitmap = gfxBmp.GetHdc();
            PrintWindow(process, hdcBitmap, 0);
            gfxBmp.ReleaseHdc(hdcBitmap);
            gfxBmp.Dispose();
            image = bmp.Clone(new Rectangle(pos.X, pos.Y, 1, 1), PixelFormat.Format32bppArgb);
            pixelColor = image.GetPixel(0, 0);
            return pixelColor;
        }

        public Bitmap CaptureRegion(Rectangle region)
        {
            Bitmap image = new Bitmap(region.Width, region.Height);
            RECT rc;
            GetWindowRect(process, out rc);
            Bitmap bmp = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
            Graphics gfxBmp = Graphics.FromImage(bmp);
            IntPtr hdcBitmap = gfxBmp.GetHdc();
            PrintWindow(process, hdcBitmap, 0);
            gfxBmp.ReleaseHdc(hdcBitmap);
            gfxBmp.Dispose();
            image = bmp.Clone(new Rectangle(region.X, region.Y, region.Width, region.Height), PixelFormat.Format32bppArgb);
            return image;
        }

        #region GetScreenImage

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);
        public Bitmap GetWindowImage()
        {
            RECT rc;
            GetWindowRect(process, out rc);
            Bitmap bmp = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
            Graphics gfxBmp = Graphics.FromImage(bmp);
            IntPtr hdcBitmap = gfxBmp.GetHdc();
            PrintWindow(process, hdcBitmap, 0);
            gfxBmp.ReleaseHdc(hdcBitmap);
            gfxBmp.Dispose();
            return bmp;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            private int _Left;
            private int _Top;
            private int _Right;
            private int _Bottom;

            public RECT(RECT Rectangle)
                : this(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom)
            {
            }
            public RECT(int Left, int Top, int Right, int Bottom)
            {
                _Left = Left;
                _Top = Top;
                _Right = Right;
                _Bottom = Bottom;
            }

            public int X
            {
                get { return _Left; }
                set { _Left = value; }
            }
            public int Y
            {
                get { return _Top; }
                set { _Top = value; }
            }
            public int Left
            {
                get { return _Left; }
                set { _Left = value; }
            }
            public int Top
            {
                get { return _Top; }
                set { _Top = value; }
            }
            public int Right
            {
                get { return _Right; }
                set { _Right = value; }
            }
            public int Bottom
            {
                get { return _Bottom; }
                set { _Bottom = value; }
            }
            public int Height
            {
                get { return _Bottom - _Top; }
                set { _Bottom = value + _Top; }
            }
            public int Width
            {
                get { return _Right - _Left; }
                set { _Right = value + _Left; }
            }
            public Point Location
            {
                get { return new Point(Left, Top); }
                set
                {
                    _Left = value.X;
                    _Top = value.Y;
                }
            }
            public Size Size
            {
                get { return new Size(Width, Height); }
                set
                {
                    _Right = value.Width + _Left;
                    _Bottom = value.Height + _Top;
                }
            }

            public static implicit operator Rectangle(RECT Rectangle)
            {
                return new Rectangle(Rectangle.Left, Rectangle.Top, Rectangle.Width, Rectangle.Height);
            }
            public static implicit operator RECT(Rectangle Rectangle)
            {
                return new RECT(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom);
            }
            public static bool operator ==(RECT Rectangle1, RECT Rectangle2)
            {
                return Rectangle1.Equals(Rectangle2);
            }
            public static bool operator !=(RECT Rectangle1, RECT Rectangle2)
            {
                return !Rectangle1.Equals(Rectangle2);
            }

            public override string ToString()
            {
                return "{Left: " + _Left + "; " + "Top: " + _Top + "; Right: " + _Right + "; Bottom: " + _Bottom + "}";
            }

            public override int GetHashCode()
            {
                return ToString().GetHashCode();
            }

            public bool Equals(RECT Rectangle)
            {
                return Rectangle.Left == _Left && Rectangle.Top == _Top && Rectangle.Right == _Right && Rectangle.Bottom == _Bottom;
            }

            public override bool Equals(object Object)
            {
                if (Object is RECT)
                {
                    return Equals((RECT)Object);
                }
                else if (Object is Rectangle)
                {
                    return Equals(new RECT((Rectangle)Object));
                }

                return false;
            }
        }
        #endregion

    }
}
