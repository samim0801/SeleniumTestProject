using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Configuration;

namespace APITestProject
{
    public class ServiceCall
    {
        public string strBaseServiceUrl = ConfigurationManager.AppSettings["BaseServiceUrl"];
        public string strApiKey = ConfigurationManager.AppSettings["ApiKey"];
        public  string BuildUri(string strEntityName, string strOutputFormat, Dictionary<string, string> dictParameters)
        {
            StringBuilder strUri = new StringBuilder(strBaseServiceUrl);
            strUri.Append(strEntityName);
            strUri.Append(".");
            strUri.Append(strOutputFormat);
            strUri.Append("?api_key=");
            strUri.Append(strApiKey);

            if (dictParameters != null)
            {
                foreach (string key in dictParameters.Keys)
                {
                    strUri.Append("&");
                    strUri.Append(key);
                    strUri.Append("=");
                    strUri.Append(dictParameters[key]);
                }
            }
            return strUri.ToString();
        }
        public HttpWebResponse CallService(string method, string uri, string contentType, string accept, string content, IDictionary<string, string> headers)
        {
            HttpWebResponse resp = null;

            try
            {
                HttpWebRequest req = GetRestRequest(method, uri, contentType,  accept, content, null);

                resp = req.GetResponse() as HttpWebResponse;             
            }
            catch (WebException ex)
            {
                resp = (HttpWebResponse)ex.Response;               
            }
            catch (Exception ex)
            {
                throw;
            }
            //finally
            //{
            //    if (resp != null && resp.ContentLength > 0)
            //    {                   
            //        resp.Close();                   
            //    }
            //    if (resp != null) resp.Dispose();
            //}
            return resp;
        }
        private HttpWebRequest GetRestRequest(string method, string uri, string contentType, string accept, string content, IDictionary<string, string> headers)
        {
            HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
            req.KeepAlive = false;
            req.Method = method.ToUpper();           

            if (headers != null)
            {
                foreach (var header in headers)
                    req.Headers.Add(header.Key, header.Value);
            }
            if (!string.IsNullOrEmpty(accept))
                req.Accept = accept;

            if (("POST,PUT").Split(',').Contains(method.ToUpper()))
            {
                if (content != null)
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(content);
                    req.ContentLength = buffer.Length;
                    req.ContentType = contentType;
                    req.Accept = accept;
                    Stream body = req.GetRequestStream();

                    body.Write(buffer, 0, buffer.Length);
                    body.Close();
                }
                else
                {
                    req.ContentLength = 0;
                    req.ContentType = contentType;
                    req.Accept = accept;
                }
            }
            return req;
        }
    }
}
