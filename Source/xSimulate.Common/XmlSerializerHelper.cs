using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace xSimulate.Common
{
    public class XmlSerializerHelper
    {
        public static string Serializer<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            byte[] buffer = null;
            using (MemoryStream ms = new MemoryStream())
            {
                XmlWriterSettings setting = new XmlWriterSettings();
                setting.OmitXmlDeclaration = true;
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                using (XmlWriter writer = XmlWriter.Create(ms, setting))
                {
                    serializer.Serialize(writer, obj, ns);
                    ms.Position = 0;
                    buffer = ms.ToArray();
                }
            }

            if (buffer != null)
            {
                return Encoding.UTF8.GetString(buffer);
            }
            else
            {
                return null;
            }
        }
    }
}
