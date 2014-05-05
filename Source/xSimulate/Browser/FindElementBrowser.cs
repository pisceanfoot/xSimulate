using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xSimulate.Action;

namespace xSimulate.Browser
{
    public class FindElementBrowser : BrowserBase
    {
        public FindElementBrowser(WebBrowser webBrowser)
            : base(webBrowser)
        {
        }

        public override void Run(IAction action)
        {
            FindElementAction findElementAction = action as FindElementAction;
            if (!string.IsNullOrEmpty(findElementAction.ID))
            {
                BrowserStorage.Storage = this.webBrowser.Document.GetElementById(findElementAction.ID);
            }
        }
    }
}
