using QuestAPI.Core.Data.Models.Player;
using QuestAPI.Core.Data.Models.Quest;
using QuestAPI.Core.Extentions.Enums;

namespace QuestAPI.Core.Data.Models.PlayerQuest
{
    public class PlayerQuestEntry : BaseEntry
    {
        public PlayerEntry Player { get; set; }
        public QuestEntry Quest { get; set; }
        public QuestStatusEnum Status { get; set; }
        /// <summary>
        /// Количество собранных предметов/убитых монстров/посещенных локаций
        /// </summary>
        public int ConditionCount { get; set; } = 0;
        public PlayerQuestEntry() { }
    }
}
