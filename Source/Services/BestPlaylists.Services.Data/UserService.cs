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

        public IQueryable<User> GetAll()
        {
            return this.usersRepo.All();
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

        public void Remove(string id)
        {
            User userToDelete = this.usersRepo.GetById(id);

            if (userToDelete != null)
            {
                this.usersRepo.Delete(userToDelete);

                this.usersRepo.SaveChanges();
            }
        }
    }
}