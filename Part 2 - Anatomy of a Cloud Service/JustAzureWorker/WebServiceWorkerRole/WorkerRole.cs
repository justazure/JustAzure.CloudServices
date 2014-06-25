using System;
using System.Web.Http;
using System.Threading;

using Microsoft.Owin.Hosting;
using Microsoft.WindowsAzure.ServiceRuntime;

using Owin;

namespace WebServiceWorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        private class WebApiStartup
        {
            public void Configuration(IAppBuilder app)
            {
                var config = new HttpConfiguration();
                config.Routes.MapHttpRoute("Default", "{controller}/{id}", new { id = RouteParameter.Optional });
                app.UseWebApi(config);
            }
        }
        public override void Run()
        {
            var externalEndPoint = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["WebApi"];
            var baseUri = String.Format("{0}://{1}", externalEndPoint.Protocol, externalEndPoint.IPEndpoint);

            WebApp.Start<WebApiStartup>(new StartOptions(url: baseUri));

            while (true)
            {
                Thread.Sleep(10000);
            }
        }
    }
}
