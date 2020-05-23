using System.Configuration;
using System.Net.Configuration;
using System.Net.Mail;

namespace CFMStats.Classes
{
    public class EmailItem
    {
        public string Server { get; set; }
        public string Message { get; set; }
        public string Subect { get; set; }
        public string Recipient { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string BccFrom { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }

        public static void SendEmail(EmailItem E)
        {
            //Build the EMail message
            MailMessage msgEMail = new MailMessage();
            SmtpClient smtpClient = new SmtpClient(E.Server);
            

            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            string username = smtpSection.Network.UserName;
            string password = smtpSection.Network.Password;
            smtpClient.Credentials = new System.Net.NetworkCredential(username, password);

            //Environment.UserName.ToString & " <" & Environment.MachineName.ToString & "@gbp.com" & ">"
            msgEMail.From = new MailAddress(string.Format("{0} {1}", E.FromName, username));
            msgEMail.To.Add(E.Recipient);

            if (E.CC != null && E.CC.Length > 10) { msgEMail.Bcc.Add(E.CC);            }
            if (E.BCC != null && E.CC.Length > 10) { msgEMail.Bcc.Add(E.BCC); }
            if (E.BccFrom != null) { msgEMail.Bcc.Add(E.BccFrom); }

            msgEMail.Subject = E.Subect;
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