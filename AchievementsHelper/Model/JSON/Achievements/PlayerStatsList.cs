using System.Text.Json.Serialization;

namespace AchievementsHelper.Model.JSON.Achievements
{
    public class PlayerStatsList
    {
        [JsonPropertyName("steamID")]
        public string SteamID { get; set; }
        [JsonPropertyName("gameName")]
        public string GameName { get; set; }
        [JsonPropertyName("achievements")]
        public List<Achievement> Achievements { get; set; }
    }
}