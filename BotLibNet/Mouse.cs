using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace BotLibNet
{
    public class Mouse
    {
        public static Point GetPosition()
        {
            Point mousePosition = Cursor.Position;
            return mousePosition;
        }

        public static void SetPosition(Point position)
        {
            Cursor.Position = position;
        }


    }
}
