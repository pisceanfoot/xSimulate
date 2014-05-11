using xSimulate.Configuration;

namespace xSimulate.Action
{
    public class KeyboardAction : FindAction
    {
        public KeyboardAction(AutomationAction automationActionData)
            : base(automationActionData)
        {
            this.KeyDown = GetAttributeValue<bool>("keydown");
            this.KeyUp = GetAttributeValue<bool>("keyup");
        }

        public bool KeyDown { get; set; }

        public bool KeyUp { get; set; }

        public override ActionType ActionType
        {
            get { return ActionType.KeyboardAction; }
        }
    }
}