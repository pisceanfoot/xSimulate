using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
