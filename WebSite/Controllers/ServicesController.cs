using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebSite.Api;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class ServicesController : Controller
    {
        DataApi dataApi = new();


        /// <summary>
        /// Вывод всех услуг
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewData["title"] = "Услуги";

            var a = dataApi.GetServices();

            if (a != null)
            {
                var c = a.ToString();

                List<Services>? services = JsonConvert.DeserializeObject<List<Services>>(c);

                return View(services);
            }
            else
                return View();

            
        }


    }
}
