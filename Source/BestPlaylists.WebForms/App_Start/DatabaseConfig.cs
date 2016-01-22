namespace BestPlaylists.WebForms
{
    using System.Data.Entity;

    using BestPlaylists.Data;
    using BestPlaylists.Data.Migrations;

    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BestPlaylistsDbContext, Configuration>());
            BestPlaylistsDbContext.Create().Database.Initialize(true);
        }
    }
}