namespace BestPlaylists.WebForms.Account
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using BestPlaylists.Common;
    using Data.Models;
    using Microsoft.AspNet.Identity;
    using Ninject;

    public partial class EditProfile : Page
    {
        [Inject]
        public Services.Data.Contracts.IUserService UserService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var userId = this.User.Identity.GetUserId();
            User user = this.UserService.GetById(userId);

            this.details.DataSource = new List<User>() { user };
            this.details.DataBind();
        }

        protected void UpdateUser_Click(object sende, EventArgs e)
        {
            string userId = this.User.Identity.GetUserId();
            User user = this.UserService.GetById(userId);

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
                user.AvatarUrl = string.IsNullOrWhiteSpace(newAvatarUrl) ? SiteConstants.DefaultAvatar : newAvatarUrl;
            }
            else
            {
                if (fileUpload.PostedFile.ContentLength > SiteConstants.ImageMaxSize)
                {
                    this.panel.Visible = true;
                    this.errorText.InnerHtml = string.Format(
                        SiteConstants.ErrorUploadMessageFormat,
                        SiteConstants.ImageMaxSize / (1000.0 * 1000.0));
                    return;
                }

                string filename = Path.GetFileNameWithoutExtension(fileUpload.FileName);
                string extension = Path.GetExtension(fileUpload.FileName);
                string path = Server.MapPath(SiteConstants.ServerPathImages);
                filename += DateTime.Now.ToString(SiteConstants.DateFormatForFileNameImages) + extension; 
                fileUpload.SaveAs(path + filename) ;
                user.AvatarUrl = SiteConstants.PublicPathImages + filename;
            }

            string errorMessage = this.ValidateUser(user);
            if (errorMessage != null)
            {
                this.panel.Visible = true;
                this.errorText.InnerHtml = errorMessage;
                return;
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

        private string ValidateUser(User user)
        {
            if (user.FirstName.Length < ModelsConstats.MinFirstNameLength || 
                user.LastName.Length < ModelsConstats.MinLastNameLength)
            {
                return SiteConstants.ErrorFieldMinLength;
            }

            if (user.FirstName.Length > ModelsConstats.MaxFirstNameLength ||
                 user.LastName.Length > ModelsConstats.MaxLastNameLength)
            {
                return SiteConstants.ErrorFieldMaxLength;
            }
            
            if (!new Regex(SiteConstants.EmailRegex).IsMatch(user.Email))
            {
                return SiteConstants.WrongEmailAddress;
            }

            if ((!string.IsNullOrWhiteSpace(user.FacebookAccount) &&
                !new Regex(SiteConstants.FacebookRegex).IsMatch(user.FacebookAccount)))
            {
                return string.Format("{0} is {2} for Facebook",user.FacebookAccount, SiteConstants.ErrorAccountLink);
            }

            if ((!string.IsNullOrWhiteSpace(user.YouTubeAccount) &&
                !new Regex(SiteConstants.YoutubeRegex).IsMatch(user.YouTubeAccount)))
            {
                return string.Format("{0} is {1} for Youtube", user.YouTubeAccount, SiteConstants.ErrorAccountLink);
            }

            return null;
        }
    }
}