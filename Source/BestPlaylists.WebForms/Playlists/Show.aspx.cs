namespace BestPlaylists.WebForms.Playlists
{
    using System;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using BestPlaylists.Data.Models;
    using BestPlaylists.Services.Data.Contracts;
    using Ninject;

    public partial class Show : Page
    {
        [Inject]
        public IPlaylistService Playlists { get; set; }

        [Inject]
        public ICategoryService Categories { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.ddlCategory.DataSource = this.Categories.GetAll().ToList();
                this.ddlCategory.DataBind();
            }
        }

        public IQueryable<Playlist> ListViewPlaylists_GetData()
        {
            var filter = this.ViewState["Filter"];

            var search = this.ViewState["Search"];

            IQueryable<Playlist> data = this.Playlists.GetAll();

            if (filter != null && int.Parse(filter.ToString()) != -1)
            {
                var id = int.Parse(filter.ToString());
                data = data.Where(p => p.CategoryId == id);
            }

            if (search != null && search.ToString().Length > 0)
            {
                var searchTitle = search.ToString();
               data = data.Where(p => p.Title.Contains(searchTitle));
            }

            return data;
        }

        protected void CategoryChanged(object sender, EventArgs e)
        {
            var ddl = sender as DropDownList;
            var id = -1;

            if (ddl != null)
            {
                id = int.Parse(ddl.SelectedValue);
            }

            this.ViewState["Filter"] = id;
            this.ListViewPlaylists_GetData();
            this.gvPlayLists.DataBind();
        }

        protected void SearchTitle(object sender, EventArgs e)
        {
            var searchBox = this.SearchTextBox;
            var searchTitle = "";

            if (searchBox != null)
            {
                searchTitle = searchBox.Text;
            }

            this.ViewState["Search"] = searchTitle;
            this.ListViewPlaylists_GetData();
            this.gvPlayLists.DataBind();
        }
    }
}