using System.Windows.Forms;
using xSimulate.Action;
using xSimulate.Browse;

namespace xSimulate.WebAutomationTasks
{
    public class BrowserTask : CommonTask
    {
        public BrowserTask(AutomationManagement manager)
            : base(manager)
        {
        }

        public override void Run(IAction action)
        {
            OnProcess(action);
        }

        protected override void OnProcess(IAction action)
        {
            BrowserAction pageAction = action as BrowserAction;
            webBrowser.Navigate(pageAction.Url);

            LoggerManager.Debug("Browser: {0}", pageAction.Url);
        }

        public override bool IsComplete()
        {
            if (this.webBrowser.IsDisposed)
            {
                return true;
            }

            return !this.webBrowser.Busy;
        }
    }
}