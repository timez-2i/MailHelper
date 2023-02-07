using MailHelperCore.Model;
using MailHelperCore.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailHelperCore
{
    /// <summary>
    /// 제  목: NcpMailer
    /// 작성자: trillion-binary
    /// 작성일: 2019-06-21
    /// 설  명: Ncp Cloud Outbound Mailer 를 사용한 Mailer
    /// 수정자: 수정자<탭>수정내용<탭>수정일자
    /// </summary>
    public class NcpMailer : Mailer
    {
        #region 변수선언
        public string AccessKey { get; set; } // Ncp Cloud API AccessKey
        public string SecretKey { get; set; } // Ncp Cloud API SecretKey
        public string SendEmail { get; set; } // 발신자 이메일
        public string SenderName { get; set; } // 발신자 이름
        public string Domain { get; set; } // Ncp Cloud API Domain
        public string Url { get; set; } // Ncp Cloud API Url

        private string contentType = "application/json";
        #endregion

        public NcpMailer(string accessKey, string secretKey, string sendEmail, string senderName, string domain, string url)
        {
            this.AccessKey = accessKey;
            this.SecretKey = secretKey;
            this.SendEmail = sendEmail;
            this.SenderName = senderName;
            this.Domain = domain;
            this.Url = url;
        }

        // SendProcess 재작성
        protected override string SendProcess(Dictionary<string, string> email, string subject, string body)
        {
            string timestamp = CTimeUtil.GetNowTimeStamp().ToString().Trim();
            // 암호화 진행
            string signature = CSecureUtil.HMACSHA256(this.SecretKey, CSignatureUtil.MakeSignature(this.Url, timestamp, this.AccessKey));
            string content = GetContent(email, subject, body);

            // 헤더 설정
            List<TextValuePair> headers = new List<TextValuePair>();
            headers.Add(new TextValuePair("x-ncp-apigw-timestamp", timestamp));
            headers.Add(new TextValuePair("x-ncp-iam-access-key", this.AccessKey));
            headers.Add(new TextValuePair("x-ncp-apigw-signature-v2", signature));

            string jsonData = CRequestUtil.PostUrl(string.Format("{0}{1}", Domain, Url), content, contentType, headers);
            return jsonData;
        }

        #region
        // 내용 만들기
        private string GetContent(Dictionary<string, string> email, string subject, string body)
        {
            CMailContent data = new CMailContent();
            data.senderAddress = this.SendEmail;
            data.senderName = this.SenderName;
            data.title = subject;
            data.body = body;
            data.recipients = GetRecipients(email);
            data.individual = true;
            data.advertising = false;

            return data.ToJSON();
        }
        
        // 수신자 목록
        private List<CRecipient> GetRecipients(Dictionary<string, string> email)
        {
            List<CRecipient> list = new List<CRecipient>();
            foreach (var item in email)
            {
                CRecipient data = new CRecipient();
                data.address = item.Key;
                data.type = item.Value;
                list.Add(data);
            }

            return list;
        }
        #endregion
    }
}
