using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using xSimulate.Action;

namespace xSimulate.Browser
{
    public class BrowserFactory
    {
        private WebBrowser webBrowser;
        private Dictionary<ActionType, IBrowser> browserDic;

        public BrowserFactory(WebBrowser webBrowser)
        {
            browserDic = new Dictionary<ActionType, IBrowser>();

            this.webBrowser = webBrowser;
            this.webBrowser.ScriptErrorsSuppressed = false;
        }

        public void Run(IAction action)
        {
            IBrowser browser = Create(action);
            browser.Run(action);
            while (!browser.IsComplete())
            {
                Application.DoEvents();
                Thread.Sleep(500);
            }

            if (action.NextAction != null && action.NextAction.Count > 0)
            {
                foreach (IAction nextAction in action.NextAction)
                {
                    Run(nextAction);
                }
            }
        }

        public IBrowser Create(IAction action)
        {
            IBrowser browser;
            browserDic.TryGetValue(action.ActionType, out browser);
            if (browser != null)
            {
                return browser;
            }

            if (action.ActionType == ActionType.PageAction)
            {
                browser = new PageBrowser(this.webBrowser);
            }
            else if (action.ActionType == ActionType.FindElementAction)
            {
                browser = new FindElementBrowser(this.webBrowser);
            }
            else if (action.ActionType == ActionType.MouseAction)
            {
                browser = new MouseBrowser(this.webBrowser);
            }
            else if (action.ActionType == ActionType.AttributeAction)
            {
                browser = new AttributeBrowser(this.webBrowser);
            }
            else if (action.ActionType == ActionType.ScrollAction)
            {
                browser = new ScrollBrowser(this.webBrowser);
            }

            browserDic.Add(action.ActionType, browser);
            return browser;
        }
    }
}
