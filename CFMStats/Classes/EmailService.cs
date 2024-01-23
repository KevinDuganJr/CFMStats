using System.Configuration;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;

namespace CFMStats.Classes
{
    public class EmailService
    {
        public string BCC { get; set; }

        public string BccFrom { get; set; }

        public string CC { get; set; }

        public string FromEmail { get; set; }

        public string FromName { get; set; }

        public string Message { get; set; }

        public string Recipient { get; set; }

        public string Server { get; set; }

        public string Subject { get; set; }

        public static void SendEmail(EmailService E)
        {
            //Build the EMail message
            var msgEMail = new MailMessage();

            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            var smtpClient = new SmtpClient(smtpSection.Network.Host, smtpSection.Network.Port);
            //smtpClient.EnableSsl = true;

            var username = smtpSection.Network.UserName;
            var password = smtpSection.Network.Password;
            smtpClient.Credentials = new NetworkCredential(username, password);

            msgEMail.From = new MailAddress($"{"CFM Stats"} {username}");
            msgEMail.To.Add(E.Recipient);

            if(E.CC != null && E.CC.Length > 10)
            {
                msgEMail.Bcc.Add(E.CC);
            }

            if(E.BCC != null && E.CC.Length > 10)
            {
                msgEMail.Bcc.Add(E.BCC);
            }

            if(E.BccFrom != null)
            {
                msgEMail.Bcc.Add(E.BccFrom);
            }

            msgEMail.Subject = E.Subject;
            msgEMail.Body = E.Message;

            //"<p><font face='Arial Black'>Font</font></p>" & _
            //"<p><font color='#0000FF'>Color</font></p>" & _
            //"<p align='right'>&nbsp;&nbsp;&nbsp; Indent</p>" & _
            //"<p><font size='5'>Size</font></p>"

            msgEMail.IsBodyHtml = true;
            smtpClient.Send(msgEMail);

            msgEMail.Dispose();
        }
    }
}