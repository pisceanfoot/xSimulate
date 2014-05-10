using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xSimulate.Action;
using xSimulate.Browse;
using xSimulate.Storage;

namespace xSimulate.WebAutomationTasks
{
    public class SendKeyTask : FindTask
    {
        public SendKeyTask(WebBrowserEx webBrowser)
            : base(webBrowser)
        {
        }

        protected override void OnProcess(Action.IAction action)
        {
            SendKeyAction sendKeyAction = action as SendKeyAction;
            if (sendKeyAction == null)
            {
                return;
            }

            HtmlElement element = this.GetData(action) as HtmlElement;
            if (element == null)
            {
                LoggerManager.Error("Element Not Found");
                return;
            }

            element.Focus();
            SendKey(sendKeyAction);
        }

        private void SendKey(SendKeyAction sendKeyAction)
        {
            if (sendKeyAction.Keys != null && sendKeyAction.Keys.Count > 0)
            {
                foreach (string key in sendKeyAction.Keys)
                {
                    System.Windows.Forms.SendKeys.Send(key);
                }
            }
        }
    }
}
