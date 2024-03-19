using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    public class RolesApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
