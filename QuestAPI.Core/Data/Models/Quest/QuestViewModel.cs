using QuestAPI.Core.Data.Models.Item;
using QuestAPI.Core.Extentions.Enums;

namespace QuestAPI.Core.Data.Models.Quest
{
    public class QuestViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        /// <summary>
        /// Количество монстров/локаций/предметов необходимых для завершения задания
        /// </summary>
        public int ConditionFinishCount { get; set; }
        public List<ItemViewModel>? ItemRewards { get; set; }
        public int ExperienceReward { get; set; }
        public int MoneyReward { get; set; }
        public int MinimalLevel { get; set; }
        /// <summary>
        /// Предыдущее задание в цепочке
        /// </summary>
        public string? PreviousQuestName { get; set; }
        public QuestViewModel(QuestEntry quest)
        {
            Name = quest.Name;
            Description = quest.Description;
            Type = Enum.GetName(typeof(QuestTypeEnum), quest.Type);
            ConditionFinishCount = quest.ConditionFinishCount;
            if (quest.ItemRewards != null)
            {
                ItemRewards = quest.ItemRewards == null ? null : new List<ItemViewModel>();
                foreach (var item in quest.ItemRewards)
                {
                    ItemRewards.Add(new ItemViewModel(item));
                }
            }
            ExperienceReward = quest.ExperienceReward;
            MoneyReward = quest.MoneyReward;
            MinimalLevel = quest.MinimalLevel;
            PreviousQuestName = quest.PreviousQuest == null ? null : quest.PreviousQuest.Name;

        }
    }
}
