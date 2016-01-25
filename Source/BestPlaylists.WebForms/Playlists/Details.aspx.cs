using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BestPlaylists.Data.Models;
using BestPlaylists.Services.Data.Contracts;
using Microsoft.AspNet.Identity;
using Ninject;

namespace BestPlaylists.WebForms.Playlists
{
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

                this.plTitle.InnerText = Server.HtmlDecode(playlist.Title);
                this.plDescription.InnerText = Server.HtmlDecode(playlist.Description);
                this.repeaterVideos.DataSource = playlist.Videos;
                this.plRating.InnerText = playlist.CurrentRating.ToString("F2");

                this.commentsCount.InnerText = playlist.Comments.Count().ToString();
                this.postComment.Visible = this.IsUserLogged();
                this.plComments.DataSource = playlist.Comments;

                var userId = this.User.Identity.GetUserId();
                if (userId != null && playlist.Ratings.All(u => u.UserId != userId))
                {
                    string[] arrayOfRating = { "0", "1", "2", "3", "4", "5" };
                    this.Rating.DataSource = arrayOfRating;
                }
                else
                {
                    this.Rating.Visible = false;
                }

                DataBind();
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

            Response.Redirect("~/Playlists/Details?Id=" + id);
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