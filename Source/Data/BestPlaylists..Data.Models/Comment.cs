using BestPlaylists.Common;

namespace BestPlaylists.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public bool IsRemoved { get; set; }

        [Required]
        [MaxLength(ModelsConstats.MaxCommentTextLength, ErrorMessage = ModelsConstats.ErrorTooLong)]
        public string Text { get; set; }

        public DateTime CreationDate { get; set; }

        public int PlaylistId { get; set; }

        public virtual Playlist Playlist { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
