using System.Windows.Forms;

using xSimulate.Action;
using xSimulate.Browse;

namespace xSimulate.WebAutomationTasks
{
    public abstract class CommonTask : ITask
    {
        protected WebBrowserEx webBrowser;

        public CommonTask(WebBrowserEx webBrowser)
        {
            this.webBrowser = webBrowser;

            this.webBrowser.DocumentCompleted += WebBrowser_DocumentCompleted;
        }

        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            OnDocumentCompleted();
        }

        public abstract void Run(IAction action);

        public virtual bool IsComplete()
        {
            return true;
        }

        protected virtual void OnDocumentCompleted()
        {
        }
    }
}