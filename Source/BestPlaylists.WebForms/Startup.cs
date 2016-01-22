using BestPlaylists.WebForms;

using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace BestPlaylists.WebForms
{
    using Owin;

    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            this.ConfigureAuth(app);
        }
    }
}
