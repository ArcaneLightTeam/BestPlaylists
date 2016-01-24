using BestPlaylists.Common;

namespace BestPlaylists.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Video
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int PlaylistId { get; set; }

        public virtual Playlist Playlist { get; set; }
        
        [MaxLength(ModelsConstats.MaxVideoUrlsLength, ErrorMessage = ModelsConstats.ErrorTooLong)]
        public string Url { get; set; }
    }
}