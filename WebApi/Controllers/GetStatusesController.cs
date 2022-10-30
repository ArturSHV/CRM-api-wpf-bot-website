using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;
using static WebApi.Models.ErrorMessages;
using static WebApi.Helpers.HashPasswordHelper;

namespace WebApi.Controllers
{
    public class GetStatusesController : Controller
    {
        AccessVerification accessVerification = new AccessVerification();
        int statusCode;
        Response response;

        /// <summary>
        /// Получить список статусов
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/{controller}")]
        [HttpPost]
        public IActionResult Index([FromServices] DataContext context, [FromBody] RequestedData<EmptyModel> requestedData)
        {
            var ControllerName = ControllerContext.ActionDescriptor.ControllerName;

            var result = accessVerification.GetResultAccess(context, requestedData, ControllerName, "access");

            if (result == true)
            {
                try
                {
                    var a = context.Status.ToList();

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
            }
            else
            {
                statusCode = 403;

                response = new Response { ok = false, status_code = statusCode, result = new { description = errorMessage[statusCode] } };

            }


            return StatusCode(statusCode, response);
        }
    }
}
