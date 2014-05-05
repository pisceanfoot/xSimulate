using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xSimulate.Action;

namespace xSimulate.Browser
{
    public class MouseBrowser : BrowserBase
    {
        public MouseBrowser(WebBrowser webBrowser)
            : base(webBrowser)
        {
        }

        public override void Run(IAction action)
        {
            HtmlElement element = BrowserStorage.Storage as HtmlElement;
            if (element == null)
            {
                return;
            }

            MouseAction mouseAction = action as MouseAction;
            if (mouseAction == null)
            {
                return;
            }

            if (mouseAction.Click)
            {
                Click(element);
            }
        }
        public void Click(HtmlElement h)
        {
            Over(h);
            Down(h);
            h.InvokeMember("click");
        }

        public void Over(HtmlElement h)
        {
            h.InvokeMember("fireEvent", new object[] { "onmouseover" });
        }

        public void Down(HtmlElement h)
        {
            h.InvokeMember("fireEvent", new object[] { "onmousedown" });
        }
    }
}
