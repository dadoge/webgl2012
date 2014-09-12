using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RPGMaster.Startup))]
namespace RPGMaster
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
