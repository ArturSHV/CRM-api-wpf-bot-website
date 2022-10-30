using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;
using static WebApi.Models.ErrorMessages;

namespace WebApi.Controllers
{
    public class GetBlogsController : Controller
    {
        int statusCode;
        Response response;

        /// <summary>
        /// Получить список блогов. Возвращает результат в теле ответа
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/{controller}")]
        [HttpPost]
        public IActionResult Index([FromServices] DataContext context)
        {
            try
            {
                var a = context.Blogs.ToList();
                
                if (a.Count > 0)
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

        /// <summary>
        /// Вывод определенного блога
        /// </summary>
        /// <param name="id"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/{controller}/{id}")]
        [HttpGet]
        public IActionResult GetBlog(int id, [FromServices] DataContext context)
        {
            try
            {
                var a = context.Blogs.FirstOrDefault(x => x.Id == id);

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
