using QuestAPI.Core.Data.Models.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestAPI.Core.Data.Models.Player
{
    public class PlayerEntry : BaseEntry
    {
        public string Name { get; set; }
        public int CurrentExp { get; set; }
        public int Level { get; set; }
        public List<ItemEntry> Inventory { get; set; }
        public int Money { get; set; }
        public PlayerEntry()
        {

        }
        public PlayerEntry(PlayerAddModel addModel) {
            Name = addModel.Name;
            CurrentExp = addModel.CurrentExp;
            Level = addModel.Level;
            Inventory = new List<ItemEntry>();
            Money = addModel.Money;
        }

    }
}
