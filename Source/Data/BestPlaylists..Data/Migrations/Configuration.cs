using System.Linq;
using BestPlaylists.Common;
using BestPlaylists.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BestPlaylists.Data.Migrations
{
    using System.Data.Entity.Migrations;

    using BestPlaylists.Data;

    public sealed class Configuration : DbMigrationsConfiguration<BestPlaylistsDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BestPlaylistsDbContext context)
        {
            this.SeedCategories(context);
            this.SeedRoles(context);
            this.SeedUsers(context);
        }

        private void SeedCategories(BestPlaylistsDbContext context)
        {
            if (!context.Categories.Any())
            {
                for (int i = 0; i < GlobalConstants.musicGenres.Count; i++)
                {
                    var category = new Category()
                    {
                        Name = GlobalConstants.musicGenres[i]
                    };

                    context.Categories.Add(category);

                    if (i % 20 == 0)
                    {
                        context.SaveChanges();
                    }
                }
            }
        }

        private void SeedRoles(BestPlaylistsDbContext context)
        {
            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);

            if (!context.Roles.Any(r => r.Name == GlobalConstants.RoleAdmin))
            {
                var roleAdmin = new IdentityRole
                {
                    Name = GlobalConstants.RoleAdmin
                };

                manager.Create(roleAdmin);
            }

            if (!context.Roles.Any(r => r.Name == GlobalConstants.RoleEditor))
            {
                var roleEditor = new IdentityRole()
                {
                    Name = GlobalConstants.RoleEditor
                };

                manager.Create(roleEditor);
            }
        }

        private void SeedUsers(BestPlaylistsDbContext context)
        {
            var store = new UserStore<User>(context);
            var manager = new UserManager<User>(store);

            if (!context.Users.Any(u => u.UserName == GlobalConstants.Admin1Name))
            {
                var user = new User()
                {
                    UserName = GlobalConstants.Admin1Name,
                    Email = GlobalConstants.Admin1Email,
                    FirstName = GlobalConstants.Admin1FirstName,
                    LastName = GlobalConstants.Admin1LastName
                };

                manager.Create(user, GlobalConstants.Admin1Pass);
                manager.AddToRole(user.Id, GlobalConstants.RoleAdmin);
            }

            if (!context.Users.Any(u => u.UserName == GlobalConstants.Editor1Name))
            {
                var user = new User()
                {
                    UserName = GlobalConstants.Editor1Name,
                    Email = GlobalConstants.Editor1Email,
                    FirstName = GlobalConstants.Editor1FirstName,
                    LastName = GlobalConstants.Editor1LastName
                };

                manager.Create(user, GlobalConstants.Editor1Pass);
                manager.AddToRole(user.Id, GlobalConstants.RoleEditor);
            }
        }
    }
}
