using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;
using static WebApi.Models.ErrorMessages;

namespace WebApi.Controllers
{
    public class GetMessagesController : Controller
    {
        AccessVerification accessVerification = new AccessVerification();
        int statusCode;
        Response response;


        /// <summary>
        /// Получить список сообщений
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/{controller}")]
        [HttpPost]
        public IActionResult GetMessages([FromServices] DataContext context, [FromBody] RequestedData<EmptyModel> requestedData)
        {
            var ControllerName = ControllerContext.ActionDescriptor.ControllerName;

            var result = accessVerification.GetResultAccess(context, requestedData, ControllerName, "modify");

            if (result == true)
            {
                try
                {
                    var a = context.Messages.Join(context.Users,
                    message => message.Email,
                    user => user.Email,
                    (message, user) => new MessageStatus
                    {
                        Id = message.Id,
                        date = message.Date,
                        Name = user.Name,
                        Text = message.Text,
                        Contact = message.Email,
                        Status = message.Status

                    }
                    ).OrderBy(x => x.Id).ToList();

                    if (a.Count>0)
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
