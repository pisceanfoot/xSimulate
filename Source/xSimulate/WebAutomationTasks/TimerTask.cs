using System;
using System.Collections.Generic;
using System.Text;
using xSimulate.Browse;

namespace xSimulate.WebAutomationTasks
{
    public class TimerTask : CommonTask
    {
        public TimerTask(WebBrowserEx webBrowser)
            : base(webBrowser)
        {
        }

        protected override void OnProcess(Action.IAction action)
        {
        }

        public override bool ChildComplete(Action.IAction action)
        {
            return true;
        }
    }
}
