using System.Collections.Generic;
using System.Linq;
using BestPlaylists.Services.Data.Contracts;
using Ninject;

namespace BestPlaylists.WebForms
{
    using System;
    using System.Web.UI;

    public partial class _Default : Page
    {
        [Inject]
        public IPlaylistService Playlists { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            // TODO:CONTSANTS - when common class library exist
            // expires, playlists, 10 for playlist count and 
            object expires = this.Cache["expires"];

            if (expires != null && DateTime.Parse(expires.ToString()) > DateTime.Now)
            {
                this.gridTopPLaylists.DataSource = this.Cache["playlists"] as IList<Data.Models.Playlist>;
                this.gridTopPLaylists.DataBind();
                return;
            }

            IList<Data.Models.Playlist> topPlaylists =
                Playlists.GetAll()
                .OrderByDescending(p => p.CurrentRating)
                .Take(10)
                .ToList();

            this.Cache["playlists"] = topPlaylists;
            this.Cache["expires"] = DateTime.Now.AddMinutes(10);

            this.gridTopPLaylists.DataSource = topPlaylists;
            this.gridTopPLaylists.DataBind();
        }
    }
}