using System;
using CFMStats.Classes;

namespace CFMStats
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            var emailItem = new Classes.EmailService
            {
                Recipient = "Zero2Cool80@gmail.com",
                FromEmail = txtFromEmail.Text.Trim(),
                Subject = $"{"CFM Stats"} - {"Contact Us"}",
                Message = $"{txtMessage.Text.Trim()} {Environment.NewLine} Reply To {txtFromEmail.Text.Trim()}"
            };
            

            // check if from email is valid email
            if (!Helper.IsStringEmailAddress(emailItem.FromEmail))
            {
                ShowAlert("danger alert-dismissible", "x", "WARNING", "Your league name must contain more than five characters.");
                return;
            }


            // check that message exists
            if(emailItem.Message.Length < 10)
            {
                ShowAlert("danger alert-dismissible", "x", "WARNING", "Your league name must contain more than five characters.");
                return;
            }


            try
            {
                // send email
                Classes.EmailService.SendEmail(emailItem);
                pnlSendMessage.Visible = false;
                pnlMessageSent.Visible = true;
            }
            catch(Exception exception)
            {
                ShowAlert("danger alert-dismissible", "x", "ERROR", exception.Message);
            }
        }


        /// <summary>
        /// Display Alert Message
        /// </summary>
        private void ShowAlert(string css, string image, string header, string details)
        {
            //alert alert-warning alert-dismissible
            pnlMessage.Visible = true;
            pnlMessage.CssClass = $"alert alert-{css}";
            if (image == "x")
            {
                lblAlert.Text = $"<strong>{header}</strong> {details}";
            }
            else
            {
                lblAlert.Text = $"<img src=\"/images/{image}.png\"> <strong>{header}</strong> {details}";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private bool SendEmail()
        {

            var emailItem = new Classes.EmailService
            {
              //  Recipient = user.Email,
                Subject = $"{"CFM Stats"} - {"Contact"}",
                Message = ""
            };

            try
            {
                Classes.EmailService.SendEmail(emailItem);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return true;
        }

    }
}