using xSimulate.Action;
using xSimulate.Browse;

namespace xSimulate.WebAutomationTasks
{
    public class ScriptTask : CommonTask
    {
        public ScriptTask(WebBrowserEx webBrowser)
            : base(webBrowser)
        {
        }

        protected override void OnProcess(Action.IAction action)
        {
            ScriptAction scriptAction = action as ScriptAction;
            if (scriptAction == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(scriptAction.ScriptContent))
            {
                object[] args = new object[1];
                args[0] = scriptAction.ScriptContent;

                this.webBrowser.Document.InvokeScript("eval", args);
            }
        }
    }
}