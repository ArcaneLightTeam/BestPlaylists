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
                for (int i = 0; i < DbConstants.musicGenres.Count; i++)
                {
                    var category = new Category()
                    {
                        Name = DbConstants.musicGenres[i]
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

            if (!context.Roles.Any(r => r.Name == DbConstants.RoleAdmin))
            {
                var roleAdmin = new IdentityRole
                {
                    Name = DbConstants.RoleAdmin
                };

                manager.Create(roleAdmin);
            }

            if (!context.Roles.Any(r => r.Name == DbConstants.RoleEditor))
            {
                var roleEditor = new IdentityRole()
                {
                    Name = DbConstants.RoleEditor
                };

                manager.Create(roleEditor);
            }
        }

        private void SeedUsers(BestPlaylistsDbContext context)
        {
            var store = new UserStore<User>(context);
            var manager = new UserManager<User>(store);

            if (!context.Users.Any(u => u.UserName == DbConstants.Admin1Name))
            {
                var user = new User()
                {
                    UserName = DbConstants.Admin1Name,
                    Email = DbConstants.Admin1Email,
                    FirstName = DbConstants.Admin1FirstName,
                    LastName = DbConstants.Admin1LastName
                };

                manager.Create(user, DbConstants.Admin1Pass);
                manager.AddToRole(user.Id, DbConstants.RoleAdmin);
            }

            if (!context.Users.Any(u => u.UserName == DbConstants.Editor1Name))
            {
                var user = new User()
                {
                    UserName = DbConstants.Editor1Name,
                    Email = DbConstants.Editor1Email,
                    FirstName = DbConstants.Editor1FirstName,
                    LastName = DbConstants.Editor1LastName
                };

                manager.Create(user, DbConstants.Editor1Pass);
                manager.AddToRole(user.Id, DbConstants.RoleEditor);
            }
        }

        private void SeedPlaylists(BestPlaylistsDbContext context)
        {
            var categoriesCount = context.Categories.Count();
            var admin1Id = context.Users.Single(u => u.UserName == DbConstants.Admin1Name).Id;

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
