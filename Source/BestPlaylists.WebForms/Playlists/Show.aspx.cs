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
                var allCategories = this.Categories.GetAll();

                foreach (var allCategory in allCategories)
                {
                    ddlCategory.Items.Add(new ListItem()
                    {
                        Text = allCategory.Name,
                        Value = allCategory.Id.ToString()
                    });
                }
            }
        }

        public IQueryable<Playlist> ListViewPlaylists_GetData()
        {
            var playlists = this.Playlists.GetAll();
            var selected = 0;

            if (this.ViewState["Filter"] != null && this.ddlCategory.SelectedValue != "-1")
            {
                selected = int.Parse(this.ViewState["Filter"].ToString());
                var filtered = playlists.Where(p => p.CategoryId == selected);
                return filtered;
            }
            else if (this.ddlCategory.SelectedValue != "-1")
            {
                selected = int.Parse(this.ddlCategory.SelectedValue);
                var filtered = playlists.Where(p => p.CategoryId == selected);
                return filtered;
            }

            return playlists;
        }

        protected void CategoryChanged(object sender, EventArgs e)
        {
            DropDownList ddlCountry = (DropDownList)sender;
            ViewState["Filter"] = ddlCountry.SelectedValue;
            this.ListViewPlaylists_GetData();
        }
    }
}