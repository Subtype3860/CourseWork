using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    public class UserApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
