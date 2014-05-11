using xSimulate.Browse;
using xSimulate.Storage;

namespace xSimulate.WebAutomationTasks
{
    public class ClearDataTask : CommonTask
    {
        public ClearDataTask(WebBrowserEx webBrowser)
            : base(webBrowser)
        {
        }

        protected override void OnProcess(Action.IAction action)
        {
            LoggerManager.Debug("Clear TaskStorage.Storage");

            TaskStorage.Clear();
        }
    }
}