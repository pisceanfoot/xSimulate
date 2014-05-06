using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        public override void Run(Action.IAction action)
        {
            TaskStorage.Storage = null;
        }
    }
}
