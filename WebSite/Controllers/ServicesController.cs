using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

            var a = dataApi.GetModel("GetServices");

            try
            {
                var response = JsonConvert.DeserializeObject<Response>(a);

                if (response?.ok == true)
                {
                    var servicesString = JObject.Parse(a)["result"]?["items"]?.ToString();

                    var services = JsonConvert.DeserializeObject<List<Services>>(servicesString);

                    return View(services);
                }

            }
            catch { }

            return View();

        }


    }
}
