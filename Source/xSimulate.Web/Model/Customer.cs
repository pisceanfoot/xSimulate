using System;
using System.Collections.Generic;
using System.Web;
using NetFramework.DataAccess.EntityBuilder;

namespace xSimulate.Web.Model
{
    [Serializable]
    public class Customer
    {
        [DataMapping]
        public int SysNo { get; set; }

        [DataMapping]
        public string CustomerID { get; set; }

        [DataMapping]
        public string Name { get; set; }

        [DataMapping]
        public string Password { get; set; }

        [DataMapping]
        public string QQ { get; set; }

        [ReferencedEntity]
        public CustomerAccount Account { get; set; }
    }
}