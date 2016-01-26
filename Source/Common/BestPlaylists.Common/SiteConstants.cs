namespace BestPlaylists.Common
{
    public class SiteConstants
    {
        public const int HomePlaylistsSize = 10;
        public const int MinutesToKeepCache = 10;
        public const int ImageMaxSize = 100 * 10240;
        public const string DateFormatForFileNameImages = "dd-MMM-yyyy-HH-mm-ss";
        public const string Ascending = "Ascending";
        public const string Descending = "Descending";

        public const string CacheExpiresKey = "expires";
        public const string CachePlaylistsKey = "playlists";
        public const string CachedUsersKey = "usernames";
        public const string FilteredKey = "filtered";
        public const string CategoriesKey = "categories";
        public const string SortExpressionKey = "sortExpression";
        public const string SortDirectionKey = "sortDirection";

        public const string ServerPathImages = "~/Images/";
        public const string PublicPathImages = "/Images/";
        public const string DefaultAvatar = "/Images/default.jpg";
        public const string SuccessIconPath = "/Images/success-icon.png";
        public const string ErrorIconPath = "/Images/error-icon.png";

        public const string FacebookRegex = "((http|https):\\/\\/|)(www\\.|)facebook\\.com\\/[a-zA-Z0-9.]{1,}";
        public const string YoutubeRegex = "((http|https):\\/\\/|)(www\\.|)youtube\\.com\\/(user\\/)[a-zA-Z0-9]{1,}";
        public const string EmailRegex = "^([\\w\\.\\-_]+)?\\w+@[\\w-_]+(\\.\\w+){1,}$";

        public const string ErrorAccountLink = "Invalid Url";
        public const string WrongEmailAddress = "Email address is not valid";
        public const string ErrorFieldMinLength = "Field is too short";
        public const string ErrorFieldMaxLength = "Field is too long";
        public const string ErrorUploadMessageFormat = "Only images with size up to <strong>{0}Mb</strong> are allowed for uploading!";
    }
}
