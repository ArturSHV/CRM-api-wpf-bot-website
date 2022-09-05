using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers
{
    public class NotFoundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
