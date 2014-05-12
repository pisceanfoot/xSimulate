using xSimulate.Configuration;

namespace xSimulate.Action
{
    public class PageAction : ClickAction
    {
        public PageAction(AutomationAction automationActionData)
            : base(automationActionData)
        {
            this.MaxPageIndex = this.GetAttributeValue<int>("max", 10);
        }

        public int MaxPageIndex { get; set; }

        public string PageRegex { get; set; }

        public override ActionType ActionType
        {
            get { return ActionType.PageAction; }
        }
    }
}