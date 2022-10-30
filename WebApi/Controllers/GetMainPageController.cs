using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Helpers;
using WebApi.Models;
using static WebApi.Models.ErrorMessages;

namespace WebApi.Controllers
{
    public class GetMainPageController : Controller
    {
        int statusCode;
        Response response;

        /// <summary>
        /// Получить информацию о главной странице
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/{controller}")]
        [HttpPost]
        public IActionResult Index([FromServices] DataContext context)
        {
            try
            {
                var a = context.MainPageModels.FirstOrDefault();

                if (a != null)
                {
                    statusCode = 200;

                    response = new Response() { ok = true, status_code = statusCode, result = new { items = a } };
                }
                else
                {
                    statusCode = 404;

                    response = new Response() { ok = false, status_code = statusCode, result = new { description = errorMessage[statusCode] } };
                }
            }
            catch
            {
                statusCode = 500;

                response = new Response { ok = false, status_code = statusCode, result = new { description = errorMessage[statusCode] } };

            }
            
            return StatusCode(statusCode, response);
        }

    }
}
