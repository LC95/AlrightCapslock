using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AlrightCapslock
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct POINT
    {
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    internal static class WindowMoveHelper
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out POINT lpPoint);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", EntryPoint = "MoveWindow")]
        static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        private static bool CAPS = false;
        private static POINT ORI_POINT;
        private static RECT ORI_RECT;

        public static void Begin()
        {
            CAPS = true;
            //找到活动窗口
            IntPtr handle = GetForegroundWindow();
            if (handle == IntPtr.Zero)
            {
                return;
            }

            //记录当前窗口的鼠标位置和宽高
            GetCursorPos(out ORI_POINT);
            GetWindowRect(handle, out ORI_RECT);
            int width = ORI_RECT.right - ORI_RECT.left;
            int height = ORI_RECT.bottom - ORI_RECT.top;

            //计算鼠标移动后窗口的新位置
            while (CAPS)
            {
                GetCursorPos(out var nowCursor);
                var finalX = ORI_RECT.left + nowCursor.X - ORI_POINT.X;
                var finalY = ORI_RECT.top + nowCursor.Y - ORI_POINT.Y;
                MoveWindow(handle, finalX, finalY, width, height, true);
            }
        }
        public static void Stop()
        {
            CAPS = false;
        }
    }
}
