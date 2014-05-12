using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace xSimulate.Util
{
    public class Win32API
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        /*
         * 
         * 比如：
         *  Temporary Internet Files  （Internet临时文件）
         *  RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 8
         *  Cookies
         *  RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 2
         *  History (历史记录)
         *  RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 1
         *  Form. Data （表单数据）
         *  RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 16
         *  Passwords (密码）
         *  RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 32
         *  Delete All  （全部删除）
         *  RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 255
         *  Delete All - "Also delete files and settings stored by add-ons"
         *  RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 4351
         * 
         * */
        public enum ShowCommands : int
        {
            SW_HIDE = 0,
            SW_SHOWNOrmAL = 1,
            SW_NOrmAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_FORCEMINIMIZE = 11,
            SW_MAX = 11
        }
        [DllImport("shell32.dll")]
        public static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, ShowCommands nShowCmd);

        public static void IE_ClearCookie()
        {
            ShellExecute(IntPtr.Zero, "open", "rundll32.exe", " InetCpl.cpl,ClearMyTracksByProcess 2", "", ShowCommands.SW_HIDE);
        }

        public static void IE_ClearHistory()
        {
            ShellExecute(IntPtr.Zero, "open", "rundll32.exe", " InetCpl.cpl,ClearMyTracksByProcess 8", "", ShowCommands.SW_HIDE);
        }

        public static void IE_ClearAll()
        {
            ShellExecute(IntPtr.Zero, "open", "rundll32.exe", " InetCpl.cpl,ClearMyTracksByProcess 255", "", ShowCommands.SW_HIDE);
        }

        public static void IE_ClearAllPlus()
        {
            ShellExecute(IntPtr.Zero, "open", "rundll32.exe", " InetCpl.cpl,ClearMyTracksByProcess 4351", "", ShowCommands.SW_HIDE);
        }
    }
}