using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdminBackoffice.Startup))]
namespace AdminBackoffice
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
