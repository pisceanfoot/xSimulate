using System;
using System.Windows.Forms;
using System.Xml.Serialization;
using xSimulate.Configuration;

namespace xSimulate.Action
{
    public class FindElementAction : ActionBase
    {
        public FindElementAction(AutomationAction automationActionData)
            : base(automationActionData)
        {
            this.Contains = GetAttributeValue<bool>("contains");
            this.ID = GetAttributeValue<string>("id");
            this.ClassName = GetAttributeValue<string>("class");
            this.XPath = GetAttributeValue<string>("xpath");
            this.TagName = GetAttributeValue<string>("tagname");
            this.Type = GetAttributeValue<string>("type");
            this.Index = GetAttributeValue<int>("index");
            this.Url = GetAttributeValue<string>("url");
            this.InnerText = GetAttributeValue<string>("innertext");
        }

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