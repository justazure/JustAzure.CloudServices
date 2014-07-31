using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace ConfigWeb
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var connectionString = RoleEnvironment.GetConfigurationSettingValue("DatabaseConnectionString");
            Literal1.Text = "<p>Connection String (from RoleEnvironment): " + connectionString + "</p>";

            connectionString = CloudConfigurationManager.GetSetting("DatabaseConnectionString");
            Literal1.Text += "<p>Connection String (from CloudConfigurationManager): " + connectionString + "</p>";

            Literal2.Text = "<p>Environment Variables:<br /><ul>";
            foreach (DictionaryEntry variable in Environment.GetEnvironmentVariables())
            {
                Literal2.Text += "<li>" + variable.Key + ": " + variable.Value + "</li>";
            }
            Literal2.Text += "</ul></p>";
        }
    }
}