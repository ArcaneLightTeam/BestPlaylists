using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestPlaylists.WebForms.Account
{
    using System.Data;
    using Data.Models;
    using Microsoft.AspNet.Identity;
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
                this.Session["filtered"] = null;
                this.Session["playlists"] = null;
                this.Session["categories"] = null;
            }

            IList<Playlist> playlistsOfThisUser = this.Session["filtered"] as List<Playlist>;
            if (playlistsOfThisUser == null)
            {
                playlistsOfThisUser = this.Session["playlists"] as List<Playlist>;
            }

            if (playlistsOfThisUser == null)
            {
                // query to db
                string userId = this.User.Identity.GetUserId();
                playlistsOfThisUser = this.PlaylistService.GetAll().Where(c => c.UserId == userId).ToList();
                this.Session["playlists"] = playlistsOfThisUser;
            }

            IList<Category> categories = this.Session["categories"] as List<Category>;
            if (categories == null)
            {
                // query to db
                categories = CategoryService.GetAll().ToList();
                this.Session["categories"] = categories;
            }

            this.ddlCategory.DataSource = categories;
            this.ddlCategory.DataBind();

            this.gvUserPlayLists.DataSource = playlistsOfThisUser;
            this.gvUserPlayLists.DataBind();
        }

        protected void CategoryChanged(object sender, EventArgs e)
        {
            DropDownList dropDownCategories = (sender as DropDownList);
            IList<Playlist> playlists = this.Session["playlists"] as List<Playlist>;
            if (dropDownCategories == null)
            {
                return;
            }

            IList<Category> categories = this.Session["categories"] as List<Category>;
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
                this.gvUserPlayLists.DataSource = this.Session["playlists"] as List<Playlist>;
                this.Session["filtered"] = null;
            }
            else
            {
                Category selectedCategory = categories.FirstOrDefault(c => c.Id == categoryId);
                IList<Playlist> filteredPlaylists = playlists.Where(p => p.Category.Name == selectedCategory.Name).ToList();
                this.gvUserPlayLists.DataSource = filteredPlaylists;
                this.Session["filtered"] = filteredPlaylists;
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
            if (this.ViewState["sortDirection"] != null)
            {
                string sortExpression = this.ViewState["sortExpression"].ToString();
                this.gvUserPlayLists.Sort(sortExpression, this.GetSortDirectionFromViewState(sortExpression));
            }
        }

        private SortDirection GetSortDirectionFromViewState(string sortExpression)
        {
            if (this.ViewState["sortDirection"] == null)
            {
                this.ViewState["sortDirection"] = "Ascending";
            }
            else
            {
                if (this.ViewState["sortDirection"].ToString() == "Ascending")
                {
                    this.ViewState["sortDirection"] =
                        this.ViewState["sortExpression"].ToString() == sortExpression ? "Descending" : "Ascending";
                }
                else if (this.ViewState["sortDirection"].ToString() == "Descending")
                {
                    this.ViewState["sortDirection"] = "Ascending";
                }
            }
            this.ViewState["sortExpression"] = sortExpression;
            return (SortDirection)Enum.Parse(typeof(SortDirection), this.ViewState["sortDirection"].ToString());
        }
    }
}