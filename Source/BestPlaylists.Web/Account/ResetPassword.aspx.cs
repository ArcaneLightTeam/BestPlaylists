namespace BestPlaylists.Web.Account
{
    using System;

    using BestPlaylists.Web.Helpers;

    public partial class ResetPassword : Page
    {
        protected string StatusMessage
        {
            get;
            private set;
        }

        protected void Reset_Click(object sender, EventArgs e)
        {
            string code = IdentityHelper.GetCodeFromRequest(Request);
            if (code != null)
            {
                var manager = Context.GetOwinContext().GetUserManager<UserManager>();

                var user = manager.FindByName(this.Email.Text);
                if (user == null)
                {
                    this.ErrorMessage.Text = "No user found";
                    return;
                }
                var result = manager.ResetPassword(user.Id, code, this.Password.Text);
                if (result.Succeeded)
                {
                    Response.Redirect("~/Account/ResetPasswordConfirmation");
                    return;
                }
                this.ErrorMessage.Text = result.Errors.FirstOrDefault();
                return;
            }

            this.ErrorMessage.Text = "An error has occurred";
        }
    }
}