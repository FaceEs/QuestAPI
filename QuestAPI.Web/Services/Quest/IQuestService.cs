using QuestAPI.Core.Data.Models.Quest;
using QuestAPI.Core.Extentions.Enums;

namespace QuestAPI.Web.Services.Quest
{
    public interface IQuestService : IDisposable
    {
        Task<QuestViewModel> GetQuestById(Guid id);
        Task<List<QuestViewModel>> GetQuests(string? search, int offset = 0, int limit = 10, QuestTypeEnum? questType = null, Guid? playerId = null);
        Task<QuestViewModel> AddQuest(QuestAddModel model);
        Task<QuestViewModel> UpdateQuest(QuestUpdateModel model);
        void Dispose();
    }
}
