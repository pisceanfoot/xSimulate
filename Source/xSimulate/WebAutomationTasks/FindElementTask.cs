using System.Windows.Forms;
using System.Linq;

using xSimulate.Action;
using xSimulate.Storage;
using xSimulate.WebAutomationTasks;
using System.Text.RegularExpressions;
using xSimulate.Browse;

namespace xSimulate.WebAutomationTasks
{
    public class FindElementTask : CommonTask
    {
        private Regex wholeWordRegex = null;

        public FindElementTask(WebBrowserEx webBrowser)
            : base(webBrowser)
        {
        }

        protected override void OnProcess(IAction action)
        {
            FindElementAction findElementAction = action as FindElementAction;
            if (findElementAction == null)
            {
                return;
            }

            FindID(findElementAction);
            FindClassName(findElementAction);
            FindUrl(findElementAction);
            FindXPath(findElementAction);
        }

        #region Find ID
        private void FindID(FindElementAction findElementAction)
        {
            if (!string.IsNullOrEmpty(findElementAction.ID))
            {
                LoggerManager.Debug("FindElementTask FindID");

                HtmlElement element = TaskStorage.Storage as HtmlElement;
                if (element == null)
                {
                    TaskStorage.Storage = this.webBrowser.Document.GetElementById(findElementAction.ID);
                }
                else
                {
                    TaskStorage.Storage = FindIDRecusive(element, findElementAction.ID);    
                }
            }
        }

        private HtmlElement FindIDRecusive(HtmlElement element, string id)
        {
            if (element == null)
            {
                return null;
            }

            if (element.Id == id)
            {
                return element;
            }

            if (element.Children != null && element.Children.Count > 0)
            {
                foreach (HtmlElement child in element.Children)
                {
                    HtmlElement find = FindIDRecusive(child, id);
                    if (find != null)
                    {
                        return find;
                    }
                }
            }

            return null;
        }
        #endregion

        #region Find ClassName
        private void FindClassName(FindElementAction findElementAction)
        {
            if (string.IsNullOrEmpty(findElementAction.ClassName))
            {
                return;
            }

            LoggerManager.Debug("FindElementTask FindClassName");

            wholeWordRegex = new Regex(string.Format("\\b{0}\\b", findElementAction.ClassName));

            HtmlElement element = TaskStorage.Storage as HtmlElement;
            if (element == null)
            {
                element = this.webBrowser.Document.Body;
                TaskStorage.Storage = FindClassRecusive(element, findElementAction.ClassName);
            }
            else
            {
                TaskStorage.Storage = FindClassRecusive(element, findElementAction.ClassName);
            }
        }

        private HtmlElement FindClassRecusive(HtmlElement element, string className)
        {
            if (element == null)
            {
                return null;
            }

            string elementClassName = element.GetAttribute("className");
            if (wholeWordRegex.Match(elementClassName).Success)
            {
                return element;
            }

            if (element.Children != null && element.Children.Count > 0)
            {
                foreach (HtmlElement child in element.Children)
                {
                    HtmlElement find =  FindClassRecusive(child, className);
                    if (find != null)
                    {
                        return find;
                    }
                }
            }

            return null;
        }
        #endregion

        #region Find Url
        private void FindUrl(FindElementAction findElementAction)
        {
            if (string.IsNullOrEmpty(findElementAction.Url))
            {
                return;
            }

            LoggerManager.Debug("FindElementTask FindUrl");

            HtmlElement element = TaskStorage.Storage as HtmlElement;
            if (element == null)
            {
                element = this.webBrowser.Document.Body;
            }

            HtmlElementCollection elementCollection = element.GetElementsByTagName("a");
            if (elementCollection != null && elementCollection.Count > 0)
            {
                foreach (HtmlElement find in elementCollection)
                {
                    if (find.GetAttribute("href") == findElementAction.Url)
                    {
                        TaskStorage.Storage = find;
                        break;
                    }
                }
            }
        }
        #endregion

        #region XPath
        private void FindXPath(FindElementAction findElementAction)
        {
            if (string.IsNullOrEmpty(findElementAction.XPath))
            {
                return;
            }

            LoggerManager.Debug("FindElementTask FindXPath");

            HtmlElement element = TaskStorage.Storage as HtmlElement;
            if (element == null)
            {
                element = this.webBrowser.Document.Body;
            }

            TaskStorage.Storage = HtmlHelp.SelectHtmlNode(findElementAction.XPath, element);
        }
        #endregion
    }
}