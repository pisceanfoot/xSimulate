using System.Collections.Generic;
using xSimulate.Configuration;

namespace xSimulate.Action
{
    public class FindAction : ActionBase
    {
        public FindAction(AutomationAction automationActionData)
            : base(automationActionData)
        {
            this.Combine = GetAttributeValue<bool>("combine");
            this.Contains = GetAttributeValue<bool>("contains");
            this.WildCard = GetAttributeValue<bool>("wildcard");
            this.Trim = GetAttributeValue<bool>("trim");

            this.ID = GetAttributeValue<string>("id");
            this.Name = GetAttributeValue<string>("name");
            this.ClassName = GetAttributeValue<string>("class");
            this.TagName = GetAttributeValue<string>("tagname");
            this.Type = GetAttributeValue<string>("type");
            this.Title = GetAttributeValue<string>("title");
            this.Url = GetAttributeValue<string>("href");
            this.InnerText = GetAttributeValue<string>("innertext");

            this.Index = GetAttributeValue<int>("index", -1);

            this.XPath = GetAttributeValue<string>("xpath");

            List<string> embedAttrbuteList = GetAttributeValueStartWith<string>("embed_");
            if (embedAttrbuteList != null && embedAttrbuteList.Count > 0)
            {
                this.EmbedAttribute = new Dictionary<string, string>();
                foreach (string str in embedAttrbuteList)
                {
                    int index = str.IndexOf(":");
                    if (index != -1)
                    {
                        string key = str.Substring(0, index);
                        string value = str.Substring(index + 1, str.Length - index - 1);

                        this.EmbedAttribute.Add(key, value);
                    }
                }
            }
        }

        public bool Combine { get; set; }

        public bool Contains { get; set; }

        public bool WildCard { get; set; }

        public bool Trim { get; set; }

        public string ID { get; set; }

        public string Name { get; set; }

        public string ClassName { get; set; }

        public string XPath { get; set; }

        public string TagName { get; set; }

        public string Type { get; set; }

        public string Title { get; set; }

        public int Index { get; set; }

        public string Url { get; set; }

        public string InnerText { get; set; }

        public Dictionary<string, string> EmbedAttribute { get; set; }

        public override ActionType ActionType
        {
            get { return ActionType.FindAction; }
        }
    }
}