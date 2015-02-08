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
            Bitmap pixel = new Bitmap(1, 1);
            Graphics graphics = Graphics.FromImage(pixel as Image);
            graphics.CopyFromScreen(newPos.X, newPos.Y, 0, 0, pixel.Size);
            pixelColor = pixel.GetPixel(0, 0);
            return pixelColor;
        }

        public static Bitmap CaptureRegion(string processName, Rectangle region)
        {
            Bitmap image = new Bitmap(region.Width, region.Height);
            Point windowPos = BotWindow.GetPosition(processName);
            Point newPos = new Point(windowPos.X + region.X, windowPos.Y + region.Y);
            Graphics graphics = Graphics.FromImage(image as Image);
            graphics.CopyFromScreen(newPos.X, newPos.Y, 0, 0, new Size(region.Width, region.Height));
            return image;
        }
    }
}
