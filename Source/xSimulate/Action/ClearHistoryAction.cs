using xSimulate.Configuration;

namespace xSimulate.Action
{
    public enum ClearHistoryType
    {
        Cookie = 0,
        History,
        All,
        AllPlus
    }

    public class ClearHistoryAction : ActionBase
    {
        public ClearHistoryAction(AutomationAction automationActionData)
            : base(automationActionData)
        {
            this.ClearHistoryType = GetAttributeValue<ClearHistoryType>("type");
        }

        public ClearHistoryType ClearHistoryType { get; set; }

        public override ActionType ActionType
        {
            get { return ActionType.ClearHistoryAction; }
        }
    }
}