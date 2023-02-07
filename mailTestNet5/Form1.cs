using MailHelperNet5;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mailTestNet5
{
    public partial class Form1 : Form
    {
        private static NcpMailer Mailer = null; // 메일 라이브러리

        public Form1()
        {
            InitializeComponent();

            string accessKey = "5cUO2qXk1zD2oaiM7ZN9";
            string secretKey = "vxzEOgZKgQOmt0VoBG2GBgfYsFz9Ya3kq6PW2J83";
            string sendEmail = "cs@sports2i.com";
            string senderName = "고객만족팀";
            string domain = "https://mail.apigw.ntruss.com";
            string url = "/api/v1/mails";
            Mailer = new NcpMailer(accessKey, secretKey, sendEmail, senderName, domain, url);
            Mailer.Success += MailSendSuccess;
            Mailer.Fail += MailSendFail;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string email = "sadtimez@naver.com";
            string subject = "test 한글 되나?";
            string body = "TEST BODY \r\n ㅋㅋㅋㅋ";

            Dictionary<string, string> dicMails = new Dictionary<string, string>();
            dicMails.Add(email, "R");

            Mailer.Send(dicMails, subject, body);
        }

        #region 메일 이벤트
        private static void MailSendSuccess(Dictionary<string, string> email, string subject, string body, string response)
        {
            Console.WriteLine(string.Format("email : {0}\r subject : {1}\r body : {2}\r response : {3}", email, subject, body, response));
        }

        private static void MailSendFail(Dictionary<string, string> email, string subject, string body, Exception ex)
        {
            Console.WriteLine(string.Format("email : {0}\r subject : {1}\r body : {2}\r Exception : {3}", email, subject, body, ex.Message));
        }
        #endregion
    }
}
