using WebApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Helpers
{
    public static class TokenHelper
    {

        /// <summary>
        /// Генерация токена
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="claimTypeModel">Модель, которая записана в токен</param>
        /// <returns></returns>
        public static string GenerateToken(int userId, ClaimType claimTypeModel)
        {
            var mySecret = "asdv234234^&%&^%&^hjsdfb2%%%";
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));
            
            var nameClaimType = claimTypeModel.GetType().Name;

            var myIssuer = SettingsHelpers.ReturnHostString();
            var myAudience = SettingsHelpers.ReturnHostString();

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = myIssuer,
                Audience = myAudience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature),
                Claims = new Dictionary<string, object>() { { nameClaimType, claimTypeModel } }
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



        /// <summary>
        /// Расшифровать токен. Возвращает модель ClaimType
        /// </summary>
        /// <param name="token">Токен</param>
        /// <param name="claimType">Модель, которая записана в токен</param>
        /// <returns></returns>
        public static string? GetClaim(string token)
        {
            ClaimType claimTypeModel = new ClaimType();

            string claimType = claimTypeModel.GetType().Name;

            string? stringClaimValue = null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (securityToken != null)
            {
                stringClaimValue = (securityToken.Claims.First(claim => claim.Type == claimType) != null ?
                    securityToken.Claims.First(claim => claim.Type == claimType).Value : null);
            }
            return stringClaimValue;
        }



        /// <summary>
        /// Валидация токена. Возвращает bool
        /// </summary>
        /// <param name="token">Токен</param>
        /// <returns></returns>
        public static bool ValidateCurrentToken(string token)
        {
            var mySecret = "asdv234234^&%&^%&^hjsdfb2%%%";
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var myIssuer = SettingsHelpers.ReturnHostString();
            var myAudience = SettingsHelpers.ReturnHostString();

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = myIssuer,
                    ValidAudience = myAudience,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
