namespace BestPlaylists.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        [Required]
        [Key]
        public int Id { get; set; }

        public bool IsRemoved { get; set; }

        public string Text { get; set; }

        public DateTime CreationDate { get; set; }

        public int PlaylistId { get; set; }

        public virtual Playlist Playlist { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
