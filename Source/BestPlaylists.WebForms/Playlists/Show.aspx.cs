namespace BestPlaylists.WebForms.Playlists
{
    using System;
    using System.Linq;
    using System.Security.Policy;
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

            int categoryNumber;
            int categoryFilterId;

            if (currentCategory != null && int.TryParse(currentCategory, out categoryNumber))
            {
                this.ddlCategory.SelectedIndex = categoryNumber;
            }

            IQueryable<Playlist> data = this.Playlists.GetAll();

            if (categoryFilter != null && int.TryParse(categoryFilter, out categoryFilterId) && categoryFilterId != -1)
            {
                data = data.Where(p => p.CategoryId == categoryFilterId);
            }

            if (!string.IsNullOrEmpty(search))
            {
                var searchTitle = search;
                data = data.Where(p => p.Title.Contains(searchTitle));
                this.SearchResultWord.InnerText = this.Server.HtmlDecode(search);
                this.SearchResultHeader.Visible = true;
            }
            else
            {
                this.SearchResultWord.InnerText = "";
                this.SearchResultHeader.Visible = false;
            }

            return data;
        }

        protected void Filter(object sender, EventArgs e)
        {

            this.Response.Redirect("/Playlists/Show.aspx?search=" + this.SearchTextBox?.Text + "&filter=" + this.ddlCategory.SelectedValue + "&cat=" + this.ddlCategory.SelectedIndex);
        }
    }
}