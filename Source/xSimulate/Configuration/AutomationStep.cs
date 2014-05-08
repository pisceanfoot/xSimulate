using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace xSimulate.Configuration
{
    [Serializable]
    public class AutomationStep
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlArray("actions")]
        [XmlArrayItem("action")]
        public List<AutomationAction> AutomationActionList { get; set; }
    }
}
