using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;
using static WebApi.Models.ErrorMessages;

namespace WebApi.Controllers
{
    public class GetContactsController : Controller
    {
        int statusCode;
        Response response;


        /// <summary>
        /// Получить список контактов. Возвращает результат в теле ответа
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/{controller}")]
        [HttpPost]
        public IActionResult GetContacts([FromServices] DataContext context)
        {
            try
            {
                var a = context.Contacts.FirstOrDefault();

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
