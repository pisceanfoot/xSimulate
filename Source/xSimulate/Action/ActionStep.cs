using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace xSimulate.Action
{
    public class ActionStep
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<IAction> ActionList { get; set; }
    }
}