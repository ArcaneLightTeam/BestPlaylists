using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BestPlaylists.Data.Models;
using BestPlaylists.Services.Data.Contracts;
using Ninject;

namespace BestPlaylists.WebForms.Playlists
{
    public partial class Show : Page
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

        public IQueryable<Playlist> ListViewPlaylists_GetData()
        {
            //var playlists = this.Playlists.GetAll();
            //var selected = 0;

            //if (this.ViewState["Filter"] != null && this.ddlCategory.SelectedValue != "-1")
            //{
            //    selected = int.Parse(this.ViewState["Filter"].ToString());
            //    var filtered = playlists.Where(p => p.CategoryId == selected);
            //    return filtered;
            //}
            //else if (this.ddlCategory.SelectedValue != "-1")
            //{
            //    selected = int.Parse(this.ddlCategory.SelectedValue);
            //    var filtered = playlists.Where(p => p.CategoryId == selected);
            //    return filtered;
            //}

            var filter = ViewState["Filter"];

            if (filter != null && int.Parse(filter.ToString()) != -1)
            {
                var id = int.Parse(filter.ToString());

                return this.Playlists.GetAll().Where(p => p.CategoryId == id);
            }

            return this.Playlists.GetAll();
        }

        protected void CategoryChanged(object sender, EventArgs e)
        {
            var ddl = sender as DropDownList;
            var id = -1;

            if (ddl != null)
            {
                id = int.Parse(ddl.SelectedValue);
            }

            ViewState["Filter"] = id;
            ListViewPlaylists_GetData();
        }
    }
}