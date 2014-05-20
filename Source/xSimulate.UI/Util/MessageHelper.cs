using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace xSimulate.UI
{
    public class MessageHelper
    {
        public static void ShowMeesageBox(string message)
        {
            MessageBox.Show(message, "应用程序提示", MessageBoxButtons.OK);
        }
    }
}
