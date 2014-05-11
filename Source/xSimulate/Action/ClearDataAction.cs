using xSimulate.Configuration;

namespace xSimulate.Action
{
    public class ClearDataAction : ActionBase
    {
        public ClearDataAction(AutomationAction automationActionData)
            : base(automationActionData)
        {
        }

        public override ActionType ActionType
        {
            get { return ActionType.ClearDataAction; }
        }
    }
}