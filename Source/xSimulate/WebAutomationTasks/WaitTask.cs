using System.Threading;
using System.Windows.Forms;
using xSimulate.Action;
using xSimulate.Browse;

namespace xSimulate.WebAutomationTasks
{
    public class WaitTask : CommonTask
    {
        public WaitTask(AutomationManagement manager)
            : base(manager)
        {
        }

        protected override void OnProcess(Action.IAction action)
        {
            WaitAction waitAction = action as WaitAction;
            if (waitAction == null)
            {
                return;
            }

            LoggerManager.Debug(action.AutomationActionData);

            if (waitAction.Seconds > 0)
            {
                Sleep(waitAction.Seconds * 1000);
            }
            if (waitAction.Milliseconds > 0)
            {
                Sleep(waitAction.Milliseconds);
            }
        }

        private void Sleep(int wait)
        {
            Thread.Sleep(wait);
        }
    }
}