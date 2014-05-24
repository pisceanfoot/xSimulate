using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace xSimulate
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                RunProcess();
                return;
            }
            if (args[0] != "{721F6B5C-125C-41A9-9EB2-C1D3B94C302B}")
            {
                RunProcess();
                return;
            }

            try
            {
                int customerSysNo = Convert.ToInt32(args[1]);
                SessionCustomer.CustomerSysNo = customerSysNo;
            }
            catch
            {
                RunProcess();
                return;
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainFrm());
        }

        private static void RunProcess()
        {
            try
            {
                Process.Start("WRM.exe");
            }
            catch { }
        }
    }
}