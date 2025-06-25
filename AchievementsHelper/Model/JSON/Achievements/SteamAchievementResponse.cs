using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AchievementsHelper.Model.JSON.Achievements
{
    public class SteamAchievementResponse
    {
        [JsonPropertyName("playerstats")]
        public PlayerStatsList PlayerStats { get; set; }
    }
}
