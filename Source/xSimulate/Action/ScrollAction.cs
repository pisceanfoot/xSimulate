using System;
using System.Xml.Serialization;
using xSimulate.Configuration;
namespace xSimulate.Action
{
    public enum Position
    {
        None,
        PageBottom
    }

    public class ScrollAction : ActionBase
    {
        public ScrollAction(AutomationAction automationActionData)
            : base(automationActionData)
        {
            this.Position = GetAttributeValue<Position>("position");
        }

        public override ActionType ActionType
        {
            get { return ActionType.ScrollAction; }
        }

        public Position Position { get; set; }
    }
}