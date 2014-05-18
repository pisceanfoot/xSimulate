using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xSimulate.Web.Model
{
    [Serializable]
    public class Customer
    {
        public int SysNo { get; set; }

        public string CustomerID { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public CustomerAccount Account { get; set; }
    }
}