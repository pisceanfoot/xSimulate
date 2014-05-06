using System.Windows.Forms;
using xSimulate.Action;
using xSimulate.Browse;
using xSimulate.Storage;

namespace xSimulate.WebAutomationTasks
{
    public class AttributeTask : CommonTask
    {
        public AttributeTask(WebBrowserEx webBrowser)
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
                HtmlElement element = TaskStorage.Storage as HtmlElement;
                if (element == null)
                {
                    return;
                }

                element.SetAttribute("value", action.SetValue);
            }
        }
    }
}