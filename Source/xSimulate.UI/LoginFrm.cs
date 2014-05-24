using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using xSimulate.UI.Services;
using xSimulate.Web.Model;

namespace xSimulate.UI
{
    public partial class LoginFrm : Form
    {
        public LoginFrm()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string userName = TxtUserName.Text.Trim();
            string password = TxtPassword.Text.Trim();

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                MessageHelper.ShowMeesageBox("用户名或密码不能为空");
                return;
            }

            Customer customer = new Customer();
            customer.CustomerID = userName;
            customer.Password = xSimulate.Common.clsMD5.MD5(password);
            ResponseInfo<Customer> response = ServiceManager.CreateCustomerService().Login(customer);
            if (response.Code != 1)
            {
                MessageHelper.ShowMeesageBox(response.Message);
            }
            else
            {
                SessionContext.CustomerInfo = response.Value;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterFrm registerFrm = new RegisterFrm();
            if (registerFrm.ShowDialog() == DialogResult.OK)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.Show();
            }
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            RegisterFrm regfrm = new RegisterFrm();
            regfrm.ShowDialog();
        }
    }
}
