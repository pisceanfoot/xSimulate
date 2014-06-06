using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace xSimulate.UI
{
    public partial class KeywordSearchCtrl : UserControl
    {
        public KeywordSearchCtrl()
        {
            InitializeComponent();
        }

        private void TxtMaxPage_TextChanged(object sender, EventArgs e)
        {
            int page;
            if (!int.TryParse(this.TxtMaxPage.Text, out page))
            {
                this.TxtMaxPage.Text = "10";
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (this.RadioButtonTaobao.Checked)
            {

            }
        }
    }
}
