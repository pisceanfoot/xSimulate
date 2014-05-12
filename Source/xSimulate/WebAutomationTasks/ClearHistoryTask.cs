using System.Diagnostics;
using System.Threading;
using xSimulate.Action;
using xSimulate.Util;

namespace xSimulate.WebAutomationTasks
{
    public class ClearHistoryTask : CommonTask
    {
        public ClearHistoryTask(AutomationManagement manager)
            : base(manager)
        {
        }

        protected override void OnProcess(Action.IAction action)
        {
            LoggerManager.Debug("ClearHistoryTask");

            ClearHistoryAction clearHistoryAction = action as ClearHistoryAction;

            if (clearHistoryAction.ClearHistoryType == ClearHistoryType.Cookie)
            {
                Win32API.IE_ClearCookie();
            }
            else if (clearHistoryAction.ClearHistoryType == ClearHistoryType.History)
            {
                Win32API.IE_ClearHistory();
            }
            else if (clearHistoryAction.ClearHistoryType == ClearHistoryType.All)
            {
                Win32API.IE_ClearAll();
            }
            else if (clearHistoryAction.ClearHistoryType == ClearHistoryType.AllPlus)
            {
                Win32API.IE_ClearAllPlus();
            }
        }

        public override bool IsComplete()
        {
            int num = 0;
            do
            {
                if (Process.GetProcessesByName("rundll32").Length == 0)
                {
                    return true;
                }
                Thread.Sleep(100);
                num += 100;
            }
            while (num <= 10000);

            return true;
        }
    }
}