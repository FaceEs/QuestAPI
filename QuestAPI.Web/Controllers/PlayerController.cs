using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestAPI.Core.Exceptions;
using QuestAPI.Web.Services.PlayerService;

namespace QuestAPI.Web.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;
        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        /// <summary>
        /// Получить список игроков
        /// </summary>
        /// <param name="search"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/player/players")]
        public async Task<IActionResult> GetPlayers(string? search, int offset = 0, int limit = 10)
        {
            try
            {
                var players = await _playerService.GetPlayers(search, offset, limit);
                if(players == null)
                {
                    return NotFound();
                }
                return Ok(players);
            }catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Получить пользователя по id
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/player/{playerId}")]
        public async Task<IActionResult> GetPlayerById(string playerId)
        {
            var player = _playerService.GetPlayerById(playerId);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }
    }
}
