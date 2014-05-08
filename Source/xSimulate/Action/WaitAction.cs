using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using xSimulate.Configuration;

namespace xSimulate.Action
{
    public class WaitAction : ActionBase
    {
        public WaitAction(AutomationAction automationActionData)
            : base(automationActionData)
        {
            this.Seconds = GetAttributeValue<int>("seconds");
            this.Milliseconds = GetAttributeValue<int>("milliseconds");
        }

        public override ActionType ActionType
        {
            get { return ActionType.WaitAction; }
        }

        public int Seconds { get; set; }

        public int Milliseconds { get; set; }
    }
}
