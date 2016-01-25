using System;
using System.Globalization;
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

                this.plTitle.InnerText = Server.HtmlEncode(playlist.Title);
                this.plDescription.InnerText = Server.HtmlEncode(playlist.Description);
                this.repeaterVideos.DataSource = playlist.Videos;
                this.plRating.InnerText = playlist.CurrentRating.ToString("F2");

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
                        UserId = this.User.Identity.GetUserId(),
                        Value = rate
                    };

                    playlist.Ratings.Add(rating);

                    playlist.CurrentRating = playlist.Ratings.Average(r => r.Value);

                    this.Playlists.Update(playlist);

                    this.Rating.Visible = false;
                }
            }
        }
    }
}