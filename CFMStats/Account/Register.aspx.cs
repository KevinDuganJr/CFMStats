using CFMStats.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using CFMStats.Classes;

namespace CFMStats.Account
{
    public partial class Register : Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();

            var user = new ApplicationUser() { UserName = Email.Text, Email = Email.Text };
            IdentityResult result = manager.Create(user, Password.Text);
            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                string code = manager.GenerateEmailConfirmationToken(user.Id);
                string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

                Classes.EmailService E = new Classes.EmailService
                {
                    Recipient = user.Email,
                    Subject = $"{"CFM Stats "} - {"Confirm Account"}",
                    Message = "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>."
                };

                try
                {
                    Classes.EmailService.SendEmail(E);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
          //      IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}