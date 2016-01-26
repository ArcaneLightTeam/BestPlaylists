namespace BestPlaylists.WebForms.Account
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Data.Models;
    using Microsoft.AspNet.Identity;
    using BestPlaylists.Common;
    using Ninject;
    using Services.Data.Contracts;

    public partial class YourPlaylists : Page
    {
        [Inject]
        public IPlaylistService PlaylistService { get; set; }

        [Inject]
        public ICategoryService CategoryService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session[SiteConstants.FilteredKey] = null;
                this.Session[SiteConstants.CachePlaylistsKey] = null;
                this.Session[SiteConstants.CategoriesKey] = null;
            }

            IList<Playlist> playlistsOfThisUser = this.Session[SiteConstants.FilteredKey] as List<Playlist>;
            if (playlistsOfThisUser == null)
            {
                playlistsOfThisUser = this.Session[SiteConstants.CachePlaylistsKey] as List<Playlist>;
            }

            if (playlistsOfThisUser == null)
            {
                // query to db
                string userId = this.User.Identity.GetUserId();
                playlistsOfThisUser = this.PlaylistService.GetAll().Where(c => c.UserId == userId).ToList();
                this.Session[SiteConstants.CachePlaylistsKey] = playlistsOfThisUser;
            }

            IList<Category> categories = this.Session[SiteConstants.CategoriesKey] as List<Category>;
            if (categories == null)
            {
                // query to db
                categories = CategoryService.GetAll().ToList();
                this.Session[SiteConstants.CategoriesKey] = categories;
            }

            this.ddlCategory.DataSource = categories;
            this.ddlCategory.DataBind();

            this.gvUserPlayLists.DataSource = playlistsOfThisUser;
            this.gvUserPlayLists.DataBind();
        }

        protected void CategoryChanged(object sender, EventArgs e)
        {
            DropDownList dropDownCategories = (sender as DropDownList);
            IList<Playlist> playlists = this.Session[SiteConstants.CachePlaylistsKey] as List<Playlist>;
            if (dropDownCategories == null)
            {
                return;
            }

            IList<Category> categories = this.Session[SiteConstants.CategoriesKey] as List<Category>;
            if (categories == null)
            {
                return;
            }

            int categoryId = 0;
            if (!int.TryParse(dropDownCategories.SelectedValue, out categoryId))
            {
                return;
            }

            if (categoryId < 0)
            {
                this.gvUserPlayLists.DataSource = this.Session[SiteConstants.CachePlaylistsKey] as List<Playlist>;
                this.Session[SiteConstants.FilteredKey] = null;
            }
            else
            {
                Category selectedCategory = categories.FirstOrDefault(c => c.Id == categoryId);
                IList<Playlist> filteredPlaylists = playlists.Where(p => p.Category.Name == selectedCategory.Name).ToList();
                this.gvUserPlayLists.DataSource = filteredPlaylists;
                this.Session[SiteConstants.FilteredKey] = filteredPlaylists;
            }


            this.gvUserPlayLists.DataBind();
        }

        protected void gvUserPlayLists_Sorting(object sender, GridViewSortEventArgs e)
        {
            // If sorting fails for some reason to try save playlists in session.
            IList<Playlist> playlists = this.gvUserPlayLists.DataSource as List<Playlist>;
            SortDirection sortDirection = this.GetSortDirectionFromViewState(e.SortExpression);
            if (sortDirection == SortDirection.Ascending)
            {
                this.gvUserPlayLists.DataSource =
                    playlists.OrderBy(
                        c => c.GetType()
                        .GetProperty(e.SortExpression)
                        .GetValue(c))
                    .ToList();
            }
            else
            {
                this.gvUserPlayLists.DataSource =
                    playlists.OrderByDescending(
                        c => c.GetType()
                        .GetProperty(e.SortExpression)
                        .GetValue(c))
                    .ToList();
            }

            this.gvUserPlayLists.DataBind();
            //this.updatePanel.Update();
        }

        protected void gvUserPlayLists_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //this.gvUserPlayLists.SetPageIndex(2); // This rises the same event here
            this.gvUserPlayLists.PageIndex = e.NewPageIndex;
            if (this.ViewState[SiteConstants.SortDirectionKey] != null)
            {
                string sortExpression = this.ViewState[SiteConstants.SortExpressionKey].ToString();
                this.gvUserPlayLists.Sort(sortExpression, this.GetSortDirectionFromViewState(sortExpression));
            }
        }

        private SortDirection GetSortDirectionFromViewState(string sortExpression)
        {
            if (this.ViewState[SiteConstants.SortDirectionKey] == null)
            {
                this.ViewState[SiteConstants.SortDirectionKey] = SiteConstants.Ascending;
            }
            else
            {
                if (this.ViewState[SiteConstants.SortDirectionKey].ToString() == SiteConstants.Ascending)
                {
                    this.ViewState[SiteConstants.SortDirectionKey] =
                        this.ViewState[SiteConstants.SortExpressionKey].ToString() == sortExpression ?
                        SiteConstants.Descending : SiteConstants.Ascending;
                }
                else 
                {
                    this.ViewState[SiteConstants.SortDirectionKey] = SiteConstants.Ascending;
                }
            }

            this.ViewState[SiteConstants.SortExpressionKey] = sortExpression;
            return (SortDirection)Enum.Parse(typeof(SortDirection), this.ViewState[SiteConstants.SortDirectionKey].ToString());
        }
    }
}