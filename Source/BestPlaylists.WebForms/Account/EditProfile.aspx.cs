using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
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

            // because controls are nested in DetailsView
            string newFirstName = this.GetTextFromInnerControl(this.details, "tbFirstName");
            string newLastName = this.GetTextFromInnerControl(this.details, "tbLastName");
            string newEmail = this.GetTextFromInnerControl(this.details, "tbEmail");
            string newYouTubeAccount = this.GetTextFromInnerControl(this.details, "tbYouTube");
            string newFaceBookAccount = this.GetTextFromInnerControl(this.details, "tbFacebook");

            user.FirstName = newFirstName != null ? newFirstName : user.FirstName;
            user.LastName = newLastName != null ? newLastName : user.LastName;
            user.Email = newEmail != null ? newEmail : user.Email;
            user.YouTubeAccount = newYouTubeAccount != null ? newYouTubeAccount : user.YouTubeAccount;
            user.FacebookAccount = newFaceBookAccount != null ? newFaceBookAccount : user.FacebookAccount;

            FileUpload fileUpload = this.details.FindControl("fileAvatar") as FileUpload;
            if (fileUpload == null || !fileUpload.HasFile ||
                !fileUpload.PostedFile.ContentType.Contains("image"))
            {
                string newAvatarUrl = this.GetTextFromInnerControl(this.details, "tbAvatar");
                user.AvatarUrl = newAvatarUrl != null ? newAvatarUrl : user.AvatarUrl;
            }
            else
            {
                string filename = Path.GetFileNameWithoutExtension(fileUpload.FileName);
                string extension = Path.GetExtension(fileUpload.FileName);
                string path = Server.MapPath("~/Images/");
                filename += DateTime.Now.ToString("dd-MMM-yyyy-HH-mm-ss") + extension; 
                fileUpload.SaveAs(path + filename) ;
                user.AvatarUrl = "/Images/" + filename;
            }

            this.UserService.Update(user);

            this.Response.Redirect("~/Account/Manage");
        }

        private string GetTextFromInnerControl(Control control, string innerControlID)
        {
            TextBox field = control.FindControl(innerControlID) as TextBox;
            if (field == null)
            {
                return null;
            }

            return field.Text.Trim();
        }
    }
}