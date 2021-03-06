﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BotLibNet
{
    public class BotKeyboard
    {
        private IntPtr process;

        public BotKeyboard(IntPtr proc)
        {
            this.process = proc;
        }

        #region Keys
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        public void SendKeyStroke(Keys key)
        {
            const uint WM_KEYDOWN = 0x100;
            const uint WM_KEYUP = 0x101;
            int k = (int)key;
            SendMessage(process, WM_KEYDOWN, ((IntPtr)k), (IntPtr)0);
            SendMessage(process, WM_KEYUP, ((IntPtr)k), (IntPtr)0);
        }
        #endregion
    }
}
