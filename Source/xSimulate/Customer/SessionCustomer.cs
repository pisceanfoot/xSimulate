using System;
using System.Collections.Generic;
using System.Text;
using xSimulate.Web.Model;

namespace xSimulate
{
    public class SessionCustomer
    {
        public static int CustomerSysNo { get; set; }

        public static string Token { get; set; }

        public static RetrieveTask CurrentTask { get; set; }
    }
}
