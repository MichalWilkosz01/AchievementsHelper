using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchievementsHelper.Model
{
    public class Game
    {
        public string AppId { get; set; }        // ID gry w Steam (np. "570" dla Dota 2)
        public string Name { get; set; }         // Nazwa gry
        public string IconUrl { get; set; }      // URL do ikonki gry (np. do wyświetlenia w ComboBox)
        public List<Achievement> Achievements { get; set; }
    }
}
