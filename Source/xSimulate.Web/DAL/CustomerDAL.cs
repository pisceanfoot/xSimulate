using System;
using NetFramework.DataAccess;

namespace xSimulate.Web.DAL
{
    public class CustomerDAL
    {
        public static bool Register(Model.Customer customer)
        {
            IDataCommand dataCommand = DataCommandManager.GetCommand("Customer_Register");
            dataCommand.SetParameter("@CustomerID", customer.CustomerID);
            dataCommand.SetParameter("@Name", customer.Name);
            dataCommand.SetParameter("@QQ", customer.QQ);
            dataCommand.SetParameter("@Password", customer.Password);

            dataCommand.ExecuteNonQuery();
            int result = Convert.ToInt32(dataCommand.Parameters["@Result"].Value);

            return result == 1;
        }

        public static bool Login(Model.Customer customer)
        {
            IDataCommand dataCommand = DataCommandManager.GetCommand("Customer_Login");
            dataCommand.SetParameter("@CustomerID", customer.CustomerID);
            dataCommand.SetParameter("@Password", customer.Password);

            int result = dataCommand.ExecuteScalar<int>();

            return result == 1;
        }

        public static Model.Customer CustomerDetail(Model.Customer customer)
        {
            IDataCommand dataCommand = DataCommandManager.GetCommand("Customer_CustomerDetail");
            dataCommand.SetParameter("@CustomerID", customer.CustomerID);

            Model.Customer customerInfo = dataCommand.ExecuteEntity<Model.Customer>();

            return customerInfo;
        }
    }
}