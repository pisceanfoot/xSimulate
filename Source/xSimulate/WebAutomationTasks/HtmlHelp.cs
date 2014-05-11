using System.Text.RegularExpressions;
using System.Windows.Forms;
using mshtml;

namespace xSimulate.WebAutomationTasks
{
    public class HtmlHelp
    {
        public static IHTMLRect GetLocation(HtmlElement he)
        {
            mshtml.IHTMLElement2 domElement = (mshtml.IHTMLElement2)he.DomElement;
            return domElement.getBoundingClientRect();
        }

        /// <summary>
        /// Use XPath
        /// </summary>
        /// <example>
        /// c#
        /// HtmlElement currentElement = SelectHtmlNode("/body/form/div/div[2]/div/div/input", webBrowser.Document.GetElementsByTagName("html")[0]);
        /// currentElement.SetAttribute("Value", "hello world");
        /// </example>
        /// <param name="xPath"></param>
        /// <param name="htmlElement"></param>
        /// <returns></returns>
        public static HtmlElement SelectHtmlNode(string xPath, HtmlElement htmlElement)
        {
            string currentNode;
            int indexOfElement;

            //get string representation of current Tag.
            if (xPath.Substring(1, xPath.Length - 2).Contains("/"))
                currentNode = xPath.Substring(1, xPath.IndexOf('/', 1) - 1);
            else
                currentNode = xPath.Substring(1, xPath.Length - 1);
            //gets the depth of current xPath
            int numOfOccurence = Regex.Matches(xPath, "/").Count;

            //gets the children's index
            int.TryParse(Regex.Match(currentNode, @"\d+").Value, out indexOfElement);

            //if i have to select nth-child ex: /tr[4]
            if (indexOfElement > 1)
            {
                currentNode = currentNode.Substring(0, xPath.IndexOf('[') - 1);
                //the tag that i want to get
                if (numOfOccurence == 1 || numOfOccurence == 0)
                {
                    return htmlElement.Children[indexOfElement - 1];
                }
                //still has some children tags
                if (numOfOccurence > 1)
                {
                    int i = 1;
                    //select nth-child
                    foreach (HtmlElement tempElement in htmlElement.Children)
                    {
                        if (tempElement.TagName.ToLower() == currentNode && i == indexOfElement)
                        {
                            return SelectHtmlNode(xPath.Substring(xPath.IndexOf('/', 1)), tempElement);
                        }
                        else if (tempElement.TagName.ToLower() == currentNode && i < indexOfElement)
                        {
                            i++;
                        }
                    }
                }
            }
            else
            {
                if (numOfOccurence == 1 || numOfOccurence == 0)
                {
                    return htmlElement.FirstChild;
                }
                if (numOfOccurence > 1)
                {
                    foreach (HtmlElement tempElement in htmlElement.Children)
                    {
                        if (tempElement.TagName.ToLower() == currentNode)
                        {
                            return SelectHtmlNode(xPath.Substring(xPath.IndexOf('/', 1)), tempElement);
                        }
                    }
                }
            }
            return null;
        }
    }
}