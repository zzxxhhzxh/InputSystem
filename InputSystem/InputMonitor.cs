using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Reflection;


namespace InputSystem
{
    public static class InputMonitor
    {
        private const int WH_MOUSE = 14;
        private const int WH_KEYBOARD = 13;
        private const int WM_LBUTTONDOWN = 0x201;
        private const int WM_RBUTTONDOWN = 0x204;
        private const int WM_MBUTTONDOWN = 0x207;
        private const int WM_LBUTTONUP = 0x202;
        private const int WM_RBUTTONUP = 0x205;
        private const int WM_MBUTTONUP = 0x208;
        private const int WM_MOUSEWHEEL = 0x020A;
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;
        private const int WM_SYSKEYDOWN = 0x104;
        private const int WM_SYSKEYUP = 0x105;

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, int dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern int UnhookWindowsHookEx(int idHook);


        private delegate int HookProc(int nCode, int wParam, IntPtr lParam);
        private static HookProc mouseDelegate;
        private static HookProc keyDelegate;
        private static int mouseHookHandle;
        private static int keyHookHandle;
        private static int preX;
        private static int preY;

        public static event MouseEventHandler MouseMove;
        public static event MouseEventHandler MouseWheel;
        public static event MouseEventHandler MouseDown;
        public static event MouseEventHandler MouseUp;
        public static event KeyEventHandler KeyUp;
        public static event KeyEventHandler KeyDown;


        [StructLayout(LayoutKind.Sequential)]
        private struct Point
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MouseLLHookStruct
        {
            public Point Point;
            public int MouseData;
            public int Flags;
            public int Time;
            public int ExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KeyboardHookStruct
        {
            public int VirtualKeyCode;
            public int ScanCode;
            public int Flags;
            public int Time;
            public int ExtraInfo;
        }

        private static int MouseHookProc(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var mouseHookStruct = (MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseLLHookStruct));
                var button = MouseButtons.None;
                short mouseDelta = 0;
                int clicks = 0;
                bool isMouseDown = false;
                bool isMouseUp = false;

                switch (wParam)
                {
                    case WM_LBUTTONDOWN:
                        isMouseDown = true;
                        button = MouseButtons.Left;
                        clicks = 1;
                        break;
                    case WM_LBUTTONUP:
                        isMouseUp = true;
                        button = MouseButtons.Left;
                        clicks = 1;
                        break;
                    case WM_MBUTTONDOWN:
                        isMouseDown = true;
                        button = MouseButtons.Middle;
                        clicks = 1;
                        break;
                    case WM_MBUTTONUP:
                        isMouseUp = true;
                        button = MouseButtons.Middle;
                        clicks = 1;
                        break;
                    case WM_RBUTTONDOWN:
                        isMouseDown = true;
                        button = MouseButtons.Right;
                        clicks = 1;
                        break;
                    case WM_RBUTTONUP:
                        isMouseUp = true;
                        button = MouseButtons.Right;
                        clicks = 1;
                        break;
                    case WM_MOUSEWHEEL:
                        mouseDelta = (short)((mouseHookStruct.MouseData >> 16) & 0xffff);
                        break;
                }
                var e = new MouseEventArgs(button, clicks, mouseHookStruct.Point.X, mouseHookStruct.Point.Y, mouseDelta);
                if (MouseUp != null && isMouseUp)
                    MouseUp.Invoke(null, e);
                if (MouseDown != null && isMouseDown)
                    MouseDown.Invoke(null, e);
                if (MouseWheel != null && mouseDelta != 0)
                    MouseWheel.Invoke(null, e);
                if (MouseMove != null && (preX != mouseHookStruct.Point.X || preY != mouseHookStruct.Point.Y))
                {
                    preX = mouseHookStruct.Point.X;
                    preY = mouseHookStruct.Point.Y;
                    MouseMove?.Invoke(null, e);
                }
            }
            return CallNextHookEx(mouseHookHandle, nCode, wParam, lParam);
        }

        private static int KeyHookProc(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var keyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
                var e = new KeyEventArgs((Keys)keyboardHookStruct.VirtualKeyCode);
                if (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN)
                    KeyDown?.Invoke(null, e);
                if (wParam == WM_KEYUP || wParam == WM_SYSKEYUP)
                    KeyUp?.Invoke(null, e);
            }
            return CallNextHookEx(keyHookHandle, nCode, wParam, lParam);
        }

        public static void SubscribeGlobalEvents()
        {
            mouseDelegate = MouseHookProc;
            keyDelegate = KeyHookProc;
            mouseHookHandle = SetWindowsHookEx(WH_MOUSE, mouseDelegate, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
            keyHookHandle = SetWindowsHookEx(WH_KEYBOARD, keyDelegate, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
        }

        public static void UnsubscribeGlobalEvents()
        {
            UnhookWindowsHookEx(mouseHookHandle);
            UnhookWindowsHookEx(keyHookHandle);
            mouseHookHandle = 0;
            keyHookHandle = 0;
            mouseDelegate = null;
            keyDelegate = null;
        }
    }
}