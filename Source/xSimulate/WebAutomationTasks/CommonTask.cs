using System.Threading;
using System.Windows.Forms;
using xSimulate.Action;
using xSimulate.Browse;
using xSimulate.Storage;

namespace xSimulate.WebAutomationTasks
{
    public abstract class CommonTask : ITask
    {
        protected WebBrowserEx webBrowser;

        public CommonTask(WebBrowserEx webBrowser)
        {
            this.webBrowser = webBrowser;
        }

        #region ITask
        public virtual void Run(IAction action)
        {
            WaitForRun();
            OnProcess(action);

            ActionBase actionBase = action as ActionBase;
            if (actionBase != null)
            {
                if (!actionBase.SaveData)
                {
                    TaskStorage.Storage = null;
                }
            }
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
        #endregion

        #region Debug
        protected void DebugElement(HtmlElement element)
        {
            LoggerManager.Debug(LoggerManager.FormatElement(element));
        }
        #endregion
    }
}