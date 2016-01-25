namespace BestPlaylists.WebForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI;
    using BestPlaylists.Common;
    using BestPlaylists.Services.Data.Contracts;
    using Ninject;

    public partial class _Default : Page
    {
        [Inject]
        public IPlaylistService Playlists { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            object expires = this.Cache[SiteConstants.CacheExpiresKey];

            if (expires != null && DateTime.Parse(expires.ToString()) > DateTime.Now)
            {
                this.gridTopPLaylists.DataSource = this.Cache[SiteConstants.CachePlaylistsKey] as IList<Data.Models.Playlist>;
                this.gridTopPLaylists.DataBind();
                return;
            }

            IList<Data.Models.Playlist> topPlaylists =
                Playlists.GetAll()
                .OrderByDescending(p => p.CurrentRating)
                .Take(SiteConstants.HomePlaylistsSize)
                .ToList();

            this.Cache[SiteConstants.CachePlaylistsKey] = topPlaylists;
            this.Cache[SiteConstants.CacheExpiresKey] = DateTime.Now.AddMinutes(SiteConstants.MinutesToKeepCache);

            this.gridTopPLaylists.DataSource = topPlaylists;
            this.gridTopPLaylists.DataBind();
        }
    }
}