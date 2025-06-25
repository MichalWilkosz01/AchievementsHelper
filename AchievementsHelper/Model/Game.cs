using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AchievementsHelper.Model
{
    public class Game
    {
        [JsonPropertyName("appid")]
        public int AppId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("playtime_forever")]
        public int PlaytimeForever { get; set; }
        public List<Achievement> Achievements { get; set; }
    }
}
