using System;
using System.Xml.Serialization;

namespace xSimulate.Configuration
{
    [Serializable]
    public class AutomationContext
    {
        [XmlAttribute("frame")]
        public string Frame { get; set; }
    }
}