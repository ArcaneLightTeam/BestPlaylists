namespace BestPlaylists.Data
{
    using System.Data.Entity;

    using BestPlaylists.Data.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class BestPlaylistsDbContext : IdentityDbContext<User>, IBestPlaylistsDbContext
    {
        public BestPlaylistsDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Playlist> Playlists { get; set; }

        public virtual IDbSet<Video> Videos { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<Rating> Ratings { get; set; }

        public static BestPlaylistsDbContext Create()
        {
            return new BestPlaylistsDbContext();
        }
    }
}
