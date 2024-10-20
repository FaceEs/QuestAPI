namespace QuestAPI.Core.Data.Models.Player
{
    public class PlayerViewModel
    {
        public string Name { get; set; }
        public int CurrentExp { get; set; }
        public int Level { get; set; }
        public PlayerViewModel(PlayerEntry player) {
            Name = player.Name;
            CurrentExp = player.CurrentExp;
            Level = player.Level;
        }
    }
}
