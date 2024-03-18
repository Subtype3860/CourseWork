using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BlogPLL.Controllers;

public class ErrorController : Controller
{
    [Route("Error/{statusCode}")]
    public IActionResult HttpStatusCodeHandler(int statusCode)
    {
        var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
        switch(statusCode)
        {
            case 404:
                ViewBag.Path = statusCodeResult!.OriginalPath;
                return RedirectToAction("ErrorNotPage");
        }
        return View("");
    }

    public IActionResult ErrorNotPage() => View();




}