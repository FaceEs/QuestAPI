using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestAPI.Core.Data.Models.PlayerQuest;
using QuestAPI.Core.Data.Models.Quest;
using QuestAPI.Core.Extentions.Enums;
using QuestAPI.Web.Services.PlayerQuest;
using QuestAPI.Web.Services.Quest;
using System.Net;

namespace QuestAPI.Web.Controllers
{
    public class QuestController : Controller
    {
        private readonly IQuestService _questService;
        private readonly IPlayerQuestService _questPlayerService;
        public QuestController(IQuestService questService, IPlayerQuestService playerQuestService)
        {
            _questService = questService;
            _questPlayerService = playerQuestService;
        }
        /// <summary>
        /// Получение списка заданий по названию
        /// </summary>
        /// <param name="search">Название задания</param>
        /// <param name="offset">Смещение</param>
        /// <param name="limit">Лимит</param>
        /// <param name="type">Тип задания. Получение списка - api/quest/types</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<QuestViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("api/quest/quests")]
        public async Task<IActionResult> GetQuests(string? search, int offset = 0, int limit = 10, QuestTypeEnum? type = null, string? playerId = null)
        {
            Guid? playerIdGuid = null;
            if (!string.IsNullOrEmpty(playerId))
            {
                playerIdGuid = Guid.Parse(playerId);
            }
            try
            {
                var quests = await _questService.GetQuests(search, offset, limit, type, playerIdGuid);
                if (quests == null || quests.Count == 0)
                {
                    return NotFound();
                }
                return Ok(quests);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
        /// <summary>
        /// Принятие задания
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="questId"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PlayerQuestViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("api/quest/accept")]
        public async Task<IActionResult> AcceptQuest(string playerId, string questId)
        {
            if (string.IsNullOrEmpty(playerId))
            {
                return BadRequest("PlayerId должен иметь значение");
            }
            if (string.IsNullOrEmpty(questId))
            {
                return BadRequest("QuestId должке иметь значение");
            }
            try
            {
                var playerQuest = await _questPlayerService.AcceptQuest(playerId, questId);
                return Ok(playerQuest);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
        /// <summary>
        /// Завершение задания
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="questId"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PlayerQuestViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("api/quest/complete")]
        public async Task<IActionResult> CompleteQuest(string playerId, string questId)
        {
            if (string.IsNullOrEmpty(playerId))
            {
                return BadRequest("PlayerId должен иметь значение");
            }
            if (string.IsNullOrEmpty(questId))
            {
                return BadRequest("QuestId должке иметь значение");
            }
            try
            {
                var playerQuest = await _questPlayerService.CompleteQuest(playerId, questId);
                return Ok(playerQuest);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }

        }
        /// <summary>
        /// Обновить прогресс задания
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="questId"></param>
        /// <param name="conditionUpdateItem"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PlayerQuestViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("api/quest/process")]
        public async Task<IActionResult> QuestProcess(string playerId, string questId, int conditionUpdateItem)
        {
            if (string.IsNullOrEmpty(playerId))
            {
                return BadRequest("PlayerId должен иметь значение");
            }
            if (string.IsNullOrEmpty(questId))
            {
                return BadRequest("QuestId должке иметь значение");
            }
            if(conditionUpdateItem < 1)
            {
                return BadRequest("ConditionUpdateItem должен быть больше 0");
            }
            try
            {
                var playerQuest = await _questPlayerService.QuestProcessUpdate(playerId, questId, conditionUpdateItem);
                return Ok(playerQuest);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
