using System.Text.Json.Serialization;

namespace AchievementsHelper.Model.JSON
{
    public class SteamGameList
    {
        [JsonPropertyName("game_count")]
        public int GameCount { get; set; }

        [JsonPropertyName("games")]
        public List<Game> Games { get; set; }
    }
}