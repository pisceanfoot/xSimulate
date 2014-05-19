using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using xSimulate.Web.DAL;

namespace xSimulate.Web.BLL
{
    public class CustomerBLL
    {
        public static bool Register(Model.Customer customer)
        {
            return CustomerDAL.Register(customer);
        }

        public static bool Login(Model.Customer customer)
        {
            return CustomerDAL.Login(customer);
        }

        public static Model.Customer GetCustomerDetail(Model.Customer customer)
        {
            return CustomerDAL.CustomerDetail(customer);
        }
    }
}