using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xSimulate.Web.Model
{
    [Serializable]
    public class CustomerAccount
    {
        public int CustomerSysNo { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal LockAmount { get; set; }
    }
}