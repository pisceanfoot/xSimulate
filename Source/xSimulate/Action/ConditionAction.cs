using xSimulate.Configuration;

namespace xSimulate.Action
{
    public class ConditionAction : ActionBase
    {
        public ConditionAction(AutomationAction automationActionData)
            : base(automationActionData)
        {
        }

        public override ActionType ActionType
        {
            get { return ActionType.ConditionAction; }
        }
    }
}