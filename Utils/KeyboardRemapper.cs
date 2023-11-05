using System.Runtime.InteropServices;
using System.Diagnostics;

namespace FastGamePanel.Utils
{
    internal class KeyboardRemapper
    {
        private static TaskCompletionSource<int> keyPressWaitingTask;

        private static bool isHookEnabled = false;

        private static bool isRemappingEnabled = false;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int HC_ACTION = 0;

        private static IntPtr hookHandle = IntPtr.Zero;

        private static IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode == HC_ACTION)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                if (wParam == (IntPtr)WM_KEYDOWN)
                {
                    if (keyPressWaitingTask != null)
                    {
                        keyPressWaitingTask.TrySetResult(vkCode);
                    }

                    if (isRemappingEnabled)
                    {
                        // TODO
                        vkCode = 0x42;
                        Marshal.WriteInt32(lParam, vkCode);
                    }
                }
                else if (wParam == (IntPtr)WM_KEYUP)
                {
                       
                    if (isRemappingEnabled)
                    {
                        // TODO
                        vkCode = 0x42;
                        Marshal.WriteInt32(lParam, vkCode);
                    }
                    
                }
            }

            return CallNextHookEx(hookHandle, nCode, wParam, lParam);
        }

        public static bool GetRemappingEnabled()
        {
            return isRemappingEnabled;
        }

        public static bool GetKeyboardHookEnabled()
        {
            return isHookEnabled;
        }

        public static void EnableRemapping()
        {
            isRemappingEnabled = true;
        }

        public static void DisableRemapping()
        {
            isRemappingEnabled = false;
        }

        public static void SetKeyboardHook()
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                LowLevelKeyboardProc proc = KeyboardHookCallback;
                hookHandle = SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
                isHookEnabled = true;
            }
        }
        public static void UnsetKeyboardHook()
        {
            UnhookWindowsHookEx(hookHandle);
            isHookEnabled = false;
        }

        public static async Task<int> GetNextKeystroke()
        {
            keyPressWaitingTask = new TaskCompletionSource<int>();
            var keyCode = await keyPressWaitingTask.Task;
            keyPressWaitingTask = null;
            return keyCode;
        }
    }
}
