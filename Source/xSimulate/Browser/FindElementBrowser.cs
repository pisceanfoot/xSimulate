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
        private FindElementAction action;

        public FindElementBrowser(WebBrowser webBrowser, FindElementAction action)
            : base(webBrowser)
        {
            this.action = action;
        }

        public override void Run()
        {
            if (!string.IsNullOrEmpty(this.action.ID))
            {
                this.action.Element = this.webBrowser.Document.GetElementById(this.action.ID);
            }
        }
    }
}
