using xSimulate.Action;
using xSimulate.Browse;

namespace xSimulate.WebAutomationTasks
{
    public class ScriptTask : CommonTask
    {
        public ScriptTask(AutomationManagement manager)
            : base(manager)
        {
        }

        protected override void OnProcess(Action.IAction action)
        {
            ScriptAction scriptAction = action as ScriptAction;
            if (scriptAction == null)
            {
                return;
            }

            LoggerManager.Debug(action.AutomationActionData);

            if (!string.IsNullOrEmpty(scriptAction.ScriptContent))
            {
                object[] args = new object[1];
                args[0] = scriptAction.ScriptContent;

                this.Call<WebBrowserEx>(delegate(WebBrowserEx ex)
                {
                    ex.Document.InvokeScript("eval", args);
                }, this.webBrowser);
                
            }
        }
    }
}