using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NetFramework.DataAccess.EntityBuilder;

namespace xSimulate.Web.Model
{
    [Serializable]
    public class CustomerAccount
    {
        [DataMapping]
        public int CustomerSysNo { get; set; }

        [DataMapping]
        public decimal Amount { get; set; }

        [DataMapping]
        public decimal LockedAmount { get; set; }
    }
}