using System;
using System.Linq;
using System.Web.UI;
using BestPlaylists.Data.Models;
using BestPlaylists.Services.Data.Contracts;
using Ninject;

namespace BestPlaylists.WebForms.Playlists
{
    public partial class Show : Page
    {
        [Inject]
        public IPlaylistService Playlists { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public IQueryable<Playlist> ListViewPlaylists_GetData()
        {
            return this.Playlists.GetAll();
        }
    }
}