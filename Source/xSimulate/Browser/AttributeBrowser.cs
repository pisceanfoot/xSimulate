using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xSimulate.Action;

namespace xSimulate.Browser
{
    public class AttributeBrowser : BrowserBase
    {
        public AttributeBrowser(WebBrowser webBrowser)
            : base(webBrowser)
        {
        }

        public override void Run(Action.IAction action)
        {
            AttributeAction attributeAction = action as AttributeAction;
            if (attributeAction == null)
            {
                return;
            }

            SetValue(attributeAction);
        }

        private void SetValue(AttributeAction action)
        {
            if (!string.IsNullOrEmpty(action.SetValue))
            {
                HtmlElement element = BrowserStorage.Storage as HtmlElement;
                if (element == null)
                {
                    return;
                }

                element.SetAttribute("value", action.SetValue);
            }
        }
    }
}
