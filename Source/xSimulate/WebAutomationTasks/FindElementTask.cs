﻿using System.Windows.Forms;
using System.Linq;

using xSimulate.Action;
using xSimulate.Storage;
using xSimulate.WebAutomationTasks;
using System.Text.RegularExpressions;
using xSimulate.Browse;
using System.Collections.Generic;
using xSimulate.Util;

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

            if (findElementAction.Combine)
            {
                FindContidion(findElementAction);
            }
            else
            {
                FindID(findElementAction);
                FindClassName(findElementAction);
                FindUrl(findElementAction);
                FindXPath(findElementAction);
            }
        }

        #region Help
        private bool AssertEqual(FindElementAction findElementAction, string x, string y)
        {
            bool result = false;
            if (string.IsNullOrEmpty(x) == null || string.IsNullOrEmpty(y) == null)
            {
                return result;
            }

            if (findElementAction.Trim)
            {
                x = x.Trim();
            }

            if (findElementAction.Contains)
            {
                result = x.Contains(y);
            }
            else if (findElementAction.WildCard)
            {
                result = WildCard.Test(y, x);
            }
            else
            {
                result = x == y;
            }

            return result;
        }
        #endregion

        #region Combine
        private void FindContidion(FindElementAction findElementAction)
        {
            LoggerManager.Debug("FindElementTask FindContidion");
            if (!AssertCanFindContidion(findElementAction))
            {
                LoggerManager.Debug("FindElementTask AssertCanFindContidion Error");
                return;
            }

            // TagName type id name xxxx
            HtmlElement findElement = null;

            HtmlElement saveElement = TaskStorage.Storage as HtmlElement;
            HtmlElementCollection htmlElementCollection;

            if (saveElement == null)
            {
                htmlElementCollection = this.webBrowser.Document.GetElementsByTagName(findElementAction.TagName);
            }
            else
            {
                htmlElementCollection = saveElement.GetElementsByTagName(findElementAction.TagName);
            }

            if (htmlElementCollection != null && htmlElementCollection.Count > 0)
            {
                List<HtmlElement> tmpHtmlElementList = new List<HtmlElement>();
                foreach (HtmlElement element in htmlElementCollection)
                {
                    #region Condition Check
                    if (!string.IsNullOrEmpty(findElementAction.ID) && !AssertEqual(findElementAction, element.Id, findElementAction.ID))
                    {
                        continue;
                    }
                    if (!string.IsNullOrEmpty(findElementAction.Type) && !AssertEqual(findElementAction, element.GetAttribute("type"), findElementAction.Type))
                    {
                        continue;
                    }
                    if (!string.IsNullOrEmpty(findElementAction.ClassName) && !AssertEqual(findElementAction, element.GetAttribute("className"), findElementAction.ClassName))
                    {
                        continue;
                    }
                    if (!string.IsNullOrEmpty(findElementAction.Name) && !AssertEqual(findElementAction, element.Name, findElementAction.Name))
                    {
                        continue;
                    }
                    if (!string.IsNullOrEmpty(findElementAction.Url) && !AssertEqual(findElementAction, element.GetAttribute("href"), findElementAction.Url))
                    {
                        continue;
                    }
                    if (!string.IsNullOrEmpty(findElementAction.InnerText) && !AssertEqual(findElementAction, element.InnerText, findElementAction.InnerText))
                    {
                        continue;
                    }

                    if (findElementAction.EmbedAttribute != null && findElementAction.EmbedAttribute.Count > 0)
                    {
                        bool find = true;
                        Dictionary<string, string>.Enumerator e = findElementAction.EmbedAttribute.GetEnumerator();
                        while (e.MoveNext())
                        {
                            if (!AssertEqual(findElementAction, element.GetAttribute(e.Current.Key), e.Current.Value))
                            {
                                find = false;
                                break;
                            }
                        }

                        if (!find)
                        {
                            continue;
                        }
                    }
                    #endregion

                    tmpHtmlElementList.Add(element);
                }

                if (tmpHtmlElementList.Count > 0)
                {
                    if (tmpHtmlElementList.Count > 1)
                    {
                        if (findElementAction.Index != -1)
                        {
                            findElement = tmpHtmlElementList[findElementAction.Index];
                        }
                        else
                        {
                            // TODO: MUTI FIND
                        }
                    }
                    else
                    {
                        findElement = tmpHtmlElementList[0];
                    }

                    TaskStorage.Storage = findElement;
                }
            }
        }

        private bool AssertCanFindContidion(FindElementAction findElementAction)
        {
            if (string.IsNullOrEmpty(findElementAction.TagName))
            {
                return false;
            }

            if (string.IsNullOrEmpty(findElementAction.ID) &&
                string.IsNullOrEmpty(findElementAction.Type) &&
                string.IsNullOrEmpty(findElementAction.ClassName) &&
                string.IsNullOrEmpty(findElementAction.Name) &&
                string.IsNullOrEmpty(findElementAction.Url) &&
                string.IsNullOrEmpty(findElementAction.InnerText) &&
                (findElementAction.EmbedAttribute == null || findElementAction.EmbedAttribute.Count == 0))
            {
                return false;
            }

            return true;
        }
        #endregion

        #region Find ID
        private HtmlElement FindID(FindElementAction findElementAction)
        {
            HtmlElement element = null;
            if (!string.IsNullOrEmpty(findElementAction.ID))
            {
                LoggerManager.Debug("FindElementTask FindID");

                element = TaskStorage.Storage as HtmlElement;
                if (element == null)
                {
                    element = this.webBrowser.Document.GetElementById(findElementAction.ID);
                }
                else
                {
                    element = FindIDRecusive(element, findElementAction.ID);    
                }

                TaskStorage.Storage = element;
            }

            return element;
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