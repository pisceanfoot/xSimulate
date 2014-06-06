using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace xSimulate.Common.Http
{
    public static class EasyWebRequest
    {
        /// <summary>
        /// HttpWebRequest方式
        /// </summary>
        public enum HttpMethod
        {
            GET,
            POST
        };

        public static WebProxy Proxy { get; set; }

        /// <summary>
        /// 发起提交请求
        /// </summary>
        /// <param name="method">请求模式</param>
        /// <param name="url">请求连接</param>
        /// <param name="postData">请求数据</param>
        /// <returns>请求获取数据</returns>
        public static string SendWebRequest(HttpMethod method, Encoding encoding, string url, string postData)
        {
            //Log.Debug("SendWebRequest method:{0}, url:{1}, postData:{2}", method, url, postData);

            string responseData = "";
            HttpWebResponse response = null;

            if (method == HttpMethod.GET && !string.IsNullOrEmpty(postData))
            {
                if (url.IndexOf("?") == -1)
                {
                    url += "?" + postData;
                }
                else
                {
                    url += "&" + postData;
                }
            }

            HttpWebRequest webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = method.ToString();
            webRequest.ServicePoint.Expect100Continue = false;
            //webRequest.UserAgent = "";
            if (EasyWebRequest.Proxy != null)
            {
                //Log.Debug("set proxy {0}", EasyWebRequest.Proxy.Address);
                webRequest.Proxy = EasyWebRequest.Proxy;
            }

            // POST 数据
            if (method == HttpMethod.POST && !string.IsNullOrEmpty(postData))
            {
                webRequest.ContentType = "application/x-www-form-urlencoded;charset=" + encoding.BodyName;
                Stream requestWriter = webRequest.GetRequestStream();

                byte[] buffer = encoding.GetBytes(postData);
                try
                {
                    requestWriter.Write(buffer, 0, buffer.Length);
                }
                catch (Exception ex)
                {
                    Exception exception = new Exception(string.Format("URI:{0}; DATA:{1}; METHOD:{2};", url, postData, method), ex);
                    //Log.Error(exception);

                    throw exception;
                }
                finally
                {
                    if (requestWriter != null)
                    {
                        requestWriter.Close();
                        requestWriter = null;
                    }
                }
            }

            // 读取数据
            StreamReader responseReader = null;
            try
            {
                response = (HttpWebResponse)webRequest.GetResponse();
                responseReader = new StreamReader(response.GetResponseStream(), encoding);
                responseData = responseReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                Exception exception = new Exception(string.Format("URI:{0}; DATA:{1}; METHOD:{2};", url, postData, method), ex);
                //Log.Error(exception);

                throw exception;
            }
            finally
            {
                if (responseReader != null)
                {
                    responseReader.Close();
                    responseReader.Dispose();
                }

                if (response != null)
                {
                    response.Close();
                    response = null;
                }

                webRequest = null;
            }

            return responseData;
        }

        /*
         * 
        /// <summary>
        /// 发起提交请求
        /// </summary>
        /// <param name="method">请求模式</param>
        /// <param name="url">请求连接</param>
        /// <param name="postData">请求数据</param>
        /// <returns>请求获取数据</returns>
        public static string PostFormData(Encoding encoding, string url, NameValueCollection collection, Dictionary<string, FileItem> binaryCollection)
        {
            Log.Debug("SendWebRequest method:POST, url:{0}", url);

            string responseData = "";
            HttpWebResponse response = null;

            HttpWebRequest webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = "POST";
            webRequest.ServicePoint.Expect100Continue = false;
            if (EasyWebRequest.Proxy != null)
            {
                Log.Debug("set proxy {0}", EasyWebRequest.Proxy.Address);
                webRequest.Proxy = EasyWebRequest.Proxy;
            }

            string boundary = "7d" + DateTime.Now.Ticks.ToString("X");
            webRequest.ContentType = "multipart/form-data;charset=" + encoding.BodyName + ";boundary=" + boundary;

            Stream stream = webRequest.GetRequestStream();

            byte[] itemBoundaryBytes = encoding.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endBoundaryBytes = encoding.GetBytes("\r\n--" + boundary + "--\r\n");

            string textTemplate = "Content-Disposition:form-data;name=\"{0}\"\r\nContent-Type:text/plain\r\n\r\n{1}";

            try
            {
                foreach (string key in collection.Keys)
                {
                    string textEntry = string.Format(textTemplate, key, collection[key]);
                    byte[] itemBytes = encoding.GetBytes(textEntry);
                    stream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
                    stream.Write(itemBytes, 0, itemBytes.Length);
                }

                if (binaryCollection != null)
                {
                    string binaryFileTemplate = "Content-Disposition:form-data;name=\"{0}\";filename=\"{1}\"\r\nContent-Type:{2}\r\n\r\n";

                    foreach (string key in binaryCollection.Keys)
                    {
                        FileItem item = binaryCollection[key];
                        string fileEntry = string.Format(binaryFileTemplate, key, item.FileName ?? key, ImageHelper.GetMimeType(item.FileName));
                        byte[] itemBytes = encoding.GetBytes(fileEntry);

                        stream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
                        stream.Write(itemBytes, 0, itemBytes.Length);
                        stream.Write(item.Content, 0, item.Content.Length);
                    }
                }

                stream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            }
            catch (Exception ex)
            {
                Exception exception = new Exception(string.Format("URI:{0};", url), ex);
                Log.Error(exception);

                throw exception;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream = null;
                }
            }

            // 读取数据
            StreamReader responseReader = null;
            try
            {
                response = (HttpWebResponse)webRequest.GetResponse();
                responseReader = new StreamReader(response.GetResponseStream(), encoding);
                responseData = responseReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                Exception exception = new Exception(string.Format("URI:{0};", url), ex);
                Log.Error(exception);

                throw exception;
            }
            finally
            {
                if (responseReader != null)
                {
                    responseReader.Close();
                    responseReader.Dispose();
                }

                if (response != null)
                {
                    response.Close();
                    response = null;
                }

                webRequest = null;
            }

            return responseData;
        }
         */
    }
}
