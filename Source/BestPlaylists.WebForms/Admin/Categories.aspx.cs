using System;
namespace BestPlaylists.WebForms.Admin
{
    using System.Linq;
    using System.Security.Claims;
    using System.Web.Security;

    using BestPlaylists.Data.Models;
    using BestPlaylists.Services.Data.Contracts;

    using Microsoft.Ajax.Utilities;

    using Ninject;

    public partial class Categories : System.Web.UI.Page
    {
        [Inject]
        public ICategoryService CategoryService { get; set; }

        protected void Page_PreRender(object sender, EventArgs e)
        {
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<Category> ListViewCategories_GetData()
        {
            return this.CategoryService.GetAll();
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void ListViewCategories_DeleteItem(int ID)
        {
            this.CategoryService.Remove(ID);
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void ListViewCategories_UpdateItem(int ID)
        {
            Category item = this.CategoryService.GetById(ID).FirstOrDefault();
            if (item == null)
            {
                // The item wasn't found
                this.ModelState.AddModelError("", String.Format("Item with id {0} was not found", ID));
                return;
            }
            this.TryUpdateModel(item);
            if (this.ModelState.IsValid)
            {
                this.CategoryService.Update(item);
            }
        }

        public void ListViewCategories_InsertItem()
        {
            var item = new Category();
            this.TryUpdateModel(item);

            if (this.ModelState.IsValid)
            {
                this.CategoryService.Add(item.Name);
            }
        }
    }
}