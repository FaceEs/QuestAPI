using QuestAPI.Core.Data.Models.Item;
using QuestAPI.Core.Extentions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestAPI.Core.Data.Models.Quest
{
    public class QuestUpdateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public QuestTypeEnum Type { get; set; }
        /// <summary>
        /// Количество монстров/локаций/предметов необходимых для завершения задания
        /// </summary>
        public int ConditionFinishCount { get; set; }
        public List<Guid>? ItemRewardsIds { get; set; }
        public int ExperienceReward { get; set; } = 0;
        public int MoneyReward { get; set; } = 0;
        public int MinimalLevel { get; set; } = 0;
        /// <summary>
        /// Предыдущее задание в цепочке
        /// </summary>
        public Guid? PreviousQuestIds { get; set; }
        public QuestUpdateModel() { }
    }
}
