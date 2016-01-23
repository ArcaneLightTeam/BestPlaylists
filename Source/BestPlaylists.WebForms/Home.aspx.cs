namespace BestPlaylists.WebForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using BestPlaylists.Services.Data.Contracts;
    using Models;
    using Ninject;

    public partial class Home : Page
    {
        [Inject]
        public IPlaylistService Playlists { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            // CONTSANTS - when common class library exist
            // expires, playlists, 10 for playlist count and 
            object expires = this.Cache["expires"];

            if (expires != null && DateTime.Parse(expires.ToString()) > DateTime.Now)
            {
                this.gridTopPLaylists.DataSource = this.Cache["playlists"] as IList<PlaylistResponseModel>;
                this.gridTopPLaylists.DataBind();
                return;
            }

            IList<PlaylistResponseModel> topPlaylists =
                Playlists.GetAll()
                .OrderByDescending(p => p.Ratings.Average(c => c.Value))
                .Take(10)
                .Select(p => new PlaylistResponseModel
                {
                    Title = p.Title,
                    Description = p.Description,
                    Rating = p.Ratings.Average(c => c.Value),
                    Category = p.Category.Name,
                    Username = p.User.UserName,
                    Date = p.CreationDate
                })
                .ToList();

            this.Cache["playlists"] = topPlaylists;
            this.Cache["expires"] = DateTime.Now.AddMinutes(10);
            
            this.gridTopPLaylists.DataSource = topPlaylists;
            this.gridTopPLaylists.DataBind();
        }
    }
}