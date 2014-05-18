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

            Services.Customer cus = new Services.Customer();
            cus.Register(new Services.Customer1() { CustomerID = "111" });
        }
    }
}
