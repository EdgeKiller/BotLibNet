using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLibNet
{
    public class BotProcess
    {
        public BotKeyboard keyboard;
        public BotMouse mouse;

        private IntPtr process;

        public BotProcess(string processName)
        {
            Process[] processesList = Process.GetProcessesByName(processName);
            if (processesList.Length > 0)
            {
                process = processesList[0].MainWindowHandle;
                keyboard = new BotKeyboard(process);
                mouse = new BotMouse(process);
            }
            
        }
    }
}