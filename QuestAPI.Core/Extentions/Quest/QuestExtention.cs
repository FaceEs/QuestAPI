using QuestAPI.Core.Data.Models.Quest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestAPI.Core.Extentions.Quest
{
    public static class QuestExtention
    {
        public static void Update(this QuestEntry quest, QuestUpdateModel questUpdate)
        {
            quest.Name = questUpdate.Name;
            quest.Description = questUpdate.Description;
            quest.ExperienceReward = questUpdate.ExperienceReward;
            quest.ConditionFinishCount = questUpdate.ConditionFinishCount;
            quest.MinimalLevel = questUpdate.MinimalLevel;
            quest.MoneyReward = questUpdate.MoneyReward;
            quest.Type = questUpdate.Type;
        }
        
        public static bool Validate(this QuestEntry quest)
        {
            bool valid = true;
            if(quest.Type == null)
            {
                valid = false;
            }
            if (string.IsNullOrEmpty(quest.Name))
            {
                valid = false;
            }
            if (string.IsNullOrEmpty(quest.Description))
            {
                valid = false;
            }
            if(quest.ConditionObjectId == null)
            {
                valid = false;
            }
            return valid;
        }
    }
}
