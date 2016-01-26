using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BestPlaylists.Data.Models;
using BestPlaylists.Services.Data.Contracts;
using Ninject;

namespace BestPlaylists.WebForms.Playlists
{
    public partial class Edit : Page
    {
        [Inject]
        public IPlaylistService PlaylistsService { get; set; }

        [Inject]
        public ICategoryService Categories { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.ddlCategory.DataSource = this.Categories.GetAll().ToList();
                this.ddlCategory.DataBind();
                string queryParamId = this.Request.Params.GetValues("id")?[0];
                this.ViewState["id"] = queryParamId;
                int playlistId = 0;
                if (!int.TryParse(queryParamId, out playlistId))
                {
                    this.playlistNotFoundPanel.Visible = true;
                    this.editPlaylistPanel.Visible = false;
                    return;
                }

                Playlist playlistDetails = this.PlaylistsService.GetById(playlistId);
                if (playlistDetails == null)
                {
                    this.playlistNotFoundPanel.Visible = true;
                    this.editPlaylistPanel.Visible = false;
                    return;
                }

                this.tbTitle.Text = playlistDetails.Title;
                this.tbDescription.Text = playlistDetails.Description;
                this.ddlCategory.SelectedValue = playlistDetails.CategoryId.ToString();
                this.cbPrivate.Checked = playlistDetails.IsPrivate;
                this.editPlaylistPanel.Visible = true;
            }
        }

        protected void BtnUpdatePlaylist_Click(object sender, EventArgs e)
        {
            int playlistId = int.Parse(this.ViewState["id"].ToString());
            Playlist playlistDetails = this.PlaylistsService.GetById(playlistId);
            playlistDetails.Title = this.tbTitle.Text;
            playlistDetails.Description = this.tbDescription.Text;
            int categoryId = -1;

            if (!int.TryParse(this.ddlCategory.SelectedValue, out categoryId))
            {
                return;
            }

            playlistDetails.CategoryId = categoryId;
            playlistDetails.IsPrivate = this.cbPrivate.Checked;
            this.PlaylistsService.Update(playlistDetails);
            this.Response.Redirect("/Playlists/Details?id=" + playlistDetails.Id);
        }
    }
}