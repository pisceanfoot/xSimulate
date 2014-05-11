using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
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
        private WebBrowserTimer timer;
        private BackgroundWorker backgroundWorker;

        private List<ActionStep> actionStepList;

        public AutomationManagement()
        {
            this.webBrowser = new WebBrowserEx();
            this.webBrowser.ScriptErrorsSuppressed = false;
            this.webBrowser.NewWindow3 += webBrowser_NewWindow3;

            timer = new WebBrowserTimer(this.webBrowser);
            timer.Tick += timer_Tick;
            timer.Interval = 10;
            timer.Enabled = false;

            this.backgroundWorker = new BackgroundWorker();
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += backgroundWorker_DoWork;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Storage.TaskStorage.Clear();

            LoggerManager.Debug("Start Run Step");
            foreach (ActionStep step in this.actionStepList)
            {
                //Application.DoEvents();
                RunStep(step);
            }
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
            if (!string.IsNullOrEmpty(this.webBrowser.NextUrl))
            {
                string nextUrl = this.webBrowser.NextUrl;
                this.webBrowser.NextUrl = null;
                this.webBrowser.Navigate(nextUrl, null, null, string.Format("Referer: {0}\r\n", this.webBrowser.Url.ToString()));
                //this.webBrowser.Busy = false;
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

        #endregion New Window

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
                    if (action.ChildAction == null)
                    {
                        action.ChildAction = new List<IAction>();
                    }
                    action.ChildAction.Add(childAction);

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

        #endregion Load Config && Convert To IAction

        #region Run

        public void Run()
        {
            Stop();
            
            this.backgroundWorker.RunWorkerAsync();
        }

        public void Stop()
        {
            this.backgroundWorker.CancelAsync();
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
            //Application.DoEvents();

            WaitBrowserBusy();
            ITask task = BrowserFactory.Create(action, this.webBrowser);
            task.Run(action);
            while (!task.IsComplete())
            {
                //Application.DoEvents();
                Thread.Sleep(100);
            }

            if (action.HasChild && task.CanChildRun(action))
            {
                foreach (IAction child in action.ChildAction)
                {
                    LoggerManager.Debug("Start Run Child Action:{0}", child.ActionType);
                    RunAction(child);
                }

                if (!task.ChildComplete(action))
                {
                    // retry
                    LoggerManager.Debug("Regry Action:{0}", action.ActionType);
                    RunAction(action);
                }
            }
        }

        private void WaitBrowserBusy()
        {
            if (this.webBrowser.Busy)
            {
                while (this.webBrowser.Busy)
                {
                    //Application.DoEvents();
                    Thread.Sleep(10);
                }
            }
        }

        #endregion Run
    }
}