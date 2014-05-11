using xSimulate.Configuration;

namespace xSimulate.Action
{
    public class BrowserAction : ActionBase
    {
        public BrowserAction(AutomationAction automationActionData)
            : base(automationActionData)
        {
            this.Name = GetAttributeValue<string>("name");
            this.Url = GetAttributeValue<string>("url");
        }

        public string Name { get; set; }

        public string Url { get; set; }

        public override ActionType ActionType
        {
            get { return ActionType.BrowserAction; }
        }
    }
}