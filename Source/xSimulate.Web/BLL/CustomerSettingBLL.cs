using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using xSimulate.Web.DAL;

namespace xSimulate.Web.BLL
{
    public class CustomerSettingBLL
    {
        public static string GetCustomerSetting(int customerSysNo)
        {
            Model.CustomerSetting setting = CustomerSettingDAL.GetCustomerSetting(customerSysNo);
            return string.Empty;
        }
    }
}