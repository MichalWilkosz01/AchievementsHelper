using AchievementsHelper.Helpers;
using AchievementsHelper.Model;
using AchievementsHelper.Model.JSON;
using AchievementsHelper.Model.JSON.Achievements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Input;

namespace AchievementsHelper.ViewModel
{
    public class AchievementsViewModel : INotifyPropertyChanged
    {
        private string _steamId;
        private readonly string _getOwnedGamesLink = @"http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?format=json";
        private readonly string _getAchievementsLink = @"http://api.steampowered.com/ISteamUserStats/GetPlayerAchievements/v0001/?format=json";
        private readonly string _getAchievementsLink2 = @"http://api.steampowered.com/ISteamUserStats/GetUserStatsForGame/v0002/?format=json&l=english";
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
            Games = new ObservableCollection<Game>(GetGamesForUser(steamId));

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

            Achievements = GetAchievementsForGame(game.AppId.ToString());
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private List<Game> GetGamesForUser(string steamId) 
        {
            Dictionary<string, string> adds = new Dictionary<string, string>()
            {
                { "include_appinfo", "1" }
            };
            string url = GetValidUrl(_getOwnedGamesLink, adds);
            string responseJson = Get(url);
            try
            {
                var result = JsonSerializer.Deserialize<SteamGamesResponse>(responseJson);
                return result?.Response?.Games ?? new List<Game>();
            }
            catch (JsonException ex)
            {
                throw new Exception("Failed to parse Steam API response.", ex);
            }
        }

        private ObservableCollection<Achievement> GetAchievementsForGame(string appId)
        {
            Dictionary<string, string> adds = new Dictionary<string, string>()
            {
                { "appid", appId },
                {"l", "english" }
            };
            string url = GetValidUrl(_getAchievementsLink, adds);
            string responseJson = Get(url);
            try
            {
                var result = JsonSerializer.Deserialize<SteamAchievementResponse>(responseJson);
                var achievements = result?.PlayerStats?.Achievements ?? new List<Achievement>();
                return new ObservableCollection<Achievement>(achievements);
            }
            catch (JsonException ex)
            {
                throw new Exception("Failed to parse Steam API response.", ex);
            }
        }

        private string GetValidUrl(string url, Dictionary<string, string>? additionalParameters = null)
        {
            var uriBuilder = new UriBuilder(url);
            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
            query["key"] = App.Secrets.ApiKey;
            query["steamid"] = _steamId;
            if (additionalParameters != null)
            {
                foreach (var kvp in additionalParameters)
                {
                    query[kvp.Key] = kvp.Value;
                }
            }
            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }

        private string Get(string url)
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = httpClient.GetAsync(url).Result;
                response.EnsureSuccessStatusCode(); 
                return response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                throw new Exception($"GET request failed for URL: {ex.Message}", ex);
            }
        }
    }
}
