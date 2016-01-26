namespace BestPlaylists.Services.Data
{
    using System;
    using BestPlaylists.Data.Models;
    using BestPlaylists.Data.Repositories;
    using BestPlaylists.Services.Data.Contracts;

    public class VideoService : IVideoService
    {
        private readonly IRepository<Video> videosRepo;

        public VideoService(IRepository<Video> videosRepository)
        {
            this.videosRepo = videosRepository;
        }

        public void RemoveById(int id)
        {
            this.videosRepo.Delete(id);
            this.videosRepo.SaveChanges();
        }
    }
}
