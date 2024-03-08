using Microsoft.AspNetCore.Mvc;

namespace BlogPLL.Controllers;

public class ErrorController : Controller
{
    // GET
    public IActionResult Forbidden()
    {
        return View();
    }

    public IActionResult Resource()
    {
        return View();
    }

    public IActionResult GoWrong()
    {
        return View();
    }
    
    
    
}