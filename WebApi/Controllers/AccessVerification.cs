using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebApi.Data;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Controllers;


[NonController]
public class AccessVerification : Controller
{
    MyClaimRoleHelper myClaimRole = new MyClaimRoleHelper();

    bool result;

    public bool GetResultAccess<T>(DataContext dataContext, RequestedData<T> requestedData, string ControllerName, string typePermission)
    {
        var role = myClaimRole.GetRole(requestedData.token);

        if (role != null)
        {
            var a = dataContext.Roles.FirstOrDefault(x => x.Name == role);

            if (a != null)
            {
                var permissions = a.Permission;

                var resultPermission = JObject.Parse(permissions)[typePermission]?.FirstOrDefault(x => (x.Value<string>() == ControllerName) || (x.Value<string>() == "*"));

                if (resultPermission == null)
                {
                    result = false;

                }
                else
                {
                    result = true;
                }

            }
        }
        else
        {
            result = false;
        }

        return result;

    }

}