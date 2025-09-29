using ArsenalTechnicalAssignment.Data.Data;
using Microsoft.AspNetCore.Mvc;

namespace ArsenalTechnicalAssignment.Portal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExternalAPIController : Controller
    {
        private ISQLSyncService _sqlSyncService;

        public ExternalAPIController(ISQLSyncService sqlSyncService)
        {
            _sqlSyncService = sqlSyncService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlayersAsync()
        {
            var result = await _sqlSyncService.GetPlayersAsync();
            return Json(result);
        }
    }
}
