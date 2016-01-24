using System.Collections.Generic;

namespace BestPlaylists.Common
{
    public static class DbConstants
    {
        public const string Admin1Name = "Deyan";
        public const string Admin1FirstName = "Deyan";
        public const string Admin1LastName = "Todorov";
        public const string Admin1Email = "deyan@gmail.com";
        public const string Admin1Pass = "123123";

        public const string Editor1Name = "Editor";
        public const string Editor1FirstName = "Editor";
        public const string Editor1LastName = "Editor";
        public const string Editor1Email = "editor@gmail.com";
        public const string Editor1Pass = "123123";

        public const string RoleAdmin = "Admin";
        public const string RoleEditor = "Editor";

        public static List<string> musicGenres = new List<string>()
        {
            "Alternative Music",
            "Blues",
            "Classical Music",
            "Country Music",
            "Dance Music",
            "Easy Listening",
            "Electronic Music",
            "European Music (Folk / Pop)",
            "Hip Hop / Rap",
            "Indie Pop",
            "Inspirational (incl. Gospel)",
            "Asian Pop (J-Pop, K-pop)",
            "Jazz",
            "Latin Music",
            "New Age",
            "Opera",
            "Pop (Popular music)",
            "R&B / Soul",
            "Reggae",
            "Rock",
            "Singer / Songwriter (inc. Folk)",
            "World Music / Beats"
        };
    }
}
