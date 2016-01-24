using BestPlaylists.Common;

namespace BestPlaylists.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        private ICollection<Comment> comments;

        private ICollection<Rating> ratings;

        private ICollection<Playlist> playlists;

        public User()
        {
            this.comments = new HashSet<Comment>();
            this.ratings = new HashSet<Rating>();
            this.playlists = new HashSet<Playlist>();
        }

        [Required]
        [MinLength(ModelsConstats.MinFirstNameLength, ErrorMessage = ModelsConstats.ErrorTooShort)]
        [MaxLength(ModelsConstats.MaxFirstNameLength, ErrorMessage = ModelsConstats.ErrorTooLong)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(ModelsConstats.MinLastNameLength, ErrorMessage = ModelsConstats.ErrorTooShort)]
        [MaxLength(ModelsConstats.MaxLastNameLength, ErrorMessage = ModelsConstats.ErrorTooLong)]
        public string LastName { get; set; }

        public string AvatarUrl { get; set; }

        public string FacebookAccount { get; set; }

        public string YouTubeAccount { get; set; }

        public virtual ICollection<Playlist> Playlists
        {
            get
            {
                return this.playlists;
            }

            set
            {
                this.playlists = value;
            }
        }

        public virtual ICollection<Comment> Comments
        {
            get
            {
                return this.comments;
            }

            set
            {
                this.comments = value;
            }
        }

        public virtual ICollection<Rating> Ratings
        {
            get
            {
                return this.ratings;
            }

            set
            {
                this.ratings = value;
            }
        }

        public ClaimsIdentity GenerateUserIdentity(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            return Task.FromResult(this.GenerateUserIdentity(manager));
        }
    }
}