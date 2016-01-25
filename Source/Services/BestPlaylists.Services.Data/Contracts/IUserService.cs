namespace BestPlaylists.Services.Data.Contracts
{
    using System.Linq;
    using BestPlaylists.Data.Models;

    public interface IUserService
    {
        User GetById(string id);

        User GetByUserName(string userName);

        void Update(User user);

        IQueryable<User> All();
    }
}
