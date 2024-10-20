using QuestAPI.Core.Data.Models.Player;

namespace QuestAPI.Core.Extentions.Player
{
    public static class PlayerExtention
    {
        public static void Update(this PlayerEntry player, PlayerUpdateModel updateModel)
        {
            player.Name = updateModel.Name != null ? updateModel.Name : player.Name;
            player.CurrentExp = updateModel.CurrentExp != null ? updateModel.CurrentExp.GetValueOrDefault() : player.CurrentExp;
            player.Level = updateModel.Level != null ? updateModel.Level.GetValueOrDefault() : player.Level;
        }
    }
}
