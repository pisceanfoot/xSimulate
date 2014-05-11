using System;
using System.Xml.Serialization;
using xSimulate.Configuration;

namespace xSimulate.Action
{
    public class PageAction : ClickAction
    {
        public PageAction(AutomationAction automationActionData)
            : base(automationActionData)
        {
        }

        public override ActionType ActionType
        {
            get { return ActionType.PageAction; }
        }
    }
}