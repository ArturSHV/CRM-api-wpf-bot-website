using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers
{
    public class ErrorController : Controller
    {
        [Route("{controller}/{message}/{code}")]
        [HttpGet]
        public IActionResult Index(string message, int code)
        {
            ViewData["Message"] = message;
            ViewData["Code"] = code;
            return View();
        }
    }
}
