using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;
using static WebApi.Models.ErrorMessages;

namespace WebApi.Controllers
{
    public class SendRequestController : Controller
    {
        int statusCode;
        Response response;

        /// <summary>
        /// Добавить в бд запрос клиента на обрутную связь. Возвращает результат в теле ответа
        /// </summary>
        /// <param name="json"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/{controller}")]
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] RequestedData<AddNewRequest> requestedData, [FromServices] DataContext context)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await context.Messages.AddAsync(new Message { Date = DateTime.Now, Text = requestedData.model.Text, Email = requestedData.model.Email, Status = "Получена" });

                    if (context.Users.FirstOrDefault(x => x.Email == requestedData.model.Email) == null)
                        await context.Users.AddAsync(new User { Email = requestedData.model.Email, Name = requestedData.model.Name });

                    await context.SaveChangesAsync();

                    statusCode = 200;

                    response = new Response { ok = true, status_code = statusCode, result = new {description = "Запрос успешно отправлен" } };

                }
                catch 
                {
                    statusCode = 500;

                    response = new Response { ok = false, status_code = statusCode, result = new { description = errorMessage[statusCode] } };
                }
            }
            else
            {
                statusCode = 400;

                response = new Response { ok = false, status_code = statusCode, result = new { description = errorMessage[statusCode] } };
            }

            return StatusCode(statusCode, response);
        }
    }
}
