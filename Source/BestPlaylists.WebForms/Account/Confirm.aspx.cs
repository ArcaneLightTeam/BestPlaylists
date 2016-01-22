﻿namespace BestPlaylists.WebForms.Account
{
    using System;
    using System.Web;
    using System.Web.UI;

    using BestPlaylists.WebForms.Helpers;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    public partial class Confirm : Page
    {
        protected string StatusMessage
        {
            get;
            private set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string code = IdentityHelper.GetCodeFromRequest(this.Request);
            string userId = IdentityHelper.GetUserIdFromRequest(this.Request);
            if (code != null && userId != null)
            {
                var manager = this.Context.GetOwinContext().GetUserManager<UserManager>();
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