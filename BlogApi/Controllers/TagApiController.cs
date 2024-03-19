using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    public class TagApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
