using ArsenalTechnicalAssignment.Data.Dtos;
using ArsenalTechnicalAssignment.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ArsenalTechnicalAssignment.Portal.Controllers
{
    public class IntegrationController : Controller
    {
        private ExternalIntegrationService _externalIntegrationService;
        public IntegrationController(ExternalIntegrationService externalIntegrationService)
        {
            _externalIntegrationService = externalIntegrationService;
        }
        
        public async Task<IActionResult> Fixtures()
        {
            //Get the data from football-data.org
            var arsenalSeasonMatches = await _externalIntegrationService.ExecuteGET("teams/57/matches?season=2025");

            MatchesDto? matches = JsonSerializer.Deserialize<MatchesDto?>(arsenalSeasonMatches, _externalIntegrationService._serializerOptions);

            if (matches is null || matches.Matches is null) return View();//TODO add error handling here

            //Show only matches not yet finished (Fixtures)
            matches.Matches = matches.Matches.Where(__ => __.Status != "FINISHED").OrderBy(__ => __.UtcDate).ToList();

            return View(matches);
        }

        public async Task<IActionResult> Results()
        {
            //Get the data from football-data.org
            var arsenalSeasonMatches = await _externalIntegrationService.ExecuteGET("teams/57/matches?season=2025");

            MatchesDto? matches = JsonSerializer.Deserialize<MatchesDto?>(arsenalSeasonMatches, _externalIntegrationService._serializerOptions);

            if (matches is null || matches.Matches is null) return View();//TODO add error handling here

            //Show only matches not yet finished (Fixtures)
            matches.Matches = matches.Matches.Where(__ => __.Status == "FINISHED").OrderBy(__ => __.UtcDate).ToList();

            return View(matches);
        }
    }
}
