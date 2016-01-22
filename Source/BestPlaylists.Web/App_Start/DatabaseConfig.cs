namespace BestPlaylists.Web
{
    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PayItForwardDbContext, Configuration>());
            PayItForwardDbContext.Create().Database.Initialize(true);
        }
    }
}