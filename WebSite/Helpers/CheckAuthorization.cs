using WebSite.Models;

namespace WebSite.Helpers
{
    /// <summary>
    /// Класс проверки куки
    /// </summary>
    public class CheckAuthorization
    {
        public Authorize authorize = new() { Host = "", Login = "", IP = "", UserAgent = "", Role = 1};

        /// <summary>
        /// проверка наличия информации в куки
        /// </summary>
        /// <param name="httpContext">HttpContext</param>
        /// <param name="controller">текущий контроллер</param>
        public Authorize Check(HttpContext httpContext)
        {
            if (httpContext.Session.Keys.Contains("Login") &&
                httpContext.Session.Keys.Contains("Role") &&
                httpContext.Session.Keys.Contains("Host") &&
                httpContext.Session.Keys.Contains("IP") &&
                httpContext.Session.Keys.Contains("userAgent"))
            {
                if (!String.IsNullOrEmpty(httpContext.Session.GetString("Login")) &&
                    !String.IsNullOrEmpty(httpContext.Session.GetString("Role")) &&
                    !String.IsNullOrEmpty(httpContext.Session.GetString("Host")) &&
                    !String.IsNullOrEmpty(httpContext.Session.GetString("IP")) &&
                    !String.IsNullOrEmpty(httpContext.Session.GetString("userAgent")))
                {
                    
                    authorize = new()
                    {
                        Host = httpContext.Session.GetString("Host"),
                        Role = Convert.ToInt32(httpContext.Session.GetString("Role")),
                        IP = httpContext.Session.GetString("IP"),
                        Login = httpContext.Session.GetString("Login"),
                        UserAgent = httpContext.Session.GetString("userAgent")
                    };
                }

            }
            return authorize;
        }
    }
}
