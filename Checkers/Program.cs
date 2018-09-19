using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Checkers{
    class Program{

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd,int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        static void Main(string[] args){


            GameControl.GetInstance().RUN();
            //MoveControler.ProcessMove();
            

        }

        public static void HideConsole() {
            var handle = GetConsoleWindow();
            ShowWindow(handle,SW_HIDE);
        }

        public static void ShowConsole() {
            var handle = GetConsoleWindow();
            ShowWindow(handle,SW_SHOW);
        }

    }
}