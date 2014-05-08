using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using xSimulate.Configuration;

namespace xSimulate.Action
{
    public class ClearDataAction : ActionBase
    {
        public ClearDataAction(AutomationAction automationActionData)
            : base(automationActionData)
        {

        }

        public override ActionType ActionType
        {
            get { return ActionType.ClearDataAction; }
        }
    }
}
