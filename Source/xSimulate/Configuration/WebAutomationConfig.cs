using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using xSimulate.Action;
using xSimulate.Util;

namespace xSimulate.Configuration
{
    [Serializable]
    [XmlRoot("config")]
    public class WebAutomationConfig
    {
        private static WebAutomationConfig actionConfig = null;

        [XmlElement("version")]
        public string Version { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

        [XmlArray("steps")]
        [XmlArrayItem("step")]
        public List<AutomationStep> AutomationStepList { get; set; }
        
        public static WebAutomationConfig Config
        {
            get
            {
                if (actionConfig == null)
                {
                    actionConfig = XmlSerializerHelper.LoadFromXml<WebAutomationConfig>("data.config");
                }

                return actionConfig;
            }
        }

        public static void Save(WebAutomationConfig config)
        {
            XmlSerializerHelper.Serialize<WebAutomationConfig>("data.config", config);
        }
    }
}
