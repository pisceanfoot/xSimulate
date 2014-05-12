using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using xSimulate.Action;
using xSimulate.Browse;
using xSimulate.Util;

namespace xSimulate.WebAutomationTasks
{
    public class FindTask : CommonTask
    {
        private Regex wholeWordRegex = null;

        public FindTask(AutomationManagement manager)
            : base(manager)
        {
        }

        protected override void OnProcess(IAction action)
        {
            FindAction findElementAction = action as FindAction;
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

        private bool AssertEqual(FindAction findElementAction, string x, string y)
        {
            bool result = false;
            if (string.IsNullOrEmpty(x) || string.IsNullOrEmpty(y))
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

        #endregion Help

        #region Context

        private HtmlDocument GetHtmlDocument(FindAction findElementAction)
        {
            if (findElementAction.ActionContext != null)
            {
                HtmlWindow htmlWindow = null;
                if (findElementAction.ActionContext.FrameIndex >= 0)
                {
                    htmlWindow = this.webBrowser.Document.Window.Frames[findElementAction.ActionContext.FrameIndex];
                }
                else if (!string.IsNullOrEmpty(findElementAction.ActionContext.FrameName))
                {
                    htmlWindow = this.webBrowser.Document.Window.Frames[findElementAction.ActionContext.FrameName];
                }

                if (htmlWindow == null)
                {
                    return null;
                }

                //IHTMLWindow2 htmlWindow2 = (IHTMLWindow2)htmlWindow.DomWindow;
                //IHTMLDocument2 document2 = CrossFrameIE.GetDocumentFromWindow(htmlWindow2);
                //mshtml.HTMLDocument d = (mshtml.HTMLDocument)document2;

                return htmlWindow.Document;
            }
            else
            {
                return this.Call<WebBrowserEx, HtmlDocument>(delegate(WebBrowserEx ex)
                {
                    return ex.Document;
                }, this.webBrowser);
            }
        }

        #endregion Context

        #region Combine

        private void FindContidion(FindAction findElementAction)
        {
            LoggerManager.Debug("FindElementTask FindContidion");
            if (!AssertCanFindContidion(findElementAction))
            {
                LoggerManager.Debug("FindElementTask AssertCanFindContidion Error");
                return;
            }

            // TagName type id name xxxx
            HtmlElement findElement = null;

            HtmlElement saveElement = this.GetData(findElementAction) as HtmlElement;
            HtmlElementCollection htmlElementCollection;

            if (saveElement == null)
            {
                HtmlDocument document = GetHtmlDocument(findElementAction);
                if (document == null)
                {
                    LoggerManager.Debug("FindElementTask HtmlDocument document Error");
                    return;
                }

                htmlElementCollection = document.GetElementsByTagName(findElementAction.TagName);
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
                    if (!string.IsNullOrEmpty(findElementAction.Title) && !AssertEqual(findElementAction, element.GetAttribute("title"), findElementAction.Title))
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

                    #endregion Condition Check

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

                    this.SaveData(findElementAction, findElement);
                }
            }
        }

        private bool AssertCanFindContidion(FindAction findElementAction)
        {
            if (string.IsNullOrEmpty(findElementAction.TagName))
            {
                return false;
            }

            if (string.IsNullOrEmpty(findElementAction.ID) &&
                string.IsNullOrEmpty(findElementAction.Type) &&
                string.IsNullOrEmpty(findElementAction.Title) &&
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

        #endregion Combine

        #region Find ID

        private void FindID(FindAction findElementAction)
        {
            if (!string.IsNullOrEmpty(findElementAction.ID))
            {
                LoggerManager.Debug("FindElementTask FindID");

                HtmlElement element = this.GetData(findElementAction) as HtmlElement;
                if (element == null)
                {
                    HtmlDocument document = GetHtmlDocument(findElementAction);
                    if (document == null)
                    {
                        LoggerManager.Debug("FindElementTask HtmlDocument document Error");
                        return;
                    }

                    element = document.GetElementById(findElementAction.ID);
                }
                else
                {
                    element = FindIDRecusive(element, findElementAction.ID);
                }

                this.SaveData(findElementAction, element);
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

        #endregion Find ID

        #region Find ClassName

        private void FindClassName(FindAction findElementAction)
        {
            if (string.IsNullOrEmpty(findElementAction.ClassName))
            {
                return;
            }

            LoggerManager.Debug("FindElementTask FindClassName");

            wholeWordRegex = new Regex(string.Format("\\b{0}\\b", findElementAction.ClassName));

            HtmlElement element = this.GetData(findElementAction) as HtmlElement;
            if (element == null)
            {
                HtmlDocument document = GetHtmlDocument(findElementAction);
                if (document == null)
                {
                    LoggerManager.Debug("FindElementTask HtmlDocument document Error");
                    return;
                }

                element = document.Body;
                this.SaveData(findElementAction, FindClassRecusive(element, findElementAction.ClassName));
            }
            else
            {
                this.SaveData(findElementAction, FindClassRecusive(element, findElementAction.ClassName));
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
                    HtmlElement find = FindClassRecusive(child, className);
                    if (find != null)
                    {
                        return find;
                    }
                }
            }

            return null;
        }

        #endregion Find ClassName

        #region Find Url

        private void FindUrl(FindAction findElementAction)
        {
            if (string.IsNullOrEmpty(findElementAction.Url))
            {
                return;
            }

            LoggerManager.Debug("FindElementTask FindUrl");

            HtmlElement element = this.GetData(findElementAction) as HtmlElement;
            if (element == null)
            {
                HtmlDocument document = GetHtmlDocument(findElementAction);
                if (document == null)
                {
                    LoggerManager.Debug("FindElementTask HtmlDocument document Error");
                    return;
                }

                element = document.Body;
            }

            HtmlElementCollection elementCollection = element.GetElementsByTagName("a");
            if (elementCollection != null && elementCollection.Count > 0)
            {
                foreach (HtmlElement find in elementCollection)
                {
                    if (find.GetAttribute("href") == findElementAction.Url)
                    {
                        this.SaveData(findElementAction, find);
                        break;
                    }
                }
            }
        }

        #endregion Find Url

        #region XPath

        private void FindXPath(FindAction findElementAction)
        {
            if (string.IsNullOrEmpty(findElementAction.XPath))
            {
                return;
            }

            LoggerManager.Debug("FindElementTask FindXPath");

            HtmlElement element = this.GetData(findElementAction) as HtmlElement;
            if (element == null)
            {
                HtmlDocument document = GetHtmlDocument(findElementAction);
                if (document == null)
                {
                    LoggerManager.Debug("FindElementTask HtmlDocument document Error");
                    return;
                }

                element = document.Body;
            }

            element = HtmlHelp.SelectHtmlNode(findElementAction.XPath, element);
            this.SaveData(findElementAction, element);
        }

        #endregion XPath
    }
}