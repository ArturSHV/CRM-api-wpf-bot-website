﻿using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;
using static WebApi.Models.ErrorMessages;

namespace WebApi.Controllers
{
    public class DeleteProjectController : Controller
    {
        AccessVerification accessVerification = new AccessVerification();
        int statusCode;
        Response response;


        /// <summary>
        /// Удалить Проект
        /// </summary>
        /// <param name="json"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/{controller}")]
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] RequestedData<int> requestedData, [FromServices] DataContext context)
        {
            if (ModelState.IsValid)
            {
                var ControllerName = ControllerContext.ActionDescriptor.ControllerName;

                var result = accessVerification.GetResultAccess(context, requestedData, ControllerName, "modify");

                if (result == true)
                {
                    try
                    {
                        Project? a = context.Projects.FirstOrDefault(x => x.Id == requestedData.model);

                        if (a != null)
                        {
                            context.Projects.Remove(a);

                            await context.SaveChangesAsync();

                            statusCode = 200;

                            response = new Response() { ok = true, status_code = statusCode, result = new { description = "Проект успешно удален" } };
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
