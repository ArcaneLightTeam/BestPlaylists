namespace BestPlaylists.WebForms.Playlists
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using BestPlaylists.Data.Models;
    using BestPlaylists.Services.Data.Contracts;

    using Error_Handler_Control;

    using Microsoft.AspNet.Identity;
    using Ninject;
    using UserControls;
    public partial class Edit : Page
    {
        private const string Id = "id";
        private const string Preview = "Preview";
        private const string Hide = "Hide";

        [Inject]
        public IPlaylistService PlaylistsService { get; set; }

        [Inject]
        public ICategoryService Categories { get; set; }

        [Inject]
        public IVideoService VideoService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.ddlCategory.DataSource = this.Categories.GetAll().ToList();
                this.ddlCategory.DataBind();
                string queryParamId = this.Request.Params.GetValues(Id)?[0];
                this.ViewState[Id] = queryParamId;
                int playlistId = 0;
                if (!int.TryParse(queryParamId, out playlistId))
                {
                    this.playlistNotFoundPanel.Visible = true;
                    this.editPlaylistPanel.Visible = false;
                    return;
                }

                Playlist playlistDetails = this.PlaylistsService.GetById(playlistId);
                if (playlistDetails == null || playlistDetails.IsRemoved == true)
                {
                    this.playlistNotFoundPanel.Visible = true;
                    this.editPlaylistPanel.Visible = false;
                    return;
                }

                this.tbTitle.Text = this.Server.HtmlEncode(playlistDetails.Title);
                this.tbDescription.Text = this.Server.HtmlEncode(playlistDetails.Description);
                this.ddlCategory.SelectedValue = playlistDetails.CategoryId.ToString();
                this.cbPrivate.Checked = playlistDetails.IsPrivate;
                this.editPlaylistPanel.Visible = true;
                this.videoCount.InnerText = playlistDetails.Videos.Count.ToString();

                this.videosRepeater.DataSource = playlistDetails.Videos;
                this.videosRepeater.DataBind();
            }
        }

        protected void BtnUpdatePlaylist_Click(object sender, EventArgs e)
        {
            int playlistId = int.Parse(this.ViewState[Id].ToString());
            Playlist playlistDetails = this.PlaylistsService.GetById(playlistId);
            playlistDetails.Title = this.tbTitle.Text;
            playlistDetails.Description = this.tbDescription.Text;
            int categoryId = -1;

            if (!int.TryParse(this.ddlCategory.SelectedValue, out categoryId))
            {
                return;
            }

            playlistDetails.CategoryId = categoryId;
            playlistDetails.IsPrivate = this.cbPrivate.Checked;
            this.PlaylistsService.Update(playlistDetails);

            ErrorSuccessNotifier.AddSuccessMessage("Playlist updated successfully!");
            ErrorSuccessNotifier.ShowAfterRedirect = true;

            this.Response.Redirect("/Playlists/Details?id=" + playlistDetails.Id);
        }

        protected void DeleteVideo_Command(object sender, CommandEventArgs e)
        {
            string commandID = e.CommandArgument.ToString();
            int videoId = -1;

            if (!int.TryParse(commandID, out videoId))
            {
                // HTML was modified from the user
                return;
            }

            this.VideoService.RemoveById(videoId);

            string queryParamId = this.Request.Params.GetValues(Id)?[0];
            Playlist playlistDetails = this.PlaylistsService.GetById(int.Parse(queryParamId));
            this.videosRepeater.DataSource = playlistDetails.Videos;
            this.videosRepeater.DataBind();

            this.videoCount.InnerText = playlistDetails.Videos.Count.ToString();
            ErrorSuccessNotifier.AddSuccessMessage("Video deleted successfully!");
        }

        protected void DeletePlaylist_Click(object sender, EventArgs e)
        {
            string queryParamId = this.Request.Params.GetValues(Id)?[0];
            Playlist playlistDetails = this.PlaylistsService.GetById(int.Parse(queryParamId));
            playlistDetails.IsRemoved = true;
            this.PlaylistsService.Update(playlistDetails);

            ErrorSuccessNotifier.AddSuccessMessage("Playlist deleted successfully!");
            ErrorSuccessNotifier.ShowAfterRedirect = true;

            this.Response.Redirect("/Account/YourPlaylists");
        }

        protected void AddVideo_Click(object sender, EventArgs e)
        {
            string queryParamId = this.Request.Params.GetValues(Id)?[0];
            Playlist playlistDetails = this.PlaylistsService.GetById(int.Parse(queryParamId));

            Video videoToAdd = new Video()
            {
                PlaylistId = playlistDetails.Id,
                Url = this.tbAddVideo.Text,
                UserId = this.User.Identity.GetUserId()
            };

            playlistDetails.Videos.Add(videoToAdd);
            this.PlaylistsService.Update(playlistDetails);

            this.tbAddVideo.Text = string.Empty;
            this.videosRepeater.DataSource = playlistDetails.Videos;
            this.videosRepeater.DataBind();

            this.videoCount.InnerText = playlistDetails.Videos.Count.ToString();

            ErrorSuccessNotifier.AddSuccessMessage("Video successfully added to playlist!");
        }

        protected void PreviewVideo_Click(object sender, EventArgs e)
        {
            YouTubePreview myControl = this.editPlaylistPanel.FindControl("ytPreview") as YouTubePreview;
            if (string.IsNullOrWhiteSpace(this.tbAddVideo.Text))
            {
                return;
            }

            if (this.btnPreview.Text == Preview)
            {
                myControl.PreviewVideoWithSlider(this.tbAddVideo, e);
                this.btnPreview.Text = Hide;
            }
            else
            {
                myControl.HideVideo();
                this.btnPreview.Text = Preview;
            }

            this.updateButtonText.Update();
        }
    }
}