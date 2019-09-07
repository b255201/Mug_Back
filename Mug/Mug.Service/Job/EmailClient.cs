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
        private const string SUBJECT = "百盛網站客戶-詢問通知";
        public void sendEmail(Contact _Contact)
        {
            try
            {
                //寄件人的帳密
                GMailer.GmailUsername = "souvenirs.mug@gmail.com";
                GMailer.GmailPassword = "jerrylan1948";
                GMailer mailer = new GMailer();
                //這裡要改成收件者
                mailer.ToEmail = "kai255201@yahoo.com.tw";
                mailer.Subject = SUBJECT;
                //寄信
                string Time = _Contact.CreateTime?.ToString("yyyy-MM-dd HH:mm");
                string mailTemplate = $@"<table style='vertical - align:top; text - align:left' border='1'>
    <tr><td>
    1.姓名：
</td>
<td>
{_Contact.Name}
</td>
</tr>
<tr>
<td>
2.電話：
</td>
<td>
<pre>
{_Contact.Phone}
</pre>
</td>
</tr>
<tr>
<td>
3.詢問項目：
</td>
<td>
{_Contact.Service}
</td>
</tr>
<tr>
<td>
4.信箱：
</td>
<td>
{_Contact.Email}
</td>
</tr>
<tr>
<td>
5.備註：
</td>
<td>
{_Contact.Memo}
</td>
</tr>
<tr>
<td>
6.時間：
</td>
<td>
{Time}
</td>
</tr>
</table>
";
                //if (mailTemplate != null)
                //{
                //    mailTemplate = string.Format(mailTemplate, _Contact.Name, _Contact.Phone, _Contact.Service, _Contact.Email, _Contact.Memo, Time);
                //}
                //else
                //{
                //    mailTemplate = $"姓名:{_Contact.Name} 電話:{_Contact.Phone} 服務:{_Contact.Service} 信箱:{_Contact.Email} 備註{_Contact.Memo} 時間{Time}";
                //}
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
            GmailPort = 587; // Gmail can use ports 25, 465 & 587; but must be 25 for medium trust environment.
            GmailSSL = true;
        }

        public void Send()
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = GmailHost;
            smtp.Port = GmailPort;
            smtp.EnableSsl = GmailSSL;
            //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = true;

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
