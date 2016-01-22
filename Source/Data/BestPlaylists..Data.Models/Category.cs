namespace BestPlaylists.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        private ICollection<Playlist> playlists;

        public Category()
        {
            this.playlists = new HashSet<Playlist>();
        }

        [Required]
        [Key]
        public int Id { get; set; }

        public bool IsRemoved { get; set; }

        public string Name { get; set; }

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
    }
}