using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace ConfigWeb
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            RoleEnvironment.Changing += OnChanging;
            RoleEnvironment.Changed += OnChanged;

            return base.OnStart();
        }

        private void OnChanging(object sender, RoleEnvironmentChangingEventArgs e)
        {
            foreach (var change in e.Changes)
            {
                var configChange = change as RoleEnvironmentConfigurationSettingChange;
                if (configChange != null)
                {
                    if (configChange.ConfigurationSettingName == "DatabaseConnectionString")
                    {
                        // Our database connectionstring changed.
                        // Maybe we need to restart the instance to make sure our cached objects no longer use the previous value.
                        e.Cancel = true;
                    }
                }
            }
        }

        private void OnChanged(object sender, RoleEnvironmentChangedEventArgs e)
        {
            
        }
    }
}
