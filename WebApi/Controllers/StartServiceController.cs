using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class StartServiceController : Controller
    {
        [Route("/{controller}")]
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Сервис запущен");
        }
    }
}
