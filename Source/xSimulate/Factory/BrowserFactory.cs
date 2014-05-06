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
        private System.Windows.Forms.Timer timer;

        public BrowserFactory(WebBrowserEx webBrowser)
        {
            taskDic = new Dictionary<ActionType, ITask>();

            timer = new System.Windows.Forms.Timer();
            timer.Tick += timer_Tick;
            timer.Interval = 500;
            timer.Enabled = false;

            this.webBrowser = webBrowser;
            this.webBrowser.ScriptErrorsSuppressed = false;
            this.webBrowser.NewWindow3 += webBrowser_NewWindow3;
            
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            if(!string.IsNullOrEmpty(this.webBrowser.NextUrl))
            {
                this.webBrowser.Navigate(this.webBrowser.NextUrl, null, null, string.Format("Referer: {0}\r\n", this.webBrowser.Url.ToString()));
                this.webBrowser.NextUrl = null;
            }
        }

        private void webBrowser_NewWindow3(object sender, WebBrowserNewWindowEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Url))
            {
                e.Cancel = true;

                this.webBrowser.NextUrl = e.Url;
                timer.Enabled = true;
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