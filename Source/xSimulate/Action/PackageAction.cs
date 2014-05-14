using System;
using System.Collections.Generic;
using System.Text;
using xSimulate.Configuration;

namespace xSimulate.Action
{
    public class PackageAction : ActionBase
    {
        public PackageAction(AutomationAction automationActionData)
            : base(automationActionData)
        {
        }

        public override ActionType ActionType
        {
            get { return ActionType.PackageAction; }
        }
    }
}
