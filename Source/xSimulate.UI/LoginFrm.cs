using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using xSimulate.UI.Services;
using xSimulate.Web.Model;
using System.IO;
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
            if (File.Exists("member"))
            {
                MessageBox.Show("0k");
                string s = File.ReadAllText("member");
                MessageBox.Show(s);
                File.AppendAllText("member", DateTime.Now.ToString() + "中国", Encoding.GetEncoding("GB2312"));
                //File.WriteAllLines
            }
            else
            {
                File.AppendAllText("member", DateTime.Now.ToString() + "中国", Encoding.GetEncoding("GB2312"));
            }
            //this.Close();
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
                string boolPass = chkPass.Checked ? "1" : "0";
                string boolLogin = chkAuto.Checked ? "1" : "0";
                string[] member = { customer.CustomerID, password, boolPass, boolLogin };

                File.WriteAllLines("member", member, Encoding.GetEncoding("GB2312"));

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

        //private bool Login()
        //{

        //}

        private void LoginFrm_Load(object sender, EventArgs e)
        {
            if (File.Exists("member"))
            {
                string[] member = File.ReadAllLines("member", Encoding.GetEncoding("GB2312"));
                if (member.Length == 4)
                {
                    //记住密码
                    if (member[2] == "1")
                    {
                        this.TxtUserName.Text = member[0];
                        this.TxtPassword.Text = member[1];
                        this.chkPass.Checked = true;
                        if (member[3] == "1")
                        {
                            this.chkAuto.Checked = true;
                            Customer customer = new Customer();
                            customer.CustomerID = member[0];
                            customer.Password = xSimulate.Common.clsMD5.MD5(member[1]);
                            ResponseInfo<Customer> response = ServiceManager.CreateCustomerService().Login(customer);
                            if (response.Code != 1)
                            {
                                MessageHelper.ShowMeesageBox(response.Message);
                            }
                            else
                            {
                                //string boolPass = chkPass.Checked ? "1" : "0";
                                //string boolLogin = chkAuto.Checked ? "1" : "0";
                                //string[] member = { customer.CustomerID, password, boolPass, boolLogin };

                                //File.WriteAllLines("member", member, Encoding.GetEncoding("GB2312"));

                                SessionContext.CustomerInfo = response.Value;
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                        }
                    }
                }
            }
        }
    }
}
