using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BestPlaylists.Data.Models;
using BestPlaylists.Services.Data.Contracts;
using Microsoft.AspNet.Identity;
using Ninject;

namespace BestPlaylists.WebForms.Playlists
{
    using Error_Handler_Control;

    public partial class Details : Page
    {
        [Inject]
        public IPlaylistService Playlists { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var id = 0;

            if (int.TryParse(this.Context.Request.QueryString["Id"], out id))
            {
                var playlist = this.Playlists.GetById(id);

                this.plTitle.InnerText = this.Server.HtmlDecode(playlist.Title);
                this.plDescription.InnerText = this.Server.HtmlDecode(playlist.Description);
                this.repeaterVideos.DataSource = this.GetUrlsFromVideo(playlist.Videos); //playlist.Videos;
                this.videoCount.InnerText = playlist.Videos.Count().ToString();
                this.plRating.InnerText = playlist.CurrentRating.ToString("F2");

                this.commentsCount.InnerText = playlist.Comments.Count().ToString();
                this.postComment.Visible = this.IsUserLogged();
                this.plComments.DataSource = playlist.Comments;

                var userId = this.User.Identity.GetUserId();
                if (userId != null && playlist.Ratings.All(u => u.UserId != userId))
                {
                    object[] arrayOfRating =
                        {
                            new {Name = "Select rating", Value = "0"},
                            new {Name = "1", Value = "1"},
                            new {Name = "2", Value = "2"},
                            new {Name = "3", Value = "3"},
                            new {Name = "4", Value = "4"},
                            new {Name = "5", Value = "5"},
                        };
                    this.Rating.DataSource = arrayOfRating;
                }
                else
                {
                    this.Rating.Visible = false;
                }

                if (playlist.UserId != userId)
                {
                    this.btnEdit.Visible = false;
                }

                this.DataBind();
            }
        }

        protected void Rating_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var dropDown = sender as DropDownList;

            if (dropDown != null)
            {
                var rate = int.Parse(dropDown.SelectedValue);

                var id = 0;
                if (int.TryParse(this.Context.Request.QueryString["Id"], out id))
                {
                    var playlist = this.Playlists.GetById(id);
                    var rating = new Rating()
                    {
                        PlaylistId = id,
                        UserId = this.GetUserId(),
                        Value = rate
                    };

                    playlist.Ratings.Add(rating);

                    playlist.CurrentRating = playlist.Ratings.Average(r => r.Value);

                    this.Playlists.Update(playlist);

                    this.Rating.Visible = false;

                    this.plRating.InnerText = playlist.CurrentRating.ToString("F2");

                    ErrorSuccessNotifier.AddSuccessMessage("Playlist rated successfully!");
                }
            }
        }

        protected void AddComment_Click(object sender, EventArgs e)
        {
            var id = 0;

            // If PlayListId is Correct
            if (!int.TryParse(this.Context.Request.QueryString["Id"], out id))
            {
                return;
            }

            var playlist = this.Playlists.GetById(id);
            var userId = this.GetUserId();

            // If last comment not from same user
            if (playlist.Comments.LastOrDefault() != null && playlist.Comments.Last().UserId == userId)
            {
                this.tbUserComment.Text = "Please, wait for replay before comment again...";
                this.tbUserComment.Style.Add("color", "red");
                this.tbUserComment.Style.Add("font-weight", "bold");
                return;
            }

            var comment = new Comment()
            {
                CreationDate = DateTime.Now,
                PlaylistId = id,
                Text = this.tbUserComment.Text,
                UserId = userId
            };

            playlist.Comments.Add(comment);
            this.Playlists.Update(playlist);

            ErrorSuccessNotifier.AddSuccessMessage("Comment added successfully!");
            ErrorSuccessNotifier.ShowAfterRedirect = true;

            Response.Redirect("~/Playlists/Details?Id=" + id);
        }

        private IEnumerable<Video> GetUrlsFromVideo(ICollection<Video> videos)
        {
            var pattern = "www.youtube.com/watch?v=";
            var videoChanged = new List<Video>();

            foreach (var video in videos)
            {
                if (video.Url.IndexOf(pattern, StringComparison.Ordinal) != -1)
                {
                    var oldUrl = video.Url;
                    var unusedVideoStringLength = oldUrl.IndexOf(pattern, StringComparison.Ordinal) + pattern.Length;
                    var newUrl = oldUrl.Substring(unusedVideoStringLength, oldUrl.Length - unusedVideoStringLength);
                    video.Url = newUrl;
                }

                videoChanged.Add(video);
            }

            return videoChanged;
        }

        private bool IsUserLogged()
        {
            return this.User.Identity.IsAuthenticated;
        }

        private string GetUserId()
        {
            return this.User.Identity.GetUserId();
        }
    }
}