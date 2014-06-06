using System;
using System.Collections.Generic;
using System.Text;
using xSimulate.Action;
using xSimulate.Browse;

namespace xSimulate.WebAutomationTasks
{
    public class TimerTask : CommonTask
    {
        private DateTime endDate = DateTime.MinValue;

        public TimerTask(AutomationManagement manager)
            : base(manager)
        {
        }

        protected override void OnProcess(Action.IAction action)
        {
            TimerAction timerAction = action as TimerAction;
            if (timerAction == null)
            {
                return;
            }

            LoggerManager.Debug(action.AutomationActionData);

            if (endDate == DateTime.MinValue)
            {
                endDate = DateTime.Now.AddSeconds(timerAction.Seconds);
            }
        }

        public override bool IsComplete()
        {
            if (DateTime.Now >= endDate)
            {
                endDate = DateTime.MinValue;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool ChildComplete(Action.IAction action)
        {
            if (DateTime.Now < endDate)
            {
                return false;
            }

            endDate = DateTime.MinValue;
            return true;
        }
    }
}
