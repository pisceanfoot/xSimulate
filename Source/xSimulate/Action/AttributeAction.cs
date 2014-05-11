using xSimulate.Configuration;

namespace xSimulate.Action
{
    public class AttributeAction : FindAction
    {
        public AttributeAction(AutomationAction automationActionData)
            : base(automationActionData)
        {
            this.SetValue = GetAttributeValue<string>("value");
        }

        public override ActionType ActionType
        {
            get { return ActionType.AttributeAction; }
        }

        public string SetValue { get; set; }
    }
}