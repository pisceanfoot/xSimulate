using System.Windows.Forms;
using xSimulate.Action;
using xSimulate.Browse;
using xSimulate.Storage;
using xSimulate.WebAutomationTasks;

namespace xSimulate.Browser
{
    public class PageTask : CommonTask
    {
        public PageTask(WebBrowserEx webBrowser)
            : base(webBrowser)
        {
        }

        public override void Run(IAction action)
        {
            OnProcess(action);
        }

        protected override void OnProcess(IAction action)
        {
            PageAction pageAction = action as PageAction;
            webBrowser.Navigate(pageAction.Url);

            LoggerManager.Debug("Browser: {0}", pageAction.Url);
        }

        public override bool IsComplete()
        {
            if (this.webBrowser.IsDisposed)
            {
                return true;
            }

            return this.webBrowser.ReadyState == WebBrowserReadyState.Complete;
        }
    }
}