using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JustAzureWebRole.Startup))]
namespace JustAzureWebRole
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
