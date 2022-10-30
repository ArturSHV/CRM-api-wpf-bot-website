using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;
using static WebApi.Models.ErrorMessages;

namespace WebApi.Controllers
{
    public class EditMainPageController : Controller
    {
        AccessVerification accessVerification = new AccessVerification();
        int statusCode;
        Response response;


        /// <summary>
        /// Изменить главную страницу
        /// </summary>
        /// <param name="json"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/{controller}")]
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] RequestedData<MainPageModel> requestedData, [FromServices] DataContext context)
        {
            if (ModelState.IsValid)
            {
                var ControllerName = ControllerContext.ActionDescriptor.ControllerName;

                var result = accessVerification.GetResultAccess(context, requestedData, ControllerName, "modify");

                if (result == true)
                {
                    try
                    {
                        MainPageModel? a = context.MainPageModels.FirstOrDefault();

                        if (a != null)
                        {
                            a.Title = requestedData.model.Title;
                            a.Placeholder = requestedData.model.Placeholder;
                            a.ButtonText = requestedData.model.ButtonText;

                            await context.SaveChangesAsync();

                            statusCode = 200;

                            response = new Response { ok = true, status_code = statusCode, result = new { description = "Главная страница успешно отредактирована" } };
                        }
                        else
                        {
                            statusCode = 404;

                            response = new Response { ok = false, status_code = statusCode, result = new { description = errorMessage[statusCode] } };

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
