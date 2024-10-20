using Microsoft.EntityFrameworkCore;
using QuestAPI.Core.Data.Models.PlayerQuest;
using QuestAPI.Core.Exceptions;
using QuestAPI.Core.Extentions.Enums;
using QuestAPI.Core.Extentions.Experience;
using QuestAPI.Web.Data;

namespace QuestAPI.Web.Services.PlayerQuest
{
    public class PlayerQuestService : IPlayerQuestService
    {
        private readonly QuestDbContext _context;
        public PlayerQuestService(QuestDbContext context)
        {
            _context = context;
        }
        public async Task<PlayerQuestViewModel> AcceptQuest(string playerId, string questId)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Id.ToString() == playerId.ToLower());
            if (player == null)
            {
                throw new EntityNotFoundException($"Player с Id {playerId} не найден");
            }
            var quest = await _context.Quests.Include(q => q.PreviousQuest).FirstOrDefaultAsync(q => q.Id.ToString() == questId.ToLower());
            if (quest == null)
            {
                throw new EntityNotFoundException($"Задание с Id {questId} не найден");
            }
            if (_context.PlayerQuests.Any(pq => pq.Player.Id.ToString() == playerId.ToLower() &&
                                               pq.Quest.Id.ToString() == questId.ToLower()))
            {
                throw new QuestException($"Player с Id {playerId} уже выполняет или выполнил задание с Id {questId}");
            }
            if (quest.PreviousQuest != null)
            {
                if(!_context.PlayerQuests.Any(pq => pq.Quest.Id == quest.PreviousQuest.Id))
                {
                    throw new QuestException($"Не выполнено предыдущее задание с Id {quest.PreviousQuest.Id}");
                }
            }
            var questPlayer = new PlayerQuestEntry();
            questPlayer.Player = player;
            questPlayer.Quest = quest;
            questPlayer.Status = QuestStatusEnum.Accepted;
            questPlayer.ConditionCount = 0;
            await _context.PlayerQuests.AddAsync(questPlayer);
            await _context.SaveChangesAsync();
            return new PlayerQuestViewModel(questPlayer);
        }

        public async Task<PlayerQuestViewModel> CompleteQuest(string playerId, string questId)
        {
            var player = await _context.Players.Include(p => p.Inventory).FirstOrDefaultAsync(p => p.Id.ToString() == playerId.ToLower());
            if (player == null)
            {
                throw new EntityNotFoundException($"Player с Id {playerId} не найден");
            }
            var quest = await _context.PlayerQuests.FirstOrDefaultAsync(q => q.Quest.Id.ToString() == questId.ToLower());
            if (quest == null)
            {
                throw new EntityNotFoundException($"Задание игрока с Id {questId} не найден");
            }
            if (_context.PlayerQuests.Any(pq => pq.Player.Id.ToString() == playerId.ToLower() &&
                                               pq.Quest.Id.ToString() == questId.ToLower() &&
                                               pq.Status == QuestStatusEnum.Finished))
            {
                throw new QuestException($"Player с Id {playerId} уже выполнил задание с Id {questId}");
            }
            var playerQuest = await _context.PlayerQuests.Include(pq => pq.Quest).ThenInclude(q => q.ItemRewards).FirstOrDefaultAsync(pq => pq.Quest.Id.ToString() == questId.ToLower() && pq.Player.Id.ToString() == playerId.ToLower());
            if (playerQuest.Status == QuestStatusEnum.Completed)
            {
                playerQuest.Status = QuestStatusEnum.Finished;
            }else
            {
                throw new QuestException($"Задание ещё не выполнено. Не выполнены условия");
            }
            _context.Update(playerQuest);
            if (playerQuest.Quest.ItemRewards != null && playerQuest.Quest.ItemRewards.Count != 0)
            {
                foreach (var itemReward in playerQuest.Quest.ItemRewards)
                {
                    var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == itemReward.Id);
                    player.Inventory.Add(item);
                }
            }
            player.CurrentExp += playerQuest.Quest.ExperienceReward;
            bool isLevelUp = false;
            do
            {
                switch (player.Level)
                {
                    case 1:
                        if (player.CurrentExp > ExpTable.Level2)
                        {
                            isLevelUp = true;
                            player.Level = 2;
                            player.CurrentExp -= ExpTable.Level2;
                        }else
                        {
                            isLevelUp = false;
                        }
                        break;
                    case 2:
                        if (player.CurrentExp > ExpTable.Level3)
                        {
                            isLevelUp = true;
                            player.Level = 3;
                            player.CurrentExp -= ExpTable.Level3;
                        }
                        else
                        {
                            isLevelUp = false;
                        }
                        break;
                    case 3:
                        if (player.CurrentExp > ExpTable.Level4)
                        {
                            isLevelUp = true;
                            player.Level = 4;
                            player.CurrentExp -= ExpTable.Level4;
                        }
                        else
                        {
                            isLevelUp = false;
                        }
                        break;
                    case 4:
                        if (player.CurrentExp > ExpTable.Level5)
                        {
                            isLevelUp = true;
                            player.Level = 5;
                            player.CurrentExp -= ExpTable.Level5;
                        }
                        else
                        {
                            isLevelUp = false;
                        }
                        break;
                    case 5:
                        isLevelUp = false;
                        break;
                    default:
                        throw new QuestException("Неверный уровень игрка");
                }
            } while (isLevelUp);
            player.Money += playerQuest.Quest.MoneyReward;
            await _context.SaveChangesAsync();
            return new PlayerQuestViewModel(playerQuest);
        }

        public async Task<PlayerQuestViewModel> QuestProcessUpdate(string playerId, string questId, int conditionUpdateItem)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Id.ToString() == playerId.ToLower());
            if (player == null)
            {
                throw new EntityNotFoundException($"Player с Id {playerId} не найден");
            }
            var quest = await _context.PlayerQuests.FirstOrDefaultAsync(q => q.Quest.Id.ToString() == questId.ToLower());
            if (quest == null)
            {
                throw new EntityNotFoundException($"Задание игрока с Id {questId} не найден");
            }
            if (_context.PlayerQuests.Any(pq => pq.Player.Id.ToString() == playerId.ToLower() &&
                                               pq.Quest.Id.ToString() == questId.ToLower() &&
                                               pq.Status == QuestStatusEnum.Finished))
            {
                throw new QuestException($"Player с Id {playerId} уже выполнил задание с Id {questId}");
            }
            var playerQuest = await _context.PlayerQuests.Include(pq => pq.Quest).FirstOrDefaultAsync(pq => pq.Quest.Id.ToString() == questId.ToLower() && pq.Player.Id.ToString() == playerId.ToLower());
            if (playerQuest.ConditionCount + conditionUpdateItem > playerQuest.Quest.ConditionFinishCount)
            {
                throw new QuestException($"Прогресс не может превышать требуемое значение {playerQuest.Quest.ConditionFinishCount}");
            }
            else
            {
                playerQuest.ConditionCount += conditionUpdateItem;
            }
            if (playerQuest.ConditionCount == playerQuest.Quest.ConditionFinishCount)
            {
                playerQuest.Status = QuestStatusEnum.Completed;
            }
            if (playerQuest.Status == QuestStatusEnum.Accepted)
            {
                playerQuest.Status = QuestStatusEnum.InProgress;
            }
            _context.PlayerQuests.Update(playerQuest);
            await _context.SaveChangesAsync();
            return new PlayerQuestViewModel(playerQuest);
        }
    }
}
