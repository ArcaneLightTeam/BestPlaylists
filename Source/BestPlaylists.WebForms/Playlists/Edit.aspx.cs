using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BestPlaylists.Services.Data.Contracts;
using Ninject;

namespace BestPlaylists.WebForms.Playlists
{
    public partial class Edit : Page
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

        }
    }
}