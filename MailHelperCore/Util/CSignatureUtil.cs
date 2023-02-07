using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailHelperCore.Util
{
    public class CSignatureUtil
    {
        public static string MakeSignature(string url, string timestamp, string accessKey)
        {
            string space = " ";  // 공백
            string newLine = "\n";	
            string method = "POST";  // HTTP 메소드

            StringBuilder ret = new StringBuilder();
            ret.Append(method);
            ret.Append(space);
            ret.Append(url);
            ret.Append(newLine);
            ret.Append(timestamp);
            ret.Append(newLine);
            ret.Append(accessKey);

            //string ret = string.Format("POST {0}\n{1}\n{2}", url, timestamp, accessKey);

            return ret.ToString();
        }
    }
}
