using QuestAPI.Core.Data.Models.Quest;
using QuestAPI.Core.Data.Models.Item;
using QuestAPI.Web.Data;
using Microsoft.EntityFrameworkCore;
using QuestAPI.Core.Extentions.Quest;
using QuestAPI.Core.Exceptions;
using QuestAPI.Core.Extentions.Enums;

namespace QuestAPI.Web.Services.Quest
{
    public class QuestService : IQuestService
    {
        private readonly QuestDbContext _context;
        public QuestService(QuestDbContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<QuestViewModel> AddQuest(QuestAddModel model)
        {
            QuestEntry entry = new QuestEntry(model);
            entry.ItemRewards = new List<ItemEntry>();
            foreach (var itemId in model.ItemRewardsIds)
            {
                var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == itemId);
                if (item == null)
                {
                    throw new EntityNotFoundException($"Предмет с Id {itemId} не найден. Задание не добавлено");
                }
            }
            var previewQuest = await _context.Quests.FirstOrDefaultAsync(q => q.Id == model.PreviousQuestId);
            if (previewQuest == null)
            {
                throw new EntityNotFoundException($"Задание с Id {model.PreviousQuestId} не найдено. Задание не добавлено");
            }
            if (!entry.Validate())
            {
                throw new WrongModelException($"Ошибка добавления задания. Название задания - {model.Name}");
            }
            else
            {
                await _context.Quests.AddAsync(entry);
                await _context.SaveChangesAsync();
            }
            return new QuestViewModel(entry);

        }

        public async Task<QuestViewModel> GetQuestById(Guid id)
        {
            if (id == null)
            {
                throw new WrongModelException("Ошибка при поиске задания. Id было равно null");
            }
            var quest = await _context.Quests.Include(q => q.PreviousQuest).Include(q => q.ItemRewards).FirstOrDefaultAsync(q => q.Id == id);
            if (quest == null)
            {
                throw new EntityNotFoundException($"Задание с Id {id.ToString()} не найдено.");
            }
            return new QuestViewModel(quest);
        }

        public async Task<List<QuestViewModel>> GetQuests(string? search, int offset = 0, int limit = 10, QuestTypeEnum? questType = null, Guid? playerId = null)
        {
            var quests = await _context.Quests
                                .Include(q => q.PreviousQuest)
                                .Include(q => q.ItemRewards)
                                .Where(q => (search == null || q.Name.ToLower().Contains(search.ToLower()))
                                      && (questType == null || q.Type == questType))
                                .OrderBy(q => q.Id)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
            if (playerId != null)
            {
                var player = await _context.Players.FirstOrDefaultAsync(q => q.Id == playerId);
                if (player == null)
                {
                    throw new EntityNotFoundException($"Не удалось получить задания. Player с ID {playerId} не найден");
                }
                var playerQuests = await _context.PlayerQuests.Include(pq => pq.Quest).Where(pq => pq.Player.Id == player.Id).ToListAsync();
                quests = quests.Where(q => (q.MinimalLevel <= player.Level) &&
                                            !playerQuests.Any(qp => qp.Quest.Id == q.Id) &&
                                            (q.PreviousQuest == null || playerQuests.Any(pq => pq.Quest.Id == q.PreviousQuest.Id && pq.Status == QuestStatusEnum.Finished))).ToList();
            }
            List<QuestViewModel> questsViewModels = new List<QuestViewModel>();
            foreach (var item in quests)
            {
                questsViewModels.Add(new QuestViewModel(item));
            }
            return questsViewModels;
        }

        public async Task<QuestViewModel> UpdateQuest(QuestUpdateModel model)
        {
            if (model.Id == null)
            {
                throw new WrongModelException("Не удалось обновить задание. Поле ID было пустым");
            }
            var quest = await _context.Quests.Include(q => q.ItemRewards).Include(q => q.PreviousQuest).FirstOrDefaultAsync(q => q.Id == model.Id);
            if (quest == null)
            {
                throw new EntityNotFoundException($"Не удалось найти задание с ID {model.Id}. Задание не было обновлено");
            }
            quest.Update(model);
            _context.Quests.Update(quest);
            await _context.SaveChangesAsync();
            return new QuestViewModel(quest);
        }
    }
}
