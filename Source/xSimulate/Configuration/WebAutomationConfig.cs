using System;
using System.Collections.Generic;
using System.Xml.Serialization;
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
        public List<AutomationStep> StepList { get; set; }

        public void Add(AutomationStep step)
        {
            if (this.StepList == null)
            {
                this.StepList = new List<AutomationStep>();
            }

            this.StepList.Add(step);
        }

        public static WebAutomationConfig Load()
        {
            if (actionConfig == null)
            {
                actionConfig = XmlSerializerHelper.LoadFromXml<WebAutomationConfig>("data.config");
            }

            return actionConfig;
        }

        public static void Save(WebAutomationConfig config)
        {
            XmlSerializerHelper.Serialize<WebAutomationConfig>("data.config", config);
        }
    }
}