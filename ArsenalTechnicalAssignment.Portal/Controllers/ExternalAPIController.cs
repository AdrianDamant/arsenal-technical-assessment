using Microsoft.AspNetCore.Mvc;

namespace ArsenalTechnicalAssignment.Portal.Controllers
{
    public class ExternalAPIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
