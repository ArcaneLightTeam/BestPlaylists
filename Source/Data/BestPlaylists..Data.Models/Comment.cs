namespace BestPlaylists.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public bool IsRemoved { get; set; }

        public string Context { get; set; }

        public int PlaylistId { get; set; }

        public virtual Playlist Playlist { get; set; }
    }
}
