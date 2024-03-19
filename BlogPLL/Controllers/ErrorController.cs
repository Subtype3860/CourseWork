using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BlogPLL.Controllers;

public class ErrorController : Controller
{
    [Route("Error/{statusCode}")]
    [AllowAnonymous]
    public IActionResult HttpStatusCodeHandler(int statusCode)
    {
        var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
        if(statusCodeResult != null)
            ViewBag.Path = statusCodeResult.OriginalQueryString!;
        return View(statusCode);
    }




}