using AchievementsHelper.Helpers;
using AchievementsHelper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace AchievementsHelper.ViewModel
{
    public class AchievementsViewModel : INotifyPropertyChanged
    {
        private string _steamId;
        public string SteamId
        {
            get => _steamId;
            set
            {
                if (_steamId != value)
                {
                    _steamId = value;
                    OnPropertyChanged(nameof(SteamId));
                }
            }
        }

        public ObservableCollection<Game> Games { get; set; }

        private Game _selectedGame;
        public Game SelectedGame
        {
            get => _selectedGame;
            set
            {
                if (_selectedGame != value)
                {
                    _selectedGame = value;
                    OnPropertyChanged(nameof(SelectedGame));
                    LoadAchievementsForGame(_selectedGame);
                }
            }
        }

        private ObservableCollection<Achievement> _achievements = new();
        public ObservableCollection<Achievement> Achievements
        {
            get => _achievements;
            set
            {
                _achievements = value;
                OnPropertyChanged(nameof(Achievements));
            }
        }

        public ICommand ConfirmSelectionCommand { get; }

        public event Action<Game>? OnGameSelected;

        public AchievementsViewModel(string steamId)
        {
            SteamId = steamId;

            // Przykładowe dane — zastąp danymi z API
            Games = new ObservableCollection<Game>(GetFakeGamesForUser(steamId));

            ConfirmSelectionCommand = new RelayCommand(
                ExecuteGameSelection,
                () => SelectedGame != null);
        }

        private void ExecuteGameSelection()
        {
            if (SelectedGame != null)
                OnGameSelected?.Invoke(SelectedGame);
        }

        private void LoadAchievementsForGame(Game game)
        {
            if (game == null) return;

            // Przykładowe osiągnięcia — zastąp danymi z API
            Achievements = new ObservableCollection<Achievement>
            {
                new Achievement { Name = "Pierwsza krew", Description = "Zabij pierwszego przeciwnika", Unlocked = true },
                new Achievement { Name = "Weteran", Description = "Rozegraj 100 meczów", Unlocked = false }
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private List<Game> GetFakeGamesForUser(string steamId) => new()
        {
            new Game { AppId = "1", Name = "Counter-Strike: Global Offensive", IconUrl = "" },
            new Game { AppId = "2", Name = "Dota 2", IconUrl = "" },
            new Game { AppId = "3", Name = "Team Fortress 2", IconUrl = "" }
        };
    }
}
