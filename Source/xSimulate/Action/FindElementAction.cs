using System;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace xSimulate.Action
{
    public class FindElementAction : ActionBase
    {
        public bool Contains { get; set; }

        public string ID { get; set; }

        public string ClassName { get; set; }

        public string XPath { get; set; }

        public string TagName { get; set; }

        public string Type { get; set; }

        public int Index { get; set; }

        public string Url { get; set; }

        public string InnerText { get; set; }

        public override ActionType ActionType
        {
            get { return ActionType.FindElementAction; }
        }
    }
}