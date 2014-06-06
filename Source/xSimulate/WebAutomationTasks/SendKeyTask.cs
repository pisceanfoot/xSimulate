using System.Windows.Forms;
using xSimulate.Action;
using xSimulate.Browse;

namespace xSimulate.WebAutomationTasks
{
    public class SendKeyTask : FindTask
    {
        public SendKeyTask(AutomationManagement manager)
            : base(manager)
        {
        }

        protected override void OnProcess(Action.IAction action)
        {
            SendKeyAction sendKeyAction = action as SendKeyAction;
            if (sendKeyAction == null)
            {
                return;
            }

            LoggerManager.Debug(action.AutomationActionData);

            HtmlElement element = this.GetData(action) as HtmlElement;
            if (element == null)
            {
                LoggerManager.Error("Element Not Found");
                throw new ElementNoFoundException("Element Not Found", action);
            }

            this.Call<HtmlElement>(delegate(HtmlElement e)
            {
                e.Focus();
                SendKey(sendKeyAction);
            }, element);
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