using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using xSimulate.Action;
using xSimulate.Browse;
using xSimulate.Configuration;
using xSimulate.Factory;
using xSimulate.Util;
using xSimulate.WebAutomationTasks;

namespace xSimulate
{
    public class AutomationManagement
    {
        private WebBrowserEx webBrowser;
        private System.Windows.Forms.Timer timer;

        private List<ActionStep> actionStepList;

        public AutomationManagement()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Tick += timer_Tick;
            timer.Interval = 10;
            timer.Enabled = false;

            this.webBrowser = new WebBrowserEx();
            this.webBrowser.ScriptErrorsSuppressed = false;
            this.webBrowser.NewWindow3 += webBrowser_NewWindow3;
        }

        public WebBrowser Browser
        {
            get
            {
                return this.webBrowser;
            }
        }

        #region New Window
        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            if(!string.IsNullOrEmpty(this.webBrowser.NextUrl))
            {
                string nextUrl = this.webBrowser.NextUrl;
                this.webBrowser.NextUrl = null;
                this.webBrowser.Navigate(nextUrl, null, null, string.Format("Referer: {0}\r\n", this.webBrowser.Url.ToString()));
                this.webBrowser.Busy = false;
            }
        }

        private void webBrowser_NewWindow3(object sender, WebBrowserNewWindowEventArgs e)
        {
            e.Cancel = true;

            if (!string.IsNullOrEmpty(e.Url))
            {
                this.webBrowser.NextUrl = e.Url;
                this.webBrowser.Busy = true;
                timer.Enabled = true;
            }
        }
        #endregion

        #region Load Config && Convert To IAction
        public void LoadConfig()
        {
            WebAutomationConfig config = WebAutomationConfig.Load();
            LoadConfig(config);
        }

        public void LoadConfig(WebAutomationConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config must not be null");
            }
            if (config.StepList == null)
            {
                return;
            }

            // conver to 
            actionStepList = new List<ActionStep>();

            foreach (AutomationStep stepData in config.StepList)
            {
                if (!stepData.Enabled)
                {
                    continue;
                }

                ActionStep step = ConvertToStep(stepData);
                if (stepData.ActionList != null)
                {
                    foreach (AutomationAction actionData in stepData.ActionList)
                    {
                        if (!actionData.Enabled)
                        {
                            continue;
                        }
                        IAction action = ConvertToAction(actionData);
                        if (step.ActionList == null)
                        {
                            step.ActionList = new List<IAction>();
                        }
                        step.ActionList.Add(action);

                        LoadChildAction(action, actionData);
                    }
                }

                actionStepList.Add(step);
            }
        }

        private void LoadChildAction(IAction action, AutomationAction actionData)
        {
            if (actionData.ChildActionList != null && actionData.ChildActionList.Count > 0)
            {
                foreach (AutomationAction childData in actionData.ChildActionList)
                {
                    if (!childData.Enabled)
                    {
                        continue;
                    }

                    IAction childAction = ConvertToAction(childData);
                    if (action.NextAction == null)
                    {
                        action.NextAction = new List<IAction>();
                    }
                    action.NextAction.Add(childAction);

                    LoadChildAction(childAction, childData);
                }
            }
        }

        private ActionStep ConvertToStep(AutomationStep stepData)
        {
            ActionStep step = new ActionStep();
            step.Name = stepData.Name;
            step.Description = stepData.Description;

            return step;
        }

        private IAction ConvertToAction(AutomationAction actionData)
        {
            return ClassLoader.LoadAction(actionData);
        }
        #endregion

        #region Run
        public void Run()
        {
            Storage.TaskStorage.Clear();

            LoggerManager.Debug("Start Run Step");
            foreach (ActionStep step in this.actionStepList)
            {
                RunStep(step);
            }
        }

        private void RunStep(ActionStep step)
        {
            WaitBrowserBusy();
            LoggerManager.Info("Step:{0}", step.Name);
            
            if (step.ActionList == null || step.ActionList.Count == 0)
            {
                return;
            }

            foreach (IAction action in step.ActionList)
            {
                LoggerManager.Debug("Start Run Action:{0}", action.ActionType);

                RunAction(action);
            }
        }

        private void RunAction(IAction action)
        {
            WaitBrowserBusy();
            ITask task = BrowserFactory.Create(action, this.webBrowser);
            task.Run(action);
            while (!task.IsComplete())
            {
                Application.DoEvents();
                Thread.Sleep(10);
            }

            if (action.HasChild)
            {
                foreach (IAction child in action.NextAction)
                {
                    LoggerManager.Debug("Start Run Child Action:{0}", child.ActionType);
                    RunAction(child);
                }
            }
        }

        private void WaitBrowserBusy()
        {
            if (this.webBrowser.Busy)
            {
                while (this.webBrowser.Busy)
                {
                    Application.DoEvents();
                    Thread.Sleep(10);
                }
            }
        }
        #endregion
    }
}
