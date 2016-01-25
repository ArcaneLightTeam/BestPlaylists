namespace BestPlaylists.Common
{
    public class SiteConstants
    {
        public const int HomePlaylistsSize = 10;
        public const int MinutesToKeepCache = 10;
        public const string CacheExpiresKey = "expires";
        public const string CachePlaylistsKey = "playlists";
        public const int ImageMaxSize = 100 * 10240;
        public const string ServerPathImages = "~/Images/";
        public const string PublicPathImages = "/Images/";
        public const string ErrorUploadMessageFormat = "Only images with size up to <strong>{0}Mb</strong> are allowed for uploading!";
        public const string DateFormatForFileNameImages = "dd-MMM-yyyy-HH-mm-ss";
    }
}
