namespace BestPlaylists.Data.Migrations
{
    using System.Data.Entity.Migrations;

    using BestPlaylists.Data;

    public sealed class Configuration : DbMigrationsConfiguration<BestPlaylistsDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BestPlaylistsDbContext context)
        {
            this.SeedUsers(context);
        }

        private void SeedUsers(BestPlaylistsDbContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
