using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using xSimulate.Common;
using xSimulate.UI.Config;
using xSimulate.UI.Services;
using xSimulate.Web.Model;

namespace xSimulate.UI
{
    public partial class CustomerSettingCtrl : UserControl
    {
        public CustomerSettingCtrl()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            this.comboBoxBrowser.SelectedIndex = 0;
        }

        private void radioButtonWangWang_CheckedChanged(object sender, EventArgs e)
        {
            TxtWangwang.Enabled = radioButtonWangWang.Checked;
        }

        private void radioButtonItemID_CheckedChanged(object sender, EventArgs e)
        {
            TxtItemID.Enabled = radioButtonItemID.Checked;
        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            FileGenerator file = new FileGenerator();
            file.Browser = comboBoxBrowser.SelectedItem.ToString();
            file.Keyword = TxtKeyword.Text;
            file.FindWangWang = radioButtonWangWang.Checked ? TxtWangwang.Text : null;
            file.FindItemID = radioButtonItemID.Checked ? TxtItemID.Text : null;

            if (!radioButtonDefault.Checked)
            {
                foreach (Control c in GroupSearchType.Controls)
                {
                    if (c is RadioButton)
                    {
                        RadioButton r = c as RadioButton;
                        file.SearchPageBrowserType = r.Text;
                        break;
                    }
                }
            }

            if (checkBoxQuJian.Checked)
            {
                file.PriceFrom = PriceFrom.Value.ToString();
                file.PriceTo = PriceTo.Value.ToString();
            }
            if (checkBoxDiZhi.Checked)
            {
                file.Province = Province.Text;
                file.City = City.Text;
            }
            file.MaxPage = MaxPage.Value.ToString();

            file.ItemBrowserTime = ItemBrowserTimes.Value.ToString();
            file.ClickReview = checkBoxItemClickReview.Checked.ToString();

            CustomerSetting setting = new CustomerSetting();
            setting.CustomerSysNo = SessionContext.CustomerInfo.SysNo;
            setting.Setting = XmlSerializerHelper.Serializer<FileGenerator>(file);

            CustomerService service = ServiceManager.CreateCustomerService();
            string result = service.SaveCustomerSetting(setting);
            if (string.IsNullOrEmpty(result))
            {
                MessageHelper.ShowMeesageBox("保持成功!");
            }
            else
            {
                MessageHelper.ShowMeesageBox(result);
            }
        }

        private void checkBoxQuJian_CheckedChanged(object sender, EventArgs e)
        {
            PriceFrom.Enabled = PriceTo.Enabled = checkBoxQuJian.Checked;
        }

        private void checkBoxDiZhi_CheckedChanged(object sender, EventArgs e)
        {
            Province.Enabled = City.Enabled = checkBoxDiZhi.Checked;
        }
    }
}
