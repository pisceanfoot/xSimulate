using System.Threading;
using xSimulate.Action;
using xSimulate.Browse;

namespace xSimulate.WebAutomationTasks
{
    public class WaitTask : CommonTask
    {
        public WaitTask(WebBrowserEx webBrowser)
            : base(webBrowser)
        {
        }

        protected override void OnProcess(Action.IAction action)
        {
            WaitAction waitAction = action as WaitAction;
            if (waitAction == null)
            {
                return;
            }

            if (waitAction.Seconds > 0)
            {
                Thread.Sleep(waitAction.Seconds * 1000);
            }
            if (waitAction.Milliseconds > 0)
            {
                Thread.Sleep(waitAction.Milliseconds);
            }
        }
    }
}