using xSimulate.Configuration;

namespace xSimulate.Action
{
    public enum Position
    {
        None = 0,
        Element,
        PageBottom
    }

    public class ScrollAction : ActionBase
    {
        public ScrollAction(AutomationAction automationActionData)
            : base(automationActionData)
        {
            this.Position = GetAttributeValue<Position>("position");
            this.Offset = GetAttributeValue<int>("offset");
            this.Period = GetAttributeValue<int>("period", 500);
        }

        public override ActionType ActionType
        {
            get { return ActionType.ScrollAction; }
        }

        public Position Position { get; set; }

        public int Offset { get; set; }

        public int Period { get; set; }
    }
}