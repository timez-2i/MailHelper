using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MailHelper.Util
{
    public class CSecureUtil
    {
        public static string HMACSHA256(string key, string data)
        {
            HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
            string ret = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(data)));
            return ret;
        }
    }
}
