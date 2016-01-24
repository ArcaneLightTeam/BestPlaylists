using BestPlaylists.Common;

namespace BestPlaylists.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Rating
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int PlaylistId { get; set; }

        public virtual Playlist Playlist { get; set; }

        [Range(ModelsConstats.MinRating, ModelsConstats.MaxRating)]
        public int Value { get; set; }
    }
}