using System;
using System.Xml.Serialization;

namespace xSimulate.Web.Model
{
    [Serializable]
    public class FileGenerator
    {
        [XmlElement]
        public string Browser { get; set; }

        [XmlElement]
        public string Keyword { get; set; }

        [XmlElement]
        public string FindWangWang { get; set; }

        [XmlElement]
        public string FindItemID { get; set; }

        [XmlElement]
        public string SearchPageBrowserType { get; set; }

        [XmlElement]
        public string PriceFrom { get; set; }

        [XmlElement]
        public string PriceTo { get; set; }

        [XmlElement]
        public string Province { get; set; }

        [XmlElement]
        public string City { get; set; }

        [XmlElement]
        public string MaxPage { get; set; }

        [XmlElement]
        public string ItemBrowserTime { get; set; }

        [XmlElement]
        public string ClickReview { get; set; }
    }
}