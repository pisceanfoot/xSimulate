using System.Threading;
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

            //this.webBrowser.DocumentCompleted += WebBrowser_DocumentCompleted;
        }

        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            OnDocumentCompleted();
        }

        public virtual void Run(IAction action)
        {
            WaitForRun();
            OnProcess(action);
        }

        protected abstract void OnProcess(IAction action);

        protected void WaitForRun()
        {
            if (this.webBrowser.ReadyState != WebBrowserReadyState.Uninitialized)
            {
                while (this.webBrowser.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                    Thread.Sleep(500);
                }
            }
        }

        public virtual bool IsComplete()
        {
            return true;
        }

        protected virtual void OnDocumentCompleted()
        {
        }
    }
}