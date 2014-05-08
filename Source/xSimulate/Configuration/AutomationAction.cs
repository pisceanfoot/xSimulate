using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace xSimulate.Configuration
{
    [Serializable]
    public class AutomationAction
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlArray("attributes")]
        [XmlArrayItem("attribute")]
        public List<AutomationActionAttribute> AutomationActionAttributeList { get; set; }

        [XmlArray("childActions")]
        [XmlArrayItem("childAction")]
        public List<AutomationAction> ChildActionList { get; set; }
    }
}
