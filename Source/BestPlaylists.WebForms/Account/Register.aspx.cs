namespace BestPlaylists.WebForms.Account
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using BestPlaylists.Common;
    using BestPlaylists.Data.Models;
    using BestPlaylists.WebForms.Helpers;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Ninject;
    using Services.Data.Contracts;

    public partial class Register : Page
    {
        [Inject]
        public IUserService UserService { get; set; }

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
                AvatarUrl = string.IsNullOrWhiteSpace(this.AvatarUrl?.Text) ? SiteConstants.DefaultAvatar : this.AvatarUrl.Text,
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

        protected void UserName_TextChanged(object sender, EventArgs e)
        {
            TextBox tbUsername = sender as TextBox;
            if (tbUsername.Text.Length < 3)
            {
                this.panelUserExist.Visible = false;
                return;
            }

            this.panelUserExist.Visible = true;
            IList<string> usernames = new List<string>();
            if (this.ViewState[SiteConstants.CachedUsersKey] != null)
            {
                usernames = this.ViewState[SiteConstants.CachedUsersKey]
                    .ToString()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                usernames = this.UserService.GetAll().Select(x => x.UserName.ToLower()).ToList();
                this.ViewState[SiteConstants.CachedUsersKey] = string.Join(" ", usernames);
            }

            if (usernames.Contains(tbUsername.Text.ToLower()))
            {
                this.Image1.ImageUrl = SiteConstants.ErrorIconPath;
                this.labelUserExists.Text = "User already exists!";
                this.labelUserExists.CssClass = "text-danger";
            }
            else
            {
                this.Image1.ImageUrl = SiteConstants.SuccessIconPath;
                this.labelUserExists.CssClass = "text-success";
                this.labelUserExists.Text = "Username is free";
            }
        }
    }
}