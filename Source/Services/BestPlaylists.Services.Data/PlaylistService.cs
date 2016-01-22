namespace BestPlaylists.Services.Data
{
    using System;
    using System.Linq;

    using BestPlaylists.Data.Models;
    using BestPlaylists.Data.Repositories;
    using BestPlaylists.Services.Data.Contracts;

    public class PlaylistService : IPlaylistService
    {
        private readonly IRepository<Playlist> playlistRepo;
        private readonly IRepository<User> usersRepo;

        public PlaylistService(IRepository<Playlist> playlistRepo, IRepository<User> usersRepo)
        {
            this.playlistRepo = playlistRepo;
            this.usersRepo = usersRepo;
        }

        public IQueryable<Playlist> GetById(int id)
        {
            return this.playlistRepo.All().Where(pr => pr.Id == id);
        }

        public IQueryable<Playlist> GetByCategory(string categoryName)
        {
            return
                this.playlistRepo.All()
                    .Where(x => (!x.IsRemoved) && x.Category.Name == categoryName)
                    .OrderByDescending(c => c.Title);

        }

        public IQueryable<Playlist> GetAll()
        {
            return this.playlistRepo.All().Where(x => (!x.IsRemoved)).OrderByDescending(c => c.Title);

        }


        public int Add(string title, string description, int categoryId, string userId, bool isPrivate)
        {
            var playlistToAdd = new Playlist
            {
                CreationDate = DateTime.Now,
                Title = title,
                Description = description,
                CategoryId = categoryId,
                UserId = userId,
                IsRemoved = false,
                IsPrivate = isPrivate
            };

            this.playlistRepo.Add(playlistToAdd);
            this.playlistRepo.SaveChanges();

            return playlistToAdd.Id;
        }

        public void Update(Playlist project)
        {
            this.playlistRepo.Update(project);
            this.playlistRepo.SaveChanges();
        }

        public void Remove(Playlist project)
        {
            project.IsRemoved = true;
            this.playlistRepo.Update(project);
            this.playlistRepo.SaveChanges();
        }

        public void RemoveById(int id)
        {
            Playlist projectToRemove = this.playlistRepo.GetById(id);
            projectToRemove.IsRemoved = true;
            this.playlistRepo.Update(projectToRemove);
            this.playlistRepo.SaveChanges();
        }
    }
}