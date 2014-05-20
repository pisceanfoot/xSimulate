using System;
using System.Collections.Generic;
using System.Web;
using NetFramework.DataAccess.EntityBuilder;

namespace xSimulate.Web.Model
{
    [Serializable]
    public class CustomerSetting
    {
        [DataMapping]
        public int SysNo { get; set; }

        [DataMapping]
        public int CustomerSysNo { get; set; }

        [DataMapping]
        public string Setting { get; set; }
    }
}