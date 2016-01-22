namespace BestPlaylists.WebForms.Account
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.UI;

    using BestPlaylists.Data.Models;
    using BestPlaylists.WebForms.Helpers;

    using Microsoft.Ajax.Utilities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    public partial class Register : Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = this.Context.GetOwinContext().GetUserManager<UserManager>();
            var signInManager = this.Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new User()
            {
                UserName = this.UserName?.Text,
                Email = this.Email?.Text,
                FirstName = this.FirstName?.Text,
                LastName = this.LastName?.Text,
                FacebookAccount = string.IsNullOrWhiteSpace(this.FacebookAccount?.Text) ? null : this.FacebookAccount.Text,
                YouTubeAccount = string.IsNullOrWhiteSpace(this.YouTubeAccount?.Text) ? null : this.YouTubeAccount.Text,
                AvatarUrl = string.IsNullOrWhiteSpace(this.AvatarUrl?.Text) ? null : this.AvatarUrl.Text,
            };

            IdentityResult result = manager.Create(user, this.Password.Text);
            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                //string code = manager.GenerateEmailConfirmationToken(user.Id);
                //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                IdentityHelper.RedirectToReturnUrl(this.Request.QueryString["ReturnUrl"], this.Response);
            }
            else
            {
                this.ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}