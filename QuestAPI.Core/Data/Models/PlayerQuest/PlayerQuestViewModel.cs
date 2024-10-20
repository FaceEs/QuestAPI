using QuestAPI.Core.Data.Models.Player;
using QuestAPI.Core.Data.Models.Quest;
using QuestAPI.Core.Extentions.Enums;

namespace QuestAPI.Core.Data.Models.PlayerQuest
{
    public class PlayerQuestViewModel
    {
        public PlayerViewModel Player { get; set; }
        public QuestViewModel Quest { get; set; }
        public string Status { get; set; }
        /// <summary>
        /// Количество собранных предметов/убитых монстров/посещенных локаций
        /// </summary>
        public int ConditionCount { get; set; } = 0;
        public PlayerQuestViewModel(PlayerQuestEntry playerQuest) {
            Player = new PlayerViewModel(playerQuest.Player);
            Quest = new QuestViewModel(playerQuest.Quest);
            Status = Enum.GetName(typeof(QuestStatusEnum), playerQuest.Status);
            ConditionCount = playerQuest.ConditionCount;
        }
    }
}
