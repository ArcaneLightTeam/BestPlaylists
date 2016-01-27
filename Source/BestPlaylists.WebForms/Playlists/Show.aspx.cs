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
            var currentCategory = this.Request.QueryString["cat"];

            var categoryFilter = this.Request.QueryString["filter"];

            var search = this.Request.QueryString["search"];

            if (currentCategory != null)
            {
                this.ddlCategory.SelectedIndex = int.Parse(currentCategory);
            }

            IQueryable<Playlist> data = this.Playlists.GetAll();

            if (categoryFilter != null && int.Parse(categoryFilter) != -1)
            {
                var id = int.Parse(categoryFilter.ToString());
                data = data.Where(p => p.CategoryId == id);
            }

            if (search != null && search.ToString().Length > 0)
            {
                var searchTitle = search.ToString();
                data = data.Where(p => p.Title.Contains(searchTitle));
            }

            return data;
        }

        protected void Filter(object sender, EventArgs e)
        {

            this.Response.Redirect("/Playlists/Show.aspx?search=" + this.SearchTextBox?.Text + "&filter=" + this.ddlCategory.SelectedValue + "&cat=" + this.ddlCategory.SelectedIndex);
        }
    }
}