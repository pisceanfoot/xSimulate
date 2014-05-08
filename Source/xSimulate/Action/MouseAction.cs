using xSimulate.Configuration;

namespace xSimulate.Action
{
    public class MouseAction : FindElementAction
    {
        public MouseAction(AutomationAction automationActionData)
            : base(automationActionData)
        {
            this.Click = GetAttributeValue<bool>("click");
            this.ClickNew = GetAttributeValue<bool>("clicknew");
            this.MouseClick = GetAttributeValue<bool>("mouseclick");
            this.Over = GetAttributeValue<bool>("over");
            this.OverNew = GetAttributeValue<bool>("overnew");
            this.Down = GetAttributeValue<bool>("down");
            this.Up = GetAttributeValue<bool>("up");
            this.Focus = GetAttributeValue<bool>("focus");
            this.Move = GetAttributeValue<bool>("move");
            this.MoveNew = GetAttributeValue<bool>("movenew");
            this.MoveEnter = GetAttributeValue<bool>("moveenter");
        }

        public override ActionType ActionType
        {
            get { return Action.ActionType.MouseAction; }
        }

        public bool Click { get; set; }

        public bool ClickNew { get; set; }

        public bool MouseClick { get; set; }

        public bool Over { get; set; }

        public bool OverNew { get; set; }

        public bool Down { get; set; }

        public bool Up { get; set; }

        public bool Focus { get; set; }

        public bool Move { get; set; }

        public bool MoveNew { get; set; }

        public bool MoveEnter { get; set; }
    }
}