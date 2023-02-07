using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailHelperNet5.Model
{
    public class CMailContent
    {
        public string senderAddress = string.Empty;
        public string senderName = string.Empty;
        public string title = string.Empty;
        public string body = string.Empty;
        public List<CRecipient> recipients = new List<CRecipient>();
        public bool individual = true;
        public bool advertising = false;
    }
}
