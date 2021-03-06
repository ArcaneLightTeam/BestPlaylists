﻿using BestPlaylists.Common;

namespace BestPlaylists.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Playlist
    {
        private ICollection<Video> videos;

        private ICollection<Comment> comments;

        private ICollection<Rating> ratings;

        public Playlist()
        {
            this.videos = new HashSet<Video>();
            this.comments = new HashSet<Comment>();
            this.ratings = new HashSet<Rating>();
        }

        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ModelsConstats.MinVideoTitleLength, ErrorMessage = ModelsConstats.ErrorTooShort)]
        [MaxLength(ModelsConstats.MaxVideoTitleLength, ErrorMessage = ModelsConstats.ErrorTooLong)]
        public string Title { get; set; }

        [Required]
        [MaxLength(ModelsConstats.MaxVideoDescriptionLength, ErrorMessage = ModelsConstats.ErrorTooLong)]
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsRemoved { get; set; }

        public bool IsPrivate { get; set; }

        public double CurrentRating { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

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

        public virtual ICollection<Video> Videos
        {
            get
            {
                return this.videos;
            }
            set
            {
                this.videos = value;
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
    }
}
