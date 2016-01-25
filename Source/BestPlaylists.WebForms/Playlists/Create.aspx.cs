using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using BestPlaylists.Data.Models;
using BestPlaylists.Services.Data.Contracts;
using Microsoft.AspNet.Identity;
using Ninject;

namespace BestPlaylists.WebForms.Playlists
{
    public partial class Create : Page
    {
        [Inject]
        public IPlaylistService Playlists { get; set; }

        [Inject]
        public ICategoryService Categories { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.ddlCategory.DataSource = this.Categories.GetAll().ToList();
                this.ddlCategory.DataBind();
            }
        }

        protected void BtnAddPlaylist_Click(object sender, EventArgs e)
        {
            var playlistId = this.Playlists.Add(
                this.tbTitle.Text,
                this.tbDescription.Text,
                int.Parse(this.ddlCategory.SelectedValue),
                this.User.Identity.GetUserId(),
                this.cbPrivate.Checked);

            var videos = ExtractVideos(this.tbVideo.Text, playlistId);

            var playlist = this.Playlists.GetById(playlistId);
            playlist.Videos = videos;

            this.Playlists.Update(playlist);

            Response.Redirect("~/Playlists/Show.aspx");
        }

        private List<Video> ExtractVideos(string videoInput, int playlistId)
        {
            var videos = videoInput.Split(',');

            return videos.Select(t => new Video()
            {
                UserId = this.User.Identity.GetUserId(), Url = t, PlaylistId = playlistId
            }).ToList();
        }
    }
}