using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Net.Security;
using MailHelper.Model;

namespace MailHelper.Util
{
    public class CRequestUtil
    {

        /// <summary>
        /// post 형식으로 호출
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static String PostUrl(string url, string param, string ContentType, List<TextValuePair> headers)
        {
            String JsonObj = String.Empty;
            try
            {

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                byte[] sendData = UTF8Encoding.UTF8.GetBytes(param);
                httpWebRequest.ContentType = ContentType;

                foreach (TextValuePair item in headers)
                {
                    httpWebRequest.Headers.Add(item.Text, item.Value.ToString());
                }
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentLength = sendData.Length;
                Stream requestStream = httpWebRequest.GetRequestStream();
                requestStream.Write(sendData, 0, sendData.Length);
                requestStream.Close();
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("UTF-8"));    // Encoding.GetEncoding("EUC-KR")
                JsonObj = streamReader.ReadToEnd();
                streamReader.Close();
                httpWebResponse.Close();
            }
            catch (Exception)
            {
                throw;
            }


            return JsonObj;

        }

    }
}
