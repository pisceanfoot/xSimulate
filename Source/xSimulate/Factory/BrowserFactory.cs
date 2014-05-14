using System.Collections.Generic;
using xSimulate.Action;
using xSimulate.Browse;
using xSimulate.Browser;
using xSimulate.WebAutomationTasks;

namespace xSimulate.Factory
{
    public class BrowserFactory
    {
        private AutomationManagement manager;

        public BrowserFactory(AutomationManagement manager)
        {
            this.manager = manager;
        }

        private Dictionary<ActionType, ITask> taskDic = new Dictionary<ActionType, ITask>();

        public ITask Create(IAction action)
        {
            ITask task = null;

            if (taskDic.TryGetValue(action.ActionType, out task))
            {
                return task;
            }

            if (action.ActionType == ActionType.PageAction)
            {
                task = new PageTask(manager);
            }
            else if (action.ActionType == ActionType.BrowserAction)
            {
                task = new BrowserTask(manager);
            }
            else if (action.ActionType == ActionType.FindAction)
            {
                task = new FindTask(manager);
            }
            else if (action.ActionType == ActionType.MouseAction)
            {
                task = new MouseTask(manager);
            }
            else if (action.ActionType == ActionType.AttributeAction)
            {
                task = new AttributeTask(manager);
            }
            else if (action.ActionType == ActionType.ScrollAction)
            {
                task = new ScrollTask(manager);
            }
            else if (action.ActionType == ActionType.ClearDataAction)
            {
                task = new ClearDataTask(manager);
            }
            else if (action.ActionType == ActionType.WaitAction)
            {
                task = new WaitTask(manager);
            }
            else if (action.ActionType == ActionType.ClickAction)
            {
                task = new ClickTask(manager);
            }
            else if (action.ActionType == ActionType.KeyboardAction)
            {
                task = new KeyboardTask(manager);
            }
            else if (action.ActionType == ActionType.SendKeyAction)
            {
                task = new SendKeyTask(manager);
            }
            else if (action.ActionType == ActionType.ConditionAction)
            {
                task = new ConditionTask(manager);
            }
            else if (action.ActionType == ActionType.ClearHistoryAction)
            {
                task = new ClearHistoryTask(manager);
            }
            else if (action.ActionType == ActionType.TextAction)
            {
                task = new TextTask(manager);
            }
            else if (action.ActionType == ActionType.ScriptAction)
            {
                task = new ScriptTask(manager);
            }
            else if (action.ActionType == ActionType.PackageAction)
            {
                task = new PackageTask(manager);
            }

            taskDic.Add(action.ActionType, task);

            return task;
        }
    }
}