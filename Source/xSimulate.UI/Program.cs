using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace xSimulate.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (IsOneInstance())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainFrm());
            }
            else
            {
                return;
            }
        }

        private static System.Threading.Mutex mutex;
        private static bool IsOneInstance()
        {
            Application.ApplicationExit += Application_ApplicationExit;

            bool one;
            mutex = new System.Threading.Mutex(true, "{585CFBAD-3B6C-413F-88D1-6233310688FE}", out one);
            return one;
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (mutex != null)
            {
                mutex.ReleaseMutex();
                mutex.Close();
            }
        }
    }
}
