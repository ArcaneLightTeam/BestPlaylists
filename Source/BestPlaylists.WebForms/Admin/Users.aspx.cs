namespace BestPlaylists.WebForms.Admin
{
    using System;
    using System.Linq;
    using System.Web.UI;

    using BestPlaylists.Data.Models;
    using BestPlaylists.Services.Data.Contracts;

    using Ninject;

    public partial class Users : Page
    {
        [Inject]
        public IUserService UserService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<User> UsersView_GetData()
        {
            return this.UserService.GetAll();
        }

        public void UsersView_Update(string id)
        {
            User item = this.UserService.GetById(id);

            if (item == null)
            {
                this.ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }
            this.TryUpdateModel(item);

            if (this.ModelState.IsValid)
            {
                this.UserService.Update(item);

            }
        }
    }
}