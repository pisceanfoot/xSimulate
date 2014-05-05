using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xSimulate.Action
{
    public class FindElementAction : ActionBase
    {
        public string ID { get; set; }

        public string ClassName { get; set; }

        public HtmlElement Element { get; set; }

        public HtmlElement[] ElementArray { get; set; }

        public override ActionType ActionType
        {
            get { return ActionType.FindElementAction; }
        }
    }
}
