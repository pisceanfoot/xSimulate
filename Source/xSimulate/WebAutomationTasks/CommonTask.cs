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
                    TaskStorage.Clear();
                }
                if (actionBase.Wait > 0)
                {
                    Thread.Sleep(actionBase.Wait);
                    //int count = 0;
                    //int step = 10;

                    //while (count <= actionBase.Wait)
                    //{
                    //    Thread.Sleep(step);
                    //    //Application.DoEvents();

                    //    count += step;
                    //    if (count < actionBase.Wait && count + step > actionBase.Wait)
                    //    {
                    //        step = actionBase.Wait - count;
                    //    }
                    //}
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
                    Thread.Sleep(10);
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

        protected void SaveData(IAction action, HtmlElement element)
        {
            ActionBase actionBase = action as ActionBase;
            if (actionBase == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(actionBase.SaveDatakey))
            {
                TaskStorage.Storage = element;
            }
            else
            {
                TaskStorage.SetKey(actionBase.SaveDatakey, element);
            }
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
                return TaskStorage.Storage;
            }
            else
            {
                return TaskStorage.GetKey(actionBase.GetDatakey);
            }
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