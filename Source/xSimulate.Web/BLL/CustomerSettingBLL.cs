using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using xSimulate.Web.DAL;

namespace xSimulate.Web.BLL
{
    public class CustomerSettingBLL
    {
        public static int SaveCustomerSetting(Model.CustomerSetting customerSetting)
        {
            CustomerSettingDAL.SaveCustomerSetting(customerSetting);
            return 1;
        }

        public static Model.CustomerSetting GetCustomerSetting(int customerSysNo)
        {
            Model.CustomerSetting setting = CustomerSettingDAL.GetCustomerSetting(customerSysNo);
            return setting;
        }
    }
}