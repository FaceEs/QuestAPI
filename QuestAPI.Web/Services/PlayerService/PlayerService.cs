using Microsoft.EntityFrameworkCore;
using QuestAPI.Core.Data.Models.Player;
using QuestAPI.Core.Exceptions;
using QuestAPI.Web.Data;

namespace QuestAPI.Web.Services.PlayerService
{
    public class PlayerService : IPlayerService
    {
        private readonly QuestDbContext _context;
        public PlayerService(QuestDbContext context) {
            _context = context;
        }
        public async Task<PlayerViewModel> GetPlayerById(string playerId)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Id.ToString() == playerId.ToLower());
            if(player == null)
            {
                throw new EntityNotFoundException($"Player с Id {playerId} не найден");
            }
            return new PlayerViewModel(player);
        }

        public async Task<List<PlayerViewModel>> GetPlayers(string? search, int offset = 0, int limit = 10)
        {
            var players = await _context.Players
                                .Include(q => q.Inventory)
                                .Where(q => (search == null || q.Name.ToLower().Contains(search.ToLower())))
                                .OrderBy(q => q.Id)
                                .Skip(offset)
                                .Take(limit).ToListAsync();
            if(players == null || players.Count == 0)
            {
                return null;
            }
            List<PlayerViewModel> playersVM = new List<PlayerViewModel>();
            foreach (var player in players)
            {
                playersVM.Add(new PlayerViewModel(player));
            }
            return playersVM;
        }
    }
}
