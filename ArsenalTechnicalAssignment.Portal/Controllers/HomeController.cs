using ArsenalTechnicalAssignment.Data.Data;
using ArsenalTechnicalAssignment.Portal.Extensions;
using ArsenalTechnicalAssignment.Portal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ArsenalTechnicalAssignment.Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ISQLSyncService _sqlSyncService;
        public HomeController(ILogger<HomeController> logger, ISQLSyncService sqlSyncService)
        {
            _logger = logger;
            _sqlSyncService = sqlSyncService;
        }

        public IActionResult ErrorStartup()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var model = new PlayersTableModel() { Players = await _sqlSyncService.GetPlayersAsync() };
            return View(model);
        }

        public async Task<IActionResult> FilterbyGoalsScored()
        {
            var played = await _sqlSyncService.GetPlayersAsync();
            played = played.OrderByDescending(__ => __.GoalsScored).ToList();
            var model = new PlayersTableModel() { Players = played };
            return View(model);
        }

        public async Task<IActionResult> CreateUpdatePlayer(CreatePlayerModel model)
        {
            var player = await _sqlSyncService.GetPlayerAsync(model.PlayerId);
            if (player is null)
            {
                //Make sure the player jersey number is unique!
                var jerseyNumber = await _sqlSyncService.GetPlayersByJerseyNumberAsync(model.JerseyNumber);
                if(jerseyNumber is null)
                {
                    await _sqlSyncService.CreatePlayerAsync(model.PlayerName, model.Position, model.JerseyNumber, model.GoalsScored);
                }
                else
                {
                    //TODO: Return an Error, jersey number is not unique
                }
            }
            else await _sqlSyncService.UpdatePlayerAsync(model.PlayerId, model.PlayerName, model.Position, model.JerseyNumber, model.GoalsScored);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> EditPlayer(Guid id)
        {
            var model = await _sqlSyncService.GetPlayerAsync(id);
            if (model is null) return View();//TODO return a neat error message
            return PartialView("_CreatePlayerModal", model.ToCreatePlayerModel());
        }

        public async Task<IActionResult> DeletePlayerConfirmation(Guid id)
        {
            var model = await _sqlSyncService.GetPlayerAsync(id);
            if (model is null) return View();//TODO return a neat error message
            return PartialView("_DeletePlayerModal", model.ToCreatePlayerModel());
        }

        public async Task<IActionResult> DeletePlayer(CreatePlayerModel model)
        {
            var deleteResult = await _sqlSyncService.DeletePlayerAsync(model.PlayerId);
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
