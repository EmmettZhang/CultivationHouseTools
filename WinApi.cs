using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CultivationHouseTools
{
    public static class WinApi
    {
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(
            IntPtr hWnd,
            uint Msg,
            IntPtr wParam,
            IntPtr lParam
        );

        public const uint WM_LBUTTONDOWN = 0x0201;
        public const uint WM_LBUTTONUP = 0x0202;


        [StructLayout(LayoutKind.Sequential)]
        public struct INPUT
        {
            public int type;
            public MOUSEINPUT mi;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [DllImport("user32.dll")]
        public static extern uint SendInput(
            uint nInputs,
            INPUT[] pInputs,
            int cbSize
        );

        public const int INPUT_MOUSE = 0;

        public const uint MOVE = 0x0001;
        public const uint ABS = 0x8000;

        public const uint DOWN = 0x0002;
        public const uint UP = 0x0004;
    }
}
