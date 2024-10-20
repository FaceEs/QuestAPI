using QuestAPI.Core.Data.Models.PlayerQuest;

namespace QuestAPI.Web.Services.PlayerQuest
{
    public interface IPlayerQuestService
    {
        public Task<PlayerQuestViewModel> AcceptQuest(string playerId, string questId);
        public Task<PlayerQuestViewModel> CompleteQuest(string playerId, string questId);
        public Task<PlayerQuestViewModel> QuestProcessUpdate(string playerId, string questId, int conditionUpdateItem);
    }
}
