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
        private static Dictionary<ActionType, ITask> taskDic = new Dictionary<ActionType,ITask>();

        public static ITask Create(IAction action, WebBrowserEx webBrowser)
        {
            ITask task = null;

            if (taskDic.TryGetValue(action.ActionType, out task))
            {
                return task;
            }

            if (action.ActionType == ActionType.PageAction)
            {
                task = new PageTask(webBrowser);
            }
            else if (action.ActionType == ActionType.BrowserAction)
            {
                task = new BrowserTask(webBrowser);
            }
            else if (action.ActionType == ActionType.FindAction)
            {
                task = new FindTask(webBrowser);
            }
            else if (action.ActionType == ActionType.MouseAction)
            {
                task = new MouseTask(webBrowser);
            }
            else if (action.ActionType == ActionType.AttributeAction)
            {
                task = new AttributeTask(webBrowser);
            }
            else if (action.ActionType == ActionType.ScrollAction)
            {
                task = new ScrollTask(webBrowser);
            }
            else if (action.ActionType == ActionType.ClearDataAction)
            {
                task = new ClearDataTask(webBrowser);
            }
            else if (action.ActionType == ActionType.WaitAction)
            {
                task = new WaitTask(webBrowser);
            }
            else if (action.ActionType == ActionType.ClickAction)
            {
                task = new ClickTask(webBrowser);
            }
            else if (action.ActionType == ActionType.KeyboardAction)
            {
                task = new KeyboardTask(webBrowser);
            }
            else if (action.ActionType == ActionType.SendKeyAction)
            {
                task = new SendKeyTask(webBrowser);
            }
            else if (action.ActionType == ActionType.ConditionAction)
            {
                task = new ConditionTask(webBrowser);
            }

            taskDic.Add(action.ActionType, task);

            return task;
        }
    }
}