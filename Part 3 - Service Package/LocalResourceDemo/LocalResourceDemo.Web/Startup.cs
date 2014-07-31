using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LocalResourceDemo.Web.Startup))]
namespace LocalResourceDemo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
