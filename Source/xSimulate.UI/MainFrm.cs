using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace xSimulate.UI
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            LoginFrm loginFrm = new LoginFrm();
            if (loginFrm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LblUserName.Text = SessionContext.CustomerInfo.CustomerID;
                LblAmount.Text = SessionContext.CustomerInfo.Account.Amount.ToString("f");
            }
            else
            {
                this.Close();
            }
        }
    }
}
