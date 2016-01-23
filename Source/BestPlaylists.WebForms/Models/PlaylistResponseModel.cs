namespace BestPlaylists.WebForms.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class PlaylistResponseModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public double Rating { get; set; }

        public string Category { get; set; }

        public string Username { get; set; }
    }
}