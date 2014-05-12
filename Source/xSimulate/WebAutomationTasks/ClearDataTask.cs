using xSimulate.Action;
using xSimulate.Browse;
using xSimulate.Storage;

namespace xSimulate.WebAutomationTasks
{
    public class ClearDataTask : CommonTask
    {
        public ClearDataTask(AutomationManagement manager)
            : base(manager)
        {
        }

        protected override void OnProcess(Action.IAction action)
        {
            LoggerManager.Debug("Clear TaskStorage.Storage");

            this.ClearData();
        }
    }
}