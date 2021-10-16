using System;
using System.Runtime.InteropServices;
using System.Text;

namespace TeamViewplay
{
    class Program
    {
        static void Main(string[] args)


        {
            IntPtr hwnd = FindWindow(null, "TeamViewer");
            if (hwnd != IntPtr.Zero)
            {
                Getwidows(hwnd);
            }
            else
            {
                Console.WriteLine("没有找到窗口");
            }
        }

        public static void Getwidows(IntPtr  hwnd) {
            IntPtr winPtr = GetWindow(hwnd, GetWindowCmd.GW_CHILD);
            while (winPtr != IntPtr.Zero)
            {
                GetText(winPtr);
                IntPtr temp = GetWindow(winPtr, GetWindowCmd.GW_CHILD);
                winPtr = temp;
            }
        }

        public static void GetText(IntPtr GetWind) {
            IntPtr GetPtr = GetWindow(GetWind, GetWindowCmd.GW_HWNDNEXT);
            while (GetPtr != IntPtr.Zero)
            {
                StringBuilder sb = new StringBuilder(1024);
                GetClassName(GetPtr, sb, 1024);
                if (sb.ToString().TrimEnd('\r', '\n') == "Edit")
                {
                    StringBuilder stringBuilder2 = new StringBuilder(1024);
                    SendMessage(GetPtr, WM_GETTEXT, 1024, stringBuilder2);
                    Console.WriteLine(stringBuilder2.ToString().TrimEnd('\r', '\n'));
                }
                Getwidows(GetPtr);
                IntPtr  temp = GetWindow(GetPtr, GetWindowCmd.GW_HWNDNEXT);
                GetPtr = temp;
            }
        }


        enum GetWindowCmd : uint
        {
            GW_HWNDFIRST = 0,
            GW_HWNDLAST = 1,
            GW_HWNDNEXT = 2,
            GW_HWNDPREV = 3,
            GW_OWNER = 4,
            GW_CHILD = 5,
            GW_ENABLEDPOPUP = 6
        }
        public static int WM_GETTEXT = 0x000D;

        [DllImport("user32.dll")]
        public static extern bool DestroyWindow([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "GetClassName")]
        public static extern int GetClassName([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd, System.Text.StringBuilder lpClassName, int nMaxCount);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetWindow(IntPtr hWnd, GetWindowCmd uCmd);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "EnumWindows")]
        public static extern System.IntPtr GetWindow([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd, uint uCmd);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, StringBuilder lParam);
        [DllImport("User32.dll ")]
        public static extern IntPtr FindWindowEx(IntPtr parent, IntPtr childe, string strclass, string FrmText);


    }

}


