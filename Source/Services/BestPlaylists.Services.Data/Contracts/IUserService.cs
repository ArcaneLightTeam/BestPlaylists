namespace BestPlaylists.Services.Data.Contracts
{
    using BestPlaylists.Data.Models;

    public interface IUserService
    {
        User GetById(string id);

        User GetByUserName(string userName);

        void Update(User user);
    }
}
