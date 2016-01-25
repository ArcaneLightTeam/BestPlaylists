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
            this.SeedComments(context);
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
            
            // Adding 20 more users
            for (int i = 1; i <= 20; i++)
            {
                var userName = DbConstants.Editor1Name + i;

                if (!context.Users.Any(u => u.UserName == userName))
                {
                    var user = new User()
                    {
                        UserName = userName,
                        Email = userName + "@gmail.com",
                        FirstName = userName,
                        LastName = userName
                    };

                    manager.Create(user, DbConstants.Editor1Pass);
                    manager.AddToRole(user.Id, DbConstants.RoleEditor);
                }
            }
        }

        private void SeedPlaylists(BestPlaylistsDbContext context)
        {
            var categoriesCount = context.Categories.Count();

            var allUsers = context
                .Users
                .Select(x => x.Id)
                .ToList();

            if (!context.Playlists.Any())
            {
                var randomGenerator = new RandomGenerator();

                for (int i = 0; i < 100; i++)
                {
                    var playlist = new Playlist()
                    {
                        Title = randomGenerator.GetRandowSentance(ModelsConstats.MinVideoTitleLength, ModelsConstats.MaxVideoTitleLength - 50),
                        Description = randomGenerator.GetRandowSentance(ModelsConstats.MinVideoTitleLength, ModelsConstats.MaxVideoDescriptionLength - 1800),
                        CreationDate = randomGenerator.GetRandomDate(DateTime.Now.AddYears(-i), DateTime.Now),
                        IsPrivate = (i % 2 == 0),
                        CurrentRating = randomGenerator.GetRandomNumber(1, 5) % 6,
                        CategoryId = randomGenerator.GetRandomNumber(1, categoriesCount),
                        UserId = allUsers[randomGenerator.GetRandomNumber(0, allUsers.Count - 1)],
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
                            Url = GetRandomVideo(),
                            UserId = context.Playlists.First(id => id.Id == i + 1).UserId
                        };

                        context.Videos.Add(video);
                    }

                    context.SaveChanges();
                }
            }
        }

        private void SeedComments(BestPlaylistsDbContext context)
        {
            if (!context.Comments.Any())
            {
                var randomGenerator = new RandomGenerator();
                var playLists = context.Playlists.ToList();
                var users = context.Users.Select(x => x.Id).ToList();

                foreach (var playlist in playLists)
                {
                    var commentsCount = randomGenerator.GetRandomNumber(3, 9);

                    for (int i = 0; i < commentsCount; i++)
                    {
                        playlist.Comments.Add(new Comment()
                        {
                            CreationDate = randomGenerator.GetRandomDate(DateTime.Now.AddYears(-i), DateTime.Now),
                            Text = randomGenerator.GetRandowSentance(ModelsConstats.MinVideoTitleLength, ModelsConstats.MaxVideoDescriptionLength - 1800),
                            UserId = users[randomGenerator.GetRandomNumber(0, users.Count - 1)]
                        });
                        
                        context.Playlists.AddOrUpdate(playlist);
                        context.SaveChanges();
                    }
                }
            }
        }

        private string GetRandomVideo()
        {
            var youTubeVideos = new List<string>()
            {
                "https://www.youtube.com/watch?v=bVRfloSVFFs",
                "https://www.youtube.com/watch?v=EIxfpanpyrs",
                "https://www.youtube.com/watch?v=KxlcEOY-7hg",
                "https://www.youtube.com/watch?v=1RjE915E4mg",
                "https://www.youtube.com/watch?v=OGWehEcKArE",
                "https://www.youtube.com/watch?v=BiADfKN_1Sc",
                "https://www.youtube.com/watch?v=62OL7SC5_A8",
                "https://www.youtube.com/watch?v=O6gkm-nklyw",
                "https://www.youtube.com/watch?v=lPmOcJ9YdYw",
                "https://www.youtube.com/watch?v=b6m-XlOxjbk",
                "https://www.youtube.com/watch?v=tPxcFm_S1qc",
                "https://www.youtube.com/watch?v=LTIJZJsLG8o",
                "https://www.youtube.com/watch?v=q2DqctUI8ho",
                "https://www.youtube.com/watch?v=Nqh3LI6l1fk",
                "https://www.youtube.com/watch?v=oq2mykyhl-0",
                "https://www.youtube.com/watch?v=6H9aHntpjcE",
                "https://www.youtube.com/watch?v=gojyDYI8bzY",
                "https://www.youtube.com/watch?v=nWLYUdejPWc",
                "https://www.youtube.com/watch?v=4Esni96oBdQ",
                "https://www.youtube.com/watch?v=1CxPGDZJPDw",
                "https://www.youtube.com/watch?v=fk9iJougDFE",
                "https://www.youtube.com/watch?v=GF60Iuh643I",
                "https://www.youtube.com/watch?v=59Zcx9YbZxI",
                "https://www.youtube.com/watch?v=VAZ5te90vlc",
                "https://www.youtube.com/watch?v=APoCpZOrI4w",
                "https://www.youtube.com/watch?v=APoCpZOrI4w",
                "https://www.youtube.com/watch?v=V4LnorVVxfw",
                "https://www.youtube.com/watch?v=Cs4OUFAr5Hc",
                "https://www.youtube.com/watch?v=MBsEPgjE7KU",
                "https://www.youtube.com/watch?v=axm-N9L2c_E",
                "https://www.youtube.com/watch?v=dGpZEuXoFWw",
                "https://www.youtube.com/watch?v=JgzgcAlpxNU",
                "https://www.youtube.com/watch?v=0FEYvKxCnYw",
                "https://www.youtube.com/watch?v=MveqXxB12YA",
                "https://www.youtube.com/watch?v=-4FlKZIo8B4",
                "https://www.youtube.com/watch?v=JWZMzcmqMwc",
                "https://www.youtube.com/watch?v=Snw0rEWyvLk",
                "https://www.youtube.com/watch?v=DaxB9y--CGI",
                "https://www.youtube.com/watch?v=Md7bfNTR0TM",
                "https://www.youtube.com/watch?v=zmIxovkV3Ro",
                "https://www.youtube.com/watch?v=tntOCGkgt98",
                "https://www.youtube.com/watch?v=CE-JlvmnRtY",
                "https://www.youtube.com/watch?v=UIrEM_9qvZU",
                "https://www.youtube.com/watch?v=25OUFtdno8U",
                "https://www.youtube.com/watch?v=HxM46vRJMZs",
                "https://www.youtube.com/watch?v=G8KpPw303PY",
                "https://www.youtube.com/watch?v=mW3S0u8bj58",
                "https://www.youtube.com/watch?v=-hIugp7p5O0",
                "https://www.youtube.com/watch?v=9Mk_t78IeO8",
                "https://www.youtube.com/watch?v=tcPVzzyB7OI",
                "https://www.youtube.com/watch?v=JgjXG_9L_Jw",
            };

            var randomGenerator = new RandomGenerator();
            var videoUrl = youTubeVideos[randomGenerator.GetRandomNumber(0, youTubeVideos.Count - 1)];

            return videoUrl;
        }
    }
}