namespace BestPlaylists.Services.Data
{
    using System;
    using System.Linq;

    using BestPlaylists.Data.Models;
    using BestPlaylists.Data.Repositories;
    using BestPlaylists.Services.Data.Contracts;

    public class UserService : IUserService
    {
        private readonly IRepository<User> usersRepo;

        public UserService(IRepository<User> usersRepo)
        {
            this.usersRepo = usersRepo;
        }

        public User GetById(string id)
        {
            return this.usersRepo.GetById(id);
        }

        public User GetByUserName(string userName)
        {
            return this.usersRepo.All().FirstOrDefault(x => x.UserName == userName);
        }

        public void Update(User user)
        {
            this.usersRepo.Update(user);
            this.usersRepo.SaveChanges();
        }

        public IQueryable<User> All()
        {
            return this.usersRepo.All();
        }
    }
}