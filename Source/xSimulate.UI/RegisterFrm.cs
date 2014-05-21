using System;
using System.Windows.Forms;
using xSimulate.UI.Services;
using xSimulate.Web.Model;

namespace xSimulate.UI
{
    public partial class RegisterFrm : Form
    {
        public RegisterFrm()
        {
            InitializeComponent();
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string userName = TxtUserName.Text.Trim();
            string password = TxtPassword.Text.Trim();
            string qq = TxtQQ.Text.Trim();

            if (string.IsNullOrEmpty(userName))
            {
                MessageHelper.ShowMeesageBox("用户名不能为空");
                TxtUserName.Focus();
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageHelper.ShowMeesageBox("请输入密码");
                TxtPassword.Focus();
            }


            Customer customer = new Customer();
            customer.CustomerID = userName;
            customer.Password = password;
            customer.QQ = qq;


            ResponseInfo<Customer> response = ServiceManager.CreateCustomerService().Register(customer);
            if (response.Code != 1)
            {
                MessageHelper.ShowMeesageBox(response.Message);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}