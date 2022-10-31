using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;
using static WebApi.Helpers.HashPasswordHelper;
using static WebApi.Helpers.TokenHelper;
using static WebApi.Models.ErrorMessages;

namespace WebApi.Controllers;
public class AuthorizeController : Controller
{
    Response result;
    int statusCode;
  

    [Route("/api/{controller}")]
    [HttpPost]
    public IActionResult Index([FromServices] DataContext dataContext, [FromBody] Account account)
    {
        try
        {
            var salt = (dataContext.Accounts.FirstOrDefault(x => x.Login == account.Login) != null ?
            dataContext.Accounts.FirstOrDefault(x => x.Login == account.Login)?.Salt : "");

            var a = dataContext.Accounts.FirstOrDefault(x => (x.Login == account.Login) && (x.Password == HashPassword(account.Password + salt)));

            if (a == null)
            {
                statusCode = 401;

                result = new Response{ ok = false, status_code = statusCode, result = new { description = errorMessage[statusCode] } };
            }
            else
            {
                var role = dataContext.Roles.FirstOrDefault(x => x.Id == a.IdRole)?.Name;

                var token = GenerateToken(a.Id, new ClaimType() { role = role });

                statusCode = 200;

                result = new Response { ok = true, status_code = statusCode, result = new { token = token, role = role } };

            }
        }
        catch
        {
            statusCode = 500;

            result = new Response { ok = false, status_code = statusCode, result = new { description = errorMessage[statusCode] } };
        }

        return StatusCode(statusCode, result);
    }
}
