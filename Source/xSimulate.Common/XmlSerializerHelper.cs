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
                using (XmlTextWriter xtw = new XmlTextWriter(ms, Encoding.UTF8))
                {
                    serializer.Serialize(xtw, obj);
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
