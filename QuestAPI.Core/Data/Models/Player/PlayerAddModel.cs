using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestAPI.Core.Data.Models.Player
{
    public class PlayerAddModel
    {
        public string Name { get; set; }
        public int CurrentExp { get; set; } = 0;
        public int Level { get; set; } = 1;
        public int Money { get; set; } = 0;
        public PlayerAddModel() { }
    }
}
