using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace BotLibNet
{
    public class Picture
    {
        public static Color GetPixelColor(int x, int y)
        {
            Color pixelColor;
            Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics graphics = Graphics.FromImage(printscreen as Image);
            graphics.CopyFromScreen(x, y, 0, 0, printscreen.Size);
            pixelColor = printscreen.GetPixel(0, 0);
            return pixelColor;
        }
    }
}
