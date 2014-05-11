using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xSimulate.Action;
using xSimulate.Browse;

namespace xSimulate.WebAutomationTasks
{
    public class ConditionTask : CommonTask
    {
        public ConditionTask(WebBrowserEx webBrowser)
            : base(webBrowser)
        {
        }

        protected override void OnProcess(Action.IAction action)
        {
        }

        public override bool CanChildRun(IAction action)
        {
            ConditionAction conditionAction = action as ConditionAction;

            HtmlElement element = this.GetData(action) as HtmlElement;
            return element != null;
        }
    }
}
