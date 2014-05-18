using System;
using NetFramework.DataAccess;

namespace xSimulate.Web.DAL
{
    public class CustomerDAL
    {
        public static bool Register(Model.Customer customer)
        {
            IDataCommand dataCommand = DataCommandManager.GetCommand("Customer_Register");
            dataCommand.Parameters["@CustomerID"].Value = customer.CustomerID;
            dataCommand.Parameters["@Name"].Value = customer.Name;
            dataCommand.Parameters["@Password"].Value = customer.Password;

            dataCommand.ExecuteNonQuery();
            int result = Convert.ToInt32(dataCommand.Parameters["@Result"].Value);

            return result == 1;
        }
    }
}