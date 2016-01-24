using System;
using System.Collections.Generic;
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
            this.SeedPlaylists(context);
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

        private void SeedPlaylists(BestPlaylistsDbContext context)
        {
            var categoriesCount = context.Categories.Count();
            var admin1Id = context.Users.Single(u => u.UserName == GlobalConstants.Admin1Name).Id;

            if (!context.Playlists.Any())
            {
                var randomGenerator = new RandomGenerator();

                for (int i = 0; i < 100; i++)
                {
                    var videos = new List<Video>();

                    var playlist = new Playlist()
                    {
                        Title = randomGenerator.GetRandomString(ModelsConstats.MinVideoTitleLength, ModelsConstats.MaxVideoTitleLength),
                        Description = randomGenerator.GetRandomString(ModelsConstats.MinVideoTitleLength, ModelsConstats.MaxVideoDescriptionLength),
                        CreationDate = randomGenerator.GetRandomDate(DateTime.Now.AddYears(-i), DateTime.Now),
                        IsPrivate = (i % 2 == 0),
                        CurrentRating = randomGenerator.GetRandomNumber(i, 100) / 2,
                        CategoryId = randomGenerator.GetRandomNumber(1, categoriesCount),
                        UserId = admin1Id,
                    };

                    context.Playlists.Add(playlist);

                    if (i % 10 == 0)
                    {
                        context.SaveChanges();
                    }
                }

                var playlistsCount = context.Playlists.Count();
                for (int i = 0; i < playlistsCount; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        var video = new Video()
                        {
                            PlaylistId = i + 1,
                            Url = "url",
                            UserId = admin1Id
                        };

                        context.Videos.Add(video);
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
