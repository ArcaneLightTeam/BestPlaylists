using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Ninject;

namespace BestPlaylists.WebForms.Account
{
    public partial class EditProfile : Page
    {
        [Inject]
        public Services.Data.Contracts.IUserService UserService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var userId = this.User.Identity.GetUserId();
            Data.Models.User user = this.UserService.GetById(userId);

            this.details.DataSource = new List<Data.Models.User>() { user };
            this.details.DataBind();

        }

        protected void UpdateUser_Click(object sende, EventArgs e)
        {
            string userId = this.User.Identity.GetUserId();
            Data.Models.User user = this.UserService.GetById(userId);

            user.FirstName = (this.details.FindControl("tbFirstName") as TextBox).Text;
            user.LastName = (this.details.FindControl("tbLastName") as TextBox).Text;
            user.Email = (this.details.FindControl("tbEmail") as TextBox).Text;
            user.YouTubeAccount = (this.details.FindControl("tbYouTube") as TextBox).Text;
            user.FacebookAccount = (this.details.FindControl("tbFacebook") as TextBox).Text;

            this.UserService.Update(user);

            this.Response.Redirect("~/Account/Manage");
        }


    }
}