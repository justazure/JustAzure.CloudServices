using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Web.Administration;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace JustAzureWebRole
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            using (var server = new ServerManager())
            {
                server.ApplicationPoolDefaults.ProcessModel.IdleTimeout = new TimeSpan(0, 120, 00);
                server.CommitChanges();
            }

            return base.OnStart();
        }
    }
}
