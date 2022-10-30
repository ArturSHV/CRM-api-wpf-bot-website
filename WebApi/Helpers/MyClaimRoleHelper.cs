using static WebApi.Helpers.TokenHelper;
using WebApi.Models;
using Newtonsoft.Json;


namespace WebApi.Helpers;

public class MyClaimRoleHelper
{
    public string? GetRole(string token)
    {
        var isValidate = ValidateCurrentToken(token);

        if (isValidate)
        {
            string? claim = GetClaim(token);

            var myClaim = JsonConvert.DeserializeObject<ClaimType>(claim);

            if (myClaim != null)
            {
                return myClaim.role;
            }
        }

        return null;
    }
}


