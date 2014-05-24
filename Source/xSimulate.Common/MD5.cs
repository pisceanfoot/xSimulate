using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;

namespace xSimulate.Common
{
    public class clsMD5
    {
        public static string QV(string str)
        {
            return "'" + str.Replace("'", "''") + "'";
        }
        public static string MD5(string str)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
        }
    }
}
