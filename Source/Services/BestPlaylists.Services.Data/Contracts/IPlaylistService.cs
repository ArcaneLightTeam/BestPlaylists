namespace BestPlaylists.Services.Data.Contracts
{
    using System.Linq;

    using BestPlaylists.Data.Models;

    public interface IPlaylistService
    {
        IQueryable<Playlist> GetById(int id);

        IQueryable<Playlist> GetAll();

        IQueryable<Playlist> GetByCategory(string categoryName);

        int Add(string title, string description, int categoryId, int userId, string imageUrl, bool isPrivate);
        
        void Update(Playlist playlist);

        void Remove(Playlist playlist);

        void RemoveById(int id);
    }
}
