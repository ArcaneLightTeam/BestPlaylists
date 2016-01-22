namespace BestPlaylists.Web.Account
{
    using System;

    using BestPlaylists.Web.Helpers;

    public partial class TwoFactorAuthenticationSignIn : System.Web.UI.Page
    {
        private ApplicationSignInManager signinManager;
        private UserManager manager;

        public TwoFactorAuthenticationSignIn()
        {
            this.manager = Context.GetOwinContext().GetUserManager<UserManager>();
            this.signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var userId = this.signinManager.GetVerifiedUserId<User, string>();
            if (userId == null)
            {
                Response.Redirect("/Account/Error", true);
            }
            var userFactors = this.manager.GetValidTwoFactorProviders(userId);
            this.Providers.DataSource = userFactors.Select(x => x).ToList();
            this.Providers.DataBind();            
        }

        protected void CodeSubmit_Click(object sender, EventArgs e)
        {
            bool rememberMe = false;
            bool.TryParse(Request.QueryString["RememberMe"], out rememberMe);
            
            var result = this.signinManager.TwoFactorSignIn<User, string>(this.SelectedProvider.Value, this.Code.Text, isPersistent: rememberMe, rememberBrowser: this.RememberBrowser.Checked);
            switch (result)
            {
                case SignInStatus.Success:
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                    break;
                case SignInStatus.LockedOut:
                    Response.Redirect("/Account/Lockout");
                    break;
                case SignInStatus.Failure:
                default:
                    this.FailureText.Text = "Invalid code";
                    this.ErrorMessage.Visible = true;
                    break;
            }
        }

        protected void ProviderSubmit_Click(object sender, EventArgs e)
        {
            if (!this.signinManager.SendTwoFactorCode(this.Providers.SelectedValue))
            {
                Response.Redirect("/Account/Error");
            }

            var user = this.manager.FindById(this.signinManager.GetVerifiedUserId<User, string>());
            if (user != null)
            {
                var code = this.manager.GenerateTwoFactorToken(user.Id, this.Providers.SelectedValue);
            }

            this.SelectedProvider.Value = this.Providers.SelectedValue;
            this.sendcode.Visible = false;
            this.verifycode.Visible = true;
        }
    }
}