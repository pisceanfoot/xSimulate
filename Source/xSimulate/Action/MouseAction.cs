using System;
using System.Xml.Serialization;
using xSimulate.Configuration;
namespace xSimulate.Action
{
    public class MouseAction : FindElementAction
    {
        public MouseAction(AutomationAction automationActionData)
            : base(automationActionData)
        {
            this.Click = GetAttributeValue<bool>("click");
            this.MouseClick = GetAttributeValue<bool>("mouseclick");
        }

        public override ActionType ActionType
        {
            get { return Action.ActionType.MouseAction; }
        }

        public bool Click { get; set; }

        public bool MouseClick { get; set; }
    }
}