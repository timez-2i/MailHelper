using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Net.Security;
using MailHelperCore.Model;

namespace MailHelperCore.Util
{
    public class CRequestUtil
    {
        private static readonly int timeout = 1000000;

        /// <summary>
        /// post 형식으로 호출
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static String PostUrl(string url, string param, string ContentType, List<TextValuePair> headers)
        {
            String JsonObj = String.Empty;
            ServicePointManager.DefaultConnectionLimit = 200;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = ContentType;
            request.Method = "POST";
            request.ReadWriteTimeout = timeout;
            request.Timeout = timeout;

            foreach (TextValuePair item in headers)
            {
                request.Headers.Add(item.Text, item.Value.ToString());
            }

            byte[] bytes = UTF8Encoding.UTF8.GetBytes(param);
            request.ContentLength = bytes.Length;

            try
            {
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(bytes, 0, bytes.Length);
                }

                // Response 처리
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream respStream = response.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(respStream, Encoding.GetEncoding("UTF-8")))
                        {
                            JsonObj = sr.ReadToEnd();
                            sr.Close();
                        }
                        respStream.Flush();
                        respStream.Close();
                    }
                    response.Close();
                }
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                request.Abort();
            }

            return JsonObj;
        }

    }
}
