using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xSimulate.Action;

namespace xSimulate.Browser
{
    public class BrowserFactory
    {
        private WebBrowser webBrowser;
        private List<IBrowser> browserList;

        public BrowserFactory(WebBrowser webBrowser)
        {
            browserList = new List<IBrowser>();

            this.webBrowser = webBrowser;
        }

        public IBrowser Create(IAction action)
        {
            IBrowser browser = null;
            if (action is PageAction)
            {
                browser = new PageBrowser(this.webBrowser, action as PageAction);
            }
            else if (action is FindElementAction)
            {
                browser = new FindElementBrowser(this.webBrowser, action as FindElementAction);
            }

            return browser;
        }
    }
}
