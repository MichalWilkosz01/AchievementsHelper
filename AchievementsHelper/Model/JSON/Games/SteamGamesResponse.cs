using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AchievementsHelper.Model.JSON
{
    public class SteamGamesResponse
    {
        [JsonPropertyName("response")]
        public SteamGameList Response { get; set; }
    }
}
