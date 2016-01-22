namespace BestPlaylists.Web.Account
{
    using System;

    using BestPlaylists.Web.Helpers;

    public partial class Confirm : Page
    {
        protected string StatusMessage
        {
            get;
            private set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string code = IdentityHelper.GetCodeFromRequest(Request);
            string userId = IdentityHelper.GetUserIdFromRequest(Request);
            if (code != null && userId != null)
            {
                var manager = Context.GetOwinContext().GetUserManager<UserManager>();
                var result = manager.ConfirmEmail(userId, code);
                if (result.Succeeded)
                {
                    this.successPanel.Visible = true;
                    return;
                }
            }
            this.successPanel.Visible = false;
            this.errorPanel.Visible = true;
        }
    }
}