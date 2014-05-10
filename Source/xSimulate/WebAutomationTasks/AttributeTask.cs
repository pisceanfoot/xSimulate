using System.Windows.Forms;
using xSimulate.Action;
using xSimulate.Browse;
using xSimulate.Storage;

namespace xSimulate.WebAutomationTasks
{
    public class AttributeTask : FindTask
    {
        public AttributeTask(WebBrowserEx webBrowser)
            : base(webBrowser)
        {
        }

        protected override void OnProcess(Action.IAction action)
        {
            base.OnProcess(action);

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
                HtmlElementCollection elementCollection = GetData(action) as HtmlElementCollection;
                if (elementCollection != null)
                {
                    SetValue(elementCollection, "value", action.SetValue);
                }
                else
                {
                    HtmlElement element = GetData(action) as HtmlElement;
                    if (element == null)
                    {
                        LoggerManager.Error("Element Not Found");
                        return;
                    }

                    SetValue(element, "value", action.SetValue);
                }
            }
        }

        private void SetValue(HtmlElementCollection elementCollection, string attr, string attrValue)
        {
            foreach (HtmlElement element in elementCollection)
            {
                SetValue(element, attr, attrValue);
            }
        }

        private void SetValue(HtmlElement element, string attr, string attrValue)
        {
            DebugElement(element);
            element.SetAttribute(attr, attrValue);
        }
    }
}