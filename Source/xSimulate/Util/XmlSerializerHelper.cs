using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace xSimulate.Util
{
    public class XmlSerializerHelper
    {
        /// <summary>
        /// deserialize an object from a file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T LoadFromXml<T>(string fileName) where T : class
        {
            FileStream fs = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                return (T)serializer.Deserialize(fs);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string ToStringXmlMessage<T>(T instance)
        {
            StringWriter sr = null;
            try
            {
                XmlSerializer xr = new XmlSerializer(typeof(T));
                StringBuilder sb = new StringBuilder();

                sr = new StringWriter(sb);
                xr.Serialize(sr, instance);

                return (sb.ToString());
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }
        }

        /// <summary>
        /// deserialize an object from a XML Message
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlMessage"></param>
        /// <returns>
        ///     Null is returned if any error occurs.
        /// </returns>
        public static T LoadFromXmlMessage<T>(string xmlMessage) where T : class
        {
            StringReader sReader = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                sReader = new StringReader(xmlMessage);
                return (T)serializer.Deserialize(sReader);
            }
            finally
            {
                if (sReader != null)
                {
                    sReader.Dispose();
                }
            }
        }

        public static bool SaveXmlToFlie<T>(string fileName, T config) where T : class
        {
            string xmlStrInfor = ToStringXmlMessage<T>(config);

            if (!string.IsNullOrEmpty(xmlStrInfor))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlStrInfor);
                xmlDoc.Save(fileName);

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 快速生成xml文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="XMLFileToCreate"></param>
        /// <param name="instance"></param>
        public static void Serialize<T>(string xMLFileToCreate, T instance)
        {
            if (instance == null) return;
            XmlSerializer xs = new XmlSerializer(typeof(T));

            using (XmlWriter xm = XmlWriter.Create(xMLFileToCreate))
            {
                xs.Serialize(xm, instance);
            }
        }
    }
}
