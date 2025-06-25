using AchievementsHelper.Helpers;
using System.Text.Json.Serialization;

namespace AchievementsHelper.Model
{
    public class Achievement
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonConverter(typeof(IntToBoolConverter))]
        [JsonPropertyName("achieved")]
        public bool Achieved { get; set; }
    }
}