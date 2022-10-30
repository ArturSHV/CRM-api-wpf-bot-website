using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;
using static WebApi.Helpers.FileHelper;
using static WebApi.Models.ErrorMessages;

namespace WebApi.Controllers
{
    public class EditProjectController : Controller
    {
        AccessVerification accessVerification = new AccessVerification();
        IWebHostEnvironment _appEnvironment;
        int statusCode;
        Response response;


        public EditProjectController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }


        /// <summary>
        /// Изменить проекты
        /// </summary>
        /// <param name="json"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/{controller}")]
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] RequestedData<Project> requestedData, [FromServices] DataContext context)
        {
            if (ModelState.IsValid)
            {
                var ControllerName = ControllerContext.ActionDescriptor.ControllerName;

                var result = accessVerification.GetResultAccess(context, requestedData, ControllerName, "modify");

                if (result == true)
                {
                    string directoryPath = requestedData.model.Image;

                    var checkImage = context.Projects.FirstOrDefault(x => x.Image == requestedData.model.Image);
                    if (checkImage == null)
                    {
                        directoryPath = await GetFile(requestedData.model.Image, _appEnvironment);
                    }

                    try
                    {
                        Project? a = context.Projects.FirstOrDefault(x => x.Id == requestedData.model.Id);

                        if (a != null)
                        {
                            a.Title = requestedData.model.Title;
                            a.Image = directoryPath;
                            a.Description = requestedData.model.Description;

                            await context.SaveChangesAsync();

                            statusCode = 200;

                            response = new Response { ok = true, status_code = statusCode, result = new {description = "Проект успешно отредактирован" }  };
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
