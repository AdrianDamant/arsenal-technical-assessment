using ArsenalTechnicalAssignment.Data.Data;
using ArsenalTechnicalAssignment.Data.Data.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ArsenalTechnicalAssignment.Data.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayersController : Controller
    {
        private readonly ILogger<PlayersController> _logger;
        private ISQLSyncService _sqlSyncService;
        public PlayersController(ILogger<PlayersController> logger,ISQLSyncService sqlSyncService)
        {
            _logger = logger;
            _sqlSyncService = sqlSyncService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlayerAsync(string playerName, Position position, int jerseyNumber, int goalsScored)
        {
            var result = await _sqlSyncService.CreatePlayerAsync(playerName,  position,  jerseyNumber,  goalsScored);
            return Json(new JsonResponse() { Result = result });
        }

        [HttpGet]
        public async Task<IActionResult> GetPlayersAsync()
        {
            var result = await _sqlSyncService.GetPlayersAsync();
            return Json(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePlayerAsync(Guid playerId,string playerName, Position position, int jerseyNumber, int goalsScored)
        {
            var result = await _sqlSyncService.UpdatePlayerAsync(playerId,playerName, position, jerseyNumber, goalsScored);
            return Json(new JsonResponse() { Result = result });
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePlayerAsync(Guid playerId)
        {
            var result = await _sqlSyncService.DeletePlayerAsync(playerId);
            return Json(new JsonResponse() { Result = result });
        }
    }
}
