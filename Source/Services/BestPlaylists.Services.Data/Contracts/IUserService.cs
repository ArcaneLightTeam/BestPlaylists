namespace BestPlaylists.Services.Data.Contracts
{
    using System.Linq;

    using BestPlaylists.Data.Models;

    public interface IUserService
    {
        IQueryable<User> GetAll();

        User GetById(string id);

        User GetByUserName(string userName);

        void Update(User user);

        void Remove(string id);
    }
}
