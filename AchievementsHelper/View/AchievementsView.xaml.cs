using AchievementsHelper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for GameSelectionView.xaml
    /// </summary>
    public partial class AchievementsView : UserControl
    {
        public AchievementsView(string steamId)
        {
            InitializeComponent();
            DataContext = new AchievementsViewModel(steamId);
        }
    }
}
