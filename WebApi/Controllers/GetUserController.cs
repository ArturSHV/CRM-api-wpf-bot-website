using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;
using static WebApi.Models.ErrorMessages;
using static WebApi.Helpers.HashPasswordHelper;

namespace WebApi.Controllers
{

    public class GetUserController : Controller
    {
        AccessVerification accessVerification = new AccessVerification();
        int statusCode;
        Response response;


        /// <summary>
        /// Проверить пользователя
        /// </summary>
        /// <returns></returns>
        [Route("/api/{controller}")]
        [HttpPost]
        public IActionResult Index([FromServices] DataContext context, [FromBody] RequestedData<Account> requestedData)
        {
            var ControllerName = ControllerContext.ActionDescriptor.ControllerName;

            var result = accessVerification.GetResultAccess(context, requestedData, ControllerName, "access");

            if (result == true)
            {
                string password = "";

                var a = context.Accounts.FirstOrDefault(x => x.Login == requestedData.model.Login);

                if (a != null)
                {
                    password = HashPassword(requestedData.model.Password + a.Salt);
                }

                var c = context.Accounts.FirstOrDefault(x => (x.Login == requestedData.model.Login) && (x.Password == password));

                if (c != null)
                {
                    statusCode = 200;

                    response = new Response { ok = true, status_code = statusCode, result = new Account { IdRole = c.IdRole, Login = c.Login } };
                }
                else
                {
                    statusCode = 404;

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
