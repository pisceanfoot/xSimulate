using System.Threading;
using System.Windows.Forms;
using xSimulate.Action;
using xSimulate.Browse;
using xSimulate.Storage;

namespace xSimulate.WebAutomationTasks
{
    public abstract class CommonTask : ITask
    {
        private AutomationManagement manager;
        protected WebBrowserEx webBrowser;

        public CommonTask(AutomationManagement manager)
        {
            this.webBrowser = (WebBrowserEx)manager.Browser;
            this.manager = manager;
        }

        #region ITask
        public void RunAction(IAction action)
        {
            this.manager.RunAction(action);
        }

        public virtual void Run(IAction action)
        {
            WaitForRun();

            OnProcess(action);

            ActionBase actionBase = action as ActionBase;
            if (actionBase != null)
            {
                if (!actionBase.SaveData)
                {
                    ClearData();
                }
                if (actionBase.Wait > 0)
                {
                    Thread.Sleep(actionBase.Wait);
                }
            }
        }

        protected abstract void OnProcess(IAction action);

        protected void WaitForRun()
        {
            bool runing = false;

            do
            {
                runing = this.webBrowser.Busy;

                if (runing)
                {
                    Thread.Sleep(100);
                }
            }
            while (runing);
        }

        public virtual bool IsComplete()
        {
            return true;
        }

        public virtual bool CanChildRun(IAction action)
        {
            return true;
        }

        public virtual bool ChildComplete(IAction action)
        {
            return true;
        }

        #endregion ITask

        #region Storage
        public void ClearData()
        {
            this.manager.ClearData();
        }

        protected void SaveData(IAction action, HtmlElement element)
        {
            ActionBase actionBase = action as ActionBase;
            if (actionBase == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(actionBase.SaveDatakey))
            {
                this.manager.SaveData(element);
            }
            else
            {
                this.manager.SaveData(actionBase.SaveDatakey, element);
            }
        }

        protected void SaveData<T>(IAction action, T obj)
        {
            ActionBase actionBase = action as ActionBase;
            if (actionBase == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(actionBase.SaveDatakey))
            {
                this.manager.SaveData(obj);
            }
            else
            {
                this.manager.SaveData(actionBase.SaveDatakey, obj);
            }
        }

        protected void SaveData<T>(string key, T obj)
        {
            this.manager.SaveData(key, obj);
        }

        protected object GetData(IAction action)
        {
            ActionBase actionBase = action as ActionBase;
            if (actionBase == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(actionBase.GetDatakey))
            {
                return this.manager.GetData();
            }
            else
            {
                return this.manager.GetData(actionBase.GetDatakey);
            }
        }

        protected object GetData(string key)
        {
            return this.manager.GetData(key);
        }

        #endregion Storage

        #region Debug

        protected void DebugElement(HtmlElement element)
        {
#if DEBUG
            LoggerManager.Debug(LoggerManager.FormatElement(element));
#endif
        }

        #endregion Debug

        #region UI
        protected void Call<T>(Callback<T> action, T obj)
        {
            if (this.webBrowser.InvokeRequired)
            {
                this.webBrowser.Invoke(action, obj);
            }
            else
            {
                action(obj);
            }
        }

        protected R Call<T, R>(Callback<T, R> action, T obj)
        {
            if (this.webBrowser.InvokeRequired)
            {
                return (R)this.webBrowser.Invoke(action, obj);
            }
            else
            {
                return (R)action(obj);
            }
        }
        #endregion
    }
}