using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;
using static WebApi.Helpers.FileHelper;
using static WebApi.Models.ErrorMessages;

namespace WebApi.Controllers
{
    public class AddBlogController : Controller
    {
        AccessVerification accessVerification = new AccessVerification();
        IWebHostEnvironment _appEnvironment;
        int statusCode;
        Response response;


        public AddBlogController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }


        /// <summary>
        /// Добавить блог
        /// </summary>
        /// <param name="json"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/{controller}")]
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] RequestedData<Blog> requestedData, [FromServices] DataContext context)
        {
            if (ModelState.IsValid)
            {
                var ControllerName = ControllerContext.ActionDescriptor.ControllerName;

                var result = accessVerification.GetResultAccess(context, requestedData, ControllerName, "modify");

                if (result == true)
                {
                    string directoryPath = await GetFile(requestedData.model.Image, _appEnvironment);
                    requestedData.model.Image = directoryPath;

                    try
                    {
                        await context.Blogs.AddAsync(requestedData.model);

                        await context.SaveChangesAsync();

                        statusCode = 200;

                        response = new Response { ok = true, status_code = statusCode, result = new { description = "Блог успешно добавлен" } };
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
