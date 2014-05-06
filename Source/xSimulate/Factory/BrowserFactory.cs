using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using xSimulate.Action;
using xSimulate.Browse;
using xSimulate.Browser;
using xSimulate.WebAutomationTasks;

namespace xSimulate.Factory
{
    public class BrowserFactory
    {
        private WebBrowserEx webBrowser;
        private Dictionary<ActionType, ITask> taskDic;

        public BrowserFactory(WebBrowserEx webBrowser)
        {
            taskDic = new Dictionary<ActionType, ITask>();

            this.webBrowser = webBrowser;
            this.webBrowser.ScriptErrorsSuppressed = false;
            this.webBrowser.NewWindow3 += webBrowser_NewWindow3;
        }

        void webBrowser_NewWindow3(object sender, WebBrowserNewWindowEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Url))
            {

                //Prevent new window
                e.Cancel = true;

                // Navigate to url from new window
                //string.Format("Referer: {0}", this.webBrowser.Url.ToString() + Environment.NewLine)
                this.webBrowser.Navigate(e.Url, "_self", null, null);
            }
            
        }

        public void Run(IAction action)
        {
            ITask task = Create(action);
            task.Run(action);
            while (!task.IsComplete())
            {
                Application.DoEvents();
                Thread.Sleep(500);
            }

            if (action.NextAction != null && action.NextAction.Count > 0)
            {
                foreach (IAction nextAction in action.NextAction)
                {
                    Run(nextAction);
                }
            }
        }

        public ITask Create(IAction action)
        {
            ITask task;
            taskDic.TryGetValue(action.ActionType, out task);
            if (task != null)
            {
                return task;
            }

            if (action.ActionType == ActionType.PageAction)
            {
                task = new PageTask(this.webBrowser);
            }
            else if (action.ActionType == ActionType.FindElementAction)
            {
                task = new FindElementTask(this.webBrowser);
            }
            else if (action.ActionType == ActionType.MouseAction)
            {
                task = new MouseTask(this.webBrowser);
            }
            else if (action.ActionType == ActionType.AttributeAction)
            {
                task = new AttributeTask(this.webBrowser);
            }
            else if (action.ActionType == ActionType.ScrollAction)
            {
                task = new ScrollTask(this.webBrowser);
            }
            else if (action.ActionType == ActionType.ClearDataAction)
            {
                task = new ClearDataTask(this.webBrowser);
            }

            taskDic.Add(action.ActionType, task);
            return task;
        }
    }
}