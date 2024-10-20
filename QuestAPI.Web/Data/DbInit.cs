using Microsoft.EntityFrameworkCore;
using QuestAPI.Core.Data.Models.Player;
using QuestAPI.Core.Data.Models.Quest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestAPI.Web.Data
{
    public class DbInit : IDbInit
    {
        private readonly QuestDbContext _context;
        public DbInit(QuestDbContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            _context.Database.Migrate();
            SeedData();
        }
        private async void SeedData()
        {
            if (!_context.Players.Any())
            {
                PlayerEntry player = new PlayerEntry()
                {
                    CurrentExp = 0,
                    Inventory = new List<Core.Data.Models.Item.ItemEntry>(),
                    Level = 1,
                    Money = 0,
                    Name = "TestPlayer"
                };
                _context.Players.Add(player);
                _context.SaveChanges();
            }
            if (!_context.Quests.Any())
            {
                List<QuestEntry> quests = new List<QuestEntry>()
                {
                    new QuestEntry()
                    {
                        PreviousQuest = null,
                        ConditionFinishCount = 100,
                        ConditionObjectId = new Guid("60dfcc16-d818-4a88-8575-270b602f0f43"),
                        Description = "Тестовое описание",
                        ExperienceReward = 50,
                        ItemRewards = null,
                        MinimalLevel = 1,
                        MoneyReward = 10,
                        Name = "Тестовое задание",
                        Type = Core.Extentions.Enums.QuestTypeEnum.SlayMonster
                    },
                    new QuestEntry()
                    {
                        PreviousQuest = null,
                        ConditionFinishCount = 100,
                        ConditionObjectId = new Guid("8fb65cd8-ed65-48e2-bc44-748b25a537f6"),
                        Description = "Тестовое описание",
                        ExperienceReward = 50,
                        ItemRewards = new List<Core.Data.Models.Item.ItemEntry>()
                        {
                            new Core.Data.Models.Item.ItemEntry()
                            {
                                Name = "Тестовый предмет",
                                Description = "Тестовое описание предмета"
                            }
                        },
                        MinimalLevel = 3,
                        MoneyReward = 10,
                        Name = "Тестовое задание",
                        Type = Core.Extentions.Enums.QuestTypeEnum.SlayMonster
                    }
                };
                quests.Add(
                    new QuestEntry()
                    {
                        PreviousQuest = quests[0],
                        ConditionFinishCount = 100,
                        ConditionObjectId = new Guid("c0d8c481-ddd7-4700-814f-c153a61d207c"),
                        Description = "Тестовое описание",
                        ExperienceReward = 50,
                        ItemRewards = null,
                        MinimalLevel = 1,
                        MoneyReward = 10,
                        Name = "Тестовое задание",
                        Type = Core.Extentions.Enums.QuestTypeEnum.SlayMonster
                    }
                );
                _context.AddRange(quests);
                _context.SaveChanges();
            }

        }
    }
}