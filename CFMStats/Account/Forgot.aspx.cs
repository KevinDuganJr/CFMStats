﻿using System;
using System.Web;
using System.Web.UI;
using CFMStats.Classes;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using CFMStats.Models;

namespace CFMStats.Account
{
    public partial class ForgotPassword : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Forgot(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Validate the user's email address
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = manager.FindByName(Email.Text);
                if (user == null || !manager.IsEmailConfirmed(user.Id))
                {
                    FailureText.Text = "The user either does not exist or is not confirmed.";
                    ErrorMessage.Visible = true;
                    return;
                }
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send email with the code and the redirect to reset password page
                var code = manager.GeneratePasswordResetToken(user.Id);
                var callbackUrl = IdentityHelper.GetResetPasswordRedirectUrl(code, Request);
                manager.SendEmail(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>.");


                var emailItem = new Classes.EmailService
                {
                    Recipient = user.Email,
                    Subject = $"{"CFM Stats"} - {"Reset Password"}",
                    Message = "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>."
                };

                try
                {
                    Classes.EmailService.SendEmail(emailItem);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                loginForm.Visible = false;
                DisplayEmail.Visible = true;
            }
        }
    }
}