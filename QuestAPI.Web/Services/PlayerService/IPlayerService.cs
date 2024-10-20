using QuestAPI.Core.Data.Models.Player;
using QuestAPI.Core.Extentions.Enums;

namespace QuestAPI.Web.Services.PlayerService
{
    public interface IPlayerService
    {
        public Task<List<PlayerViewModel>> GetPlayers(string? search, int offset = 0, int limit = 10);
        public Task<PlayerViewModel> GetPlayerById(string playerId);
    }
}
