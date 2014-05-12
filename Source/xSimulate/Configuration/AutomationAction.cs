using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace xSimulate.Configuration
{
    [Serializable]
    public class AutomationAction
    {
        public AutomationAction()
        {
            this.Enabled = true;
        }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("enabled")]
        public bool Enabled { get; set; }

        [XmlElement("saveData")]
        public string SaveData { get; set; }

        [XmlElement("saveDataKey")]
        public string SaveDataKey { get; set; }

        [XmlElement("getDataKey")]
        public string GetDataKey { get; set; }

        [XmlElement("context")]
        public AutomationContext Context { get; set; }

        [XmlArray("attributes")]
        [XmlArrayItem("attribute")]
        public List<AutomationActionAttribute> AttributeList { get; set; }

        [XmlArray("childActions")]
        [XmlArrayItem("action")]
        public List<AutomationAction> ChildActionList { get; set; }

        [XmlArray("conditionActions")]
        [XmlArrayItem("action")]
        public List<AutomationAction> ConditionActionList { get; set; }

        public void Add(AutomationActionAttribute attribute)
        {
            if (this.AttributeList == null)
            {
                this.AttributeList = new List<AutomationActionAttribute>();
            }

            this.AttributeList.Add(attribute);
        }

        public void AddChild(AutomationAction childAction)
        {
            if (this.ChildActionList == null)
            {
                this.ChildActionList = new List<AutomationAction>();
            }

            this.ChildActionList.Add(childAction);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("type:");
            builder.AppendLine(this.Type);
            builder.Append("saveData:");
            builder.AppendLine(this.SaveData);
            builder.Append("saveDataKey:");
            builder.AppendLine(this.SaveDataKey);
            builder.Append("getDataKey:");
            builder.AppendLine(this.GetDataKey);

            if (this.Context != null)
            {
                builder.AppendLine("context:");
                builder.AppendFormat("\tframe:{0}\r\n", this.Context.Frame);
            }
            
            if (this.AttributeList != null && this.AttributeList.Count > 0)
            {
                builder.AppendLine("attributes:");
                foreach (AutomationActionAttribute attr in this.AttributeList)
                {
                    builder.Append("\tattr:");
                    builder.AppendFormat("{0}:{1}\r\n", attr.Name, attr.Value);
                }
            }

            if (this.ChildActionList != null && this.ChildActionList.Count > 0)
            {
                builder.Append("childAction:");
                builder.AppendLine("true");
            }

            return builder.ToString();
        }
    }
}