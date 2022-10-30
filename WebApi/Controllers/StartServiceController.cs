using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Helpers;

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
