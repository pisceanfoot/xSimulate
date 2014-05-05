using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xSimulate.Action;

namespace xSimulate.Browser
{
    public class PageBrowser : BrowserBase
    {
        public PageBrowser(WebBrowser webBrowser)
            : base(webBrowser)
        {
        }

        public override void Run(IAction action)
        {
            PageAction pageAction = action as PageAction;
            webBrowser.Navigate(pageAction.Uri);
        }

        public override bool IsComplete()
        {
            return this.webBrowser.ReadyState == WebBrowserReadyState.Complete;
        }
    }
}
