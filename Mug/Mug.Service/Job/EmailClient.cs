using Mug.Dao;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mug.Service.Job
{
    public class EmailClient
    {
        private const string SUBJECT = "XX網站客戶-詢問通知";
        public  void  sendEmail(Contact _Contact)
        {
            try
            {
                //寄件人的帳密
                GMailer.GmailUsername = "ZZZZ@gmail.com";
                GMailer.GmailPassword = "OOOO";
                GMailer mailer = new GMailer();
                //這裡要改成收件者
                mailer.ToEmail = "ka@@@@@gmail.com,OOOO@yahoo.com.tw";
                mailer.Subject = SUBJECT;
                //寄信
                string Time = _Contact.CreateTime?.ToString("yyyy-MM-dd HH:mm");
                string mailTemplate = GetEmbeddedTemplate("MailTemplate.html");
                if (mailTemplate != null)
                {
                    mailTemplate = string.Format(mailTemplate, _Contact.Name, _Contact.Phone, _Contact.Service, _Contact.Email, _Contact.Memo, Time);
                }
                else
                {
                    mailTemplate = $"姓名:{_Contact.Name} 電話:{_Contact.Phone} 服務:{_Contact.Service} 信箱:{_Contact.Email} 備註{_Contact.Memo} 時間{Time}";
                }
                mailer.Body = mailTemplate;
                mailer.IsHtml = true;
                mailer.Send();
            }
            catch (Exception err)
            {
                return;
                throw;
            }
           
        }

        public static String GetEmbeddedTemplate(string resourceName)
        {
            Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            Stream stream = asm.GetManifestResourceStream(asm.GetName().Name + "." + resourceName);
            if (stream != null)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(stream, System.Text.Encoding.Default);
                return sr.ReadToEnd();
            }
            return null;
        }
    }


    public class GMailer
    {
        public static string GmailUsername { get; set; }
        public static string GmailPassword { get; set; }
        public static string GmailHost { get; set; }
        public static int GmailPort { get; set; }
        public static bool GmailSSL { get; set; }

        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }

        static GMailer()
        {
            GmailHost = "smtp.gmail.com";
            GmailPort = 25; // Gmail can use ports 25, 465 & 587; but must be 25 for medium trust environment.
            GmailSSL = true;
        }

        public void Send()
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = GmailHost;
            smtp.Port = GmailPort;
            smtp.EnableSsl = GmailSSL;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(GmailUsername, GmailPassword);

            using (var message = new MailMessage(GmailUsername, ToEmail))
            {
                message.Subject = Subject;
                message.Body = Body;
                message.IsBodyHtml = IsHtml;
                smtp.Send(message);
            }
        }
    }
}
