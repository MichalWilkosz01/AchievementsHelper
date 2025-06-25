using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AchievementsHelper.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        private HttpListener listener = new HttpListener();
        private const string ReturnUrl = "http://localhost:5000/";

        public event Action<string>? OnLoginSuccess;

        public LoginView()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (webView.CoreWebView2 == null)
                {
                    var env = await CoreWebView2Environment.CreateAsync(null, null);
                    await webView.EnsureCoreWebView2Async(env);

                    webView.CoreWebView2.Settings.UserAgent =
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/125.0.0.0 Safari/537.36";

                    webView.CoreWebView2.Settings.IsScriptEnabled = true;
                }

                if (!listener.Prefixes.Contains(ReturnUrl))
                    listener.Prefixes.Add(ReturnUrl);

                if (!listener.IsListening)
                    listener.Start();

                string steamOpenIdUrl = "https://steamcommunity.com/openid/login" +
                    "?openid.ns=http://specs.openid.net/auth/2.0" +
                    "&openid.mode=checkid_setup" +
                    "&openid.return_to=" + Uri.EscapeDataString(ReturnUrl) +
                    "&openid.realm=" + Uri.EscapeDataString(ReturnUrl) +
                    "&openid.identity=http://specs.openid.net/auth/2.0/identifier_select" +
                    "&openid.claimed_id=http://specs.openid.net/auth/2.0/identifier_select";

                webView.Source = new Uri(steamOpenIdUrl);
                webView.Visibility = Visibility.Visible;

                var context = await listener.GetContextAsync();
                listener.Stop();
                webView.Visibility = Visibility.Collapsed;

                string claimedId = context.Request.QueryString["openid.claimed_id"];
                string steamId = ExtractSteamID(claimedId);

                if (!string.IsNullOrEmpty(steamId))
                {
                    OnLoginSuccess?.Invoke(steamId);
                }
                else
                {
                    MessageBox.Show("Nie udało się pobrać SteamID", "Błąd");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message);
            }
        }

        private string? ExtractSteamID(string claimedId)
        {
            if (string.IsNullOrEmpty(claimedId)) return null;

            var parts = claimedId.Split('/');
            return parts.Length > 0 ? parts[^1] : null;
        }
    }
}
