using AchievementsHelper.Helpers;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;
using System.Windows;

namespace AchievementsHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IConfiguration Configuration { get; private set; }
        public static SecretsConfig Secrets = new SecretsConfig();
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<App>(); 

            Configuration = builder.Build();

            string apiKey = Configuration["steamAPIKey"];
            Secrets.ApiKey = apiKey;
        }
    }

}
