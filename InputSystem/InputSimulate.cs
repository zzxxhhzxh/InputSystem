using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms;


namespace InputSystem
{
    public static class InputSimulate
    {
        [StructLayout(LayoutKind.Explicit)]
        struct INPUT
        {
            [FieldOffset(0)]
            public int Type;
            [FieldOffset(4)]
            public MOUSEINPUT MouseInput;
            [FieldOffset(4)]
            public KEYBDINPUT KeyboardInput;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct MOUSEINPUT
        {
            public int dX;
            public int dY;
            public int mouseData;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        };

        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT
        {
            public short wVk;
            public short wScan;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        };

        [Flags]
        enum SendMouseInputFlags
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            Wheel = 0x0800,
            Absolute = 0x8000
        };

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        static extern int GetSystemMetrics(int nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int MapVirtualKey(int nVirtKey, int nMapType);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int SendInput(int nInputs, ref INPUT mi, int cbSize);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern short VkKeyScan(char ch);

        [DllImport("kernel32.dll")]
        static extern uint GetTickCount();


        public static void MouseDown(MouseButtons mButton)
        {
            switch (mButton)
            {
                case MouseButtons.Left:
                    SendMouseInput(0, 0, 0, SendMouseInputFlags.LeftDown);
                    break;
                case MouseButtons.Right:
                    SendMouseInput(0, 0, 0, SendMouseInputFlags.RightDown);
                    break;
                case MouseButtons.Middle:
                    SendMouseInput(0, 0, 0, SendMouseInputFlags.MiddleDown);
                    break;
            }
        }

        public static void MouseUp(MouseButtons mButton)
        {
            switch (mButton)
            {
                case MouseButtons.Left:
                    SendMouseInput(0, 0, 0, SendMouseInputFlags.LeftUp);
                    break;
                case MouseButtons.Right:
                    SendMouseInput(0, 0, 0, SendMouseInputFlags.RightUp);
                    break;
                case MouseButtons.Middle:
                    SendMouseInput(0, 0, 0, SendMouseInputFlags.MiddleUp);
                    break;
            }
        }

        public static void MouseMove(Point point)
        {
            SendMouseInput(point.X, point.Y, 0, SendMouseInputFlags.Move | SendMouseInputFlags.Absolute);
        }

        public static void MouseScroll(int lines)
        {
            SendMouseInput(0, 0, 120 * lines, SendMouseInputFlags.Wheel);
        }

        public static void Delay(int ms)
        {
            uint time = GetTickCount() + (uint)ms;
            while (GetTickCount() < time)
            {
                Application.DoEvents();
            }
        }

        public static void KeyDown(Keys key)
        {
            SendKeyboardInput(key, true);
        }

        public static void KeyUp(Keys key)
        {
            SendKeyboardInput(key, false);
        }

        public static void KeyModifierPress(Keys key, Keys[] mKeys)
        {
            foreach (Keys mKey in mKeys)
                SendKeyboardInput(mKey, true);
            SendKeyboardInput(key, true);
            SendKeyboardInput(key, false);
            foreach (Keys mKey in mKeys.Reverse())
                SendKeyboardInput(mKey, false);
        }

        public static void KeyType(string text)
        {
            foreach (char c in text)
            {
                Keys key = (Keys)VkKeyScan(c);
                if (c > 64 && c < 91)
                {
                    SendKeyboardInput(Keys.ShiftKey, true);
                    SendKeyboardInput(key, true);
                    SendKeyboardInput(key, false);
                    SendKeyboardInput(Keys.ShiftKey, false);
                }
                else
                {
                    SendKeyboardInput(key, true);
                    SendKeyboardInput(key, false);
                }
            }
        }

        [PermissionSet(SecurityAction.Assert, Name = "FullTrust")]
        private static void SendMouseInput(int x, int y, int data, SendMouseInputFlags flags)
        {
            var permissions = new PermissionSet(PermissionState.Unrestricted);
            permissions.Demand();
            int dX = x, dY = y;
            if (((int)flags & (int)SendMouseInputFlags.Absolute) != 0)
            {
                int vScreenWidth = GetSystemMetrics(78); // SMCxvirtualscreen
                int vScreenHeight = GetSystemMetrics(79); // SMCyvirtualscreen
                int vScreenLeft = GetSystemMetrics(76); // SMXvirtualscreen
                int vScreenTop = GetSystemMetrics(77); // SMYvirtualscreen
                dX = (x - vScreenLeft) * 65536 / vScreenWidth + 65536 / (vScreenWidth * 2);
                dY = (y - vScreenTop) * 65536 / vScreenHeight + 65536 / (vScreenHeight * 2);
            }
            var input = new INPUT()
            {
                Type = 0,
                MouseInput = new MOUSEINPUT()
                {
                    dX = dX,
                    dY = dY,
                    mouseData = data,
                    dwFlags = (int)flags,
                    time = 0,
                    dwExtraInfo = new IntPtr(0)
                }
            };
            SendInput(1, ref input, Marshal.SizeOf(input));
        }

        [PermissionSet(SecurityAction.Assert, Name = "FullTrust")]
        private static void SendKeyboardInput(Keys key, bool press)
        {
            PermissionSet permissions = new PermissionSet(PermissionState.Unrestricted);
            permissions.Demand();

            short wVk = (short)key;
            short wScan = (short)MapVirtualKey(wVk, 0);
            int dwFlags = 0;
            if (wScan > 0)
                dwFlags |= 0x0008; // KeyeventfScancode
            if (!press)
                dwFlags |= 0x0002; // KeyeventfKeyup
            var input = new INPUT()
            {
                Type = 1,
                KeyboardInput = new KEYBDINPUT()
                {
                    wVk = wVk,
                    wScan = wScan,
                    dwFlags = dwFlags,
                    time = 0,
                    dwExtraInfo = new IntPtr(0)
                }
            };
            SendInput(1, ref input, Marshal.SizeOf(input));
        }
    }
}