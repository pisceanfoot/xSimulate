using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        [XmlElement("saveDataKey")]
        public string SaveDataKey { get; set; }

        [XmlElement("getDataKey")]
        public string GetDataKey { get; set; }

        [XmlArray("attributes")]
        [XmlArrayItem("attribute")]
        public List<AutomationActionAttribute> AttributeList { get; set; }

        [XmlArray("childActions")]
        [XmlArrayItem("action")]
        public List<AutomationAction> ChildActionList { get; set; }

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
    }
}
