using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NetFramework.Logger;

namespace xSimulate.Web.Util
{
    public class Logger
    {
        private static ILogger log = NetFramework.Logger.LoggerManager.GetLog("WebLog");

        public static void Error(Exception ex)
        {
            log.Error(ex);
        }
    }
}