using System.Threading;
using System.Windows.Forms;
using xSimulate.Action;
using xSimulate.Browse;

namespace xSimulate.WebAutomationTasks
{
    public class WaitTask : CommonTask
    {
        public WaitTask(WebBrowserEx webBrowser)
            : base(webBrowser)
        {
        }

        protected override void OnProcess(Action.IAction action)
        {
            WaitAction waitAction = action as WaitAction;
            if (waitAction == null)
            {
                return;
            }

            LoggerManager.Debug("WaitTask");

            if (waitAction.Seconds > 0)
            {
                Sleep(waitAction.Seconds * 1000);
            }
            if (waitAction.Milliseconds > 0)
            {
                Sleep(waitAction.Milliseconds);
            }
        }

        private void Sleep(int wait)
        {
            Thread.Sleep(wait);
            //int count = 0;
            //int step = 10;

            //while (count <= wait)
            //{
            //    //Application.DoEvents();
            //    Thread.Sleep(step);

            //    count += step;
            //    if (count < wait && count + step > wait)
            //    {
            //        step = wait - count;
            //    }
            //}
        }
    }
}