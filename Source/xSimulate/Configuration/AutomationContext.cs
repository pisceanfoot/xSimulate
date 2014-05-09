using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
