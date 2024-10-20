using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestAPI.Core.Data.Models.Player
{
    public class PlayerUpdateModel
    {
        public string? Name { get; set; }
        public int? CurrentExp { get; set; }
        public int? Level { get; set; }
        public PlayerUpdateModel() { }
    }
}
