using QuestAPI.Core.Data.Models.Item;
using QuestAPI.Core.Extentions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestAPI.Core.Data.Models.Quest
{
    public class QuestEntry : BaseEntry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public QuestTypeEnum Type { get; set; }
        /// <summary>
        /// Количество монстров/локаций/предметов необходимых для завершения задания
        /// </summary>
        public int ConditionFinishCount { get; set; }
        /// <summary>
        /// Id необходимого монстра/локации/предмета для задания
        /// </summary>
        public Guid ConditionObjectId { get; set; }
        public List<ItemEntry>? ItemRewards { get; set; }
        public int ExperienceReward { get; set; } = 0;
        public int MoneyReward { get; set; } = 0;
        public int MinimalLevel { get; set; } = 0;
        /// <summary>
        /// Предыдущее задание в цепочке
        /// </summary>
        public QuestEntry? PreviousQuest { get; set; }
        public QuestEntry() { }
        public QuestEntry(QuestAddModel model)
        {
            Name = model.Name;
            Description = model.Description;
            Type = model.Type;
            ConditionFinishCount = model.ConditionFinishCount;
            ExperienceReward = model.ExperienceReward;
            MoneyReward = model.MoneyReward;
            MinimalLevel = model.MinimalLevel;
        }
    }
}
