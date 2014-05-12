using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using xSimulate.Action;
using xSimulate.Browse;
using xSimulate.Configuration;
using xSimulate.Factory;
using xSimulate.Storage;
using xSimulate.Util;
using xSimulate.WebAutomationTasks;

namespace xSimulate
{
    public class AutomationManagement
    {
        #region Field
        private WebBrowserEx webBrowser;
        private TaskStorage storage;
        private BrowserFactory factory;
        private WebBrowserTimer timer;
        private BackgroundWorker backgroundWorker;

        private List<ActionStep> actionStepList;
        #endregion

        #region Event
        public event MessageHandle<string, string> ErrorMessage;

        public event MessageHandle<string, string> PageChanged;

        public event MessageHandle<string, string> RuningInfo;
        #endregion

        public AutomationManagement()
        {
            this.storage = new TaskStorage();
            this.factory = new BrowserFactory(this);

            this.webBrowser = new WebBrowserEx();
            this.webBrowser.ScriptErrorsSuppressed = false;
            this.webBrowser.NewWindow3 += webBrowser_NewWindow3;
            this.webBrowser.DocumentCompleted += webBrowser_DocumentCompleted;

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
            ClearData();

            LoggerManager.Debug("Start Run Step");
            foreach (ActionStep step in this.actionStepList)
            {
                
                RunStep(step);
            }

            //Application.Exit();
        }

        public WebBrowser Browser
        {
            get
            {
                return this.webBrowser;
            }
        }

        #region Document Complete
        private string lastUrl = null;
        private string lastTitle = null;
        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if(this.webBrowser.ReadyState == WebBrowserReadyState.Complete && this.lastUrl != e.Url.ToString())
            {
                if (this.PageChanged != null)
                {
                    this.PageChanged(lastTitle, this.webBrowser.DocumentTitle);
                }
            }
        }
        #endregion

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
                        LoadConditionAction(action, actionData);
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

        private void LoadConditionAction(IAction action, AutomationAction actionData)
        {
            if (actionData.ConditionActionList != null && actionData.ConditionActionList.Count > 0)
            {
                foreach (AutomationAction childData in actionData.ConditionActionList)
                {
                    if (!childData.Enabled)
                    {
                        continue;
                    }

                    IAction childAction = ConvertToAction(childData);
                    if (action.ConditoinAction == null)
                    {
                        action.ConditoinAction = new List<IAction>();
                    }
                    action.ConditoinAction.Add(childAction);

                    LoadConditionAction(childAction, childData);
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

        #region Action
        public ITask BuildTask(IAction action)
        {
            return factory.Create(action);
        }
        #endregion

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

        public void RunStep(ActionStep step)
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

                try
                {
                    RunAction(action);
                }
                catch(ElementNoFoundException ex)
                {
                    // report fatal
                    if (ErrorMessage != null)
                    {
                        ErrorMessage(ex.Message, ex.Action.AutomationActionData.ToString());
                    }

                    return;
                }
            }
        }

        public void RunAction(IAction action)
        {
            WaitBrowserBusy();
            ITask task = BuildTask(action);
            task.Run(action);
            while (!task.IsComplete())
            {
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
                    Thread.Sleep(100);
                }
            }
        }
        #endregion Run

        #region Storage
        public void SaveData(object value)
        {
            this.storage.Storage = value;
        }

        public void SaveData(string key, object value)
        {
            this.storage.SetKey(key, value);
        }

        public object GetData()
        {
            return this.storage.Storage;
        }

        public object GetData(string key)
        {
            return this.storage.GetKey(key);
        }

        public void ClearData()
        {
            this.storage.Clear();
        }
        #endregion
    }
}