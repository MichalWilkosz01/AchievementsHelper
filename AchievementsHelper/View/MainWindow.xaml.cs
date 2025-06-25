using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var loginView = new LoginView();
            loginView.OnLoginSuccess += LoginView_OnLoginSuccess;

            MainContent.Content = loginView;
        }

        private void LoginView_OnLoginSuccess(string steamId)
        {
            // Po zalogowaniu załaduj widok wyboru gry
            var gameSelectionView = new AchievementsView(steamId); // zakładamy, że taki konstruktor istnieje
            MainContent.Content = gameSelectionView;
        }
    }
}