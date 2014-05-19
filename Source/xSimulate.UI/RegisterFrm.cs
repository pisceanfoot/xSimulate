using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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

            WebService.Customer customer = new WebService.Customer();
            customer.CustomerID = userName;
            customer.Password = password;

            WebService.CustomerService customerService = new WebService.CustomerService();
            customerService.Url = "http://localhost:54706/Service/CustomerService.asmx";
            customerService.Register(customer);
        }
    }
}
