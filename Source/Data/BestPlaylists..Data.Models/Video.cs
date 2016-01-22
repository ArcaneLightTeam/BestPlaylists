namespace BestPlaylists.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Video
    {
        [Required]
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int PlaylistId { get; set; }

        public virtual Playlist Playlist { get; set; }

        public string Url { get; set; }
    }
}