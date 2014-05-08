using System;
using System.Xml.Serialization;
using xSimulate.Configuration;

namespace xSimulate.Action
{
    public class ClickAction : FindElementAction
    {
        public ClickAction(AutomationAction automationActionData)
            : base(automationActionData)
        {

        }

        public override ActionType ActionType
        {
            get { return ActionType.ClickAction; }
        }
    }
}