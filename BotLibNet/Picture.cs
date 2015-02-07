using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace BotLibNet
{
    public class BotImage
    {
        public static Color GetPixelColor(string processName, Point pos)
        {
            Color pixelColor;
            Point windowPos = BotWindow.GetPosition(processName);
            Point newPos = new Point(windowPos.X + pos.X, windowPos.Y + pos.Y);
            Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics graphics = Graphics.FromImage(printscreen as Image);
            graphics.CopyFromScreen(newPos.X, newPos.Y, 0, 0, printscreen.Size);
            pixelColor = printscreen.GetPixel(0, 0);
            return pixelColor;
        }

        //public static Bitmap CaptureRegion()
    }
}
