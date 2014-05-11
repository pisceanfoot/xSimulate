using System.Windows.Forms;
using xSimulate.Action;
using xSimulate.Browse;
using xSimulate.WebAutomationTasks;

namespace xSimulate.Browser
{
    public class PageTask : ClickTask
    {
        public PageTask(WebBrowserEx webBrowser)
            : base(webBrowser)
        {
        }

        protected override void OnProcess(IAction action)
        {
        }

        public override bool ChildComplete(IAction action)
        {
            PageAction pageAction = action as PageAction;

            HtmlElement element = this.GetData(action) as HtmlElement;
            if (element == null)
            {
                base.OnProcess(action);
                return false;
            }

            return true;
        }

        private void Run(PageAction pageAction)
        {
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