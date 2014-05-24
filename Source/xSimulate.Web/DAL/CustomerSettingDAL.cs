using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NetFramework.DataAccess;

namespace xSimulate.Web.DAL
{
    public class CustomerSettingDAL
    {
        public static Model.CustomerSetting GetCustomerSetting(int customerSysNo)
        {
            IDataCommand dataCommand = DataCommandManager.GetCommand("Customer_GetCustomerSetting");
            dataCommand.SetParameter("@CustomerSysNo", customerSysNo);

            return dataCommand.ExecuteEntity<Model.CustomerSetting>();
        }

        public static void SaveCustomerSetting(Model.CustomerSetting customerSetting)
        {
            IDataCommand dataCommand = DataCommandManager.GetCommand("Customer_SaveCustomerSetting");
            dataCommand.SetParameter("@CustomerSysNo", customerSetting.CustomerSysNo);
            dataCommand.SetParameter("@Setting", customerSetting.Setting);

            dataCommand.ExecuteNonQuery();
            customerSetting.SysNo = Convert.ToInt32(dataCommand.Parameters["@SysNo"].Value);
        }
    }
}