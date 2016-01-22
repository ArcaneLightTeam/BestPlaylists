using BestPlaylists.WebForms;

[assembly: OwinStartupAttribute(typeof(Startup))]
namespace BestPlaylists.WebForms
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            this.ConfigureAuth(app);
        }
    }
}
