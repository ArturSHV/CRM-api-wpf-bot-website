using Microsoft.AspNetCore.Mvc;
using WebSite.Models;
using WebSite.Api;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    {
        DataApi dataApi = new();
        

        /// <summary>
        /// Главная страница
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewData["title"] = "Главная страница";

            var a = dataApi.GetModel("GetMainPage");

            try
            {
                var response = JsonConvert.DeserializeObject<Response>(a);

                if (response?.ok == true)
                {
                    var mainPageString = JObject.Parse(a)["result"]?["items"]?.ToString();

                    var mainPage = JsonConvert.DeserializeObject<MainPageModel>(mainPageString);

                    return View(mainPage);
                }

            }
            catch { }

            return View();
        }


        /// <summary>
        /// Обработчик обратной связи
        /// </summary>
        /// <param name="callBackModel"></param>
        /// <returns></returns>
        [HttpPost]
        public string Responser(CallBackModel callBackModel)
        {
            var a = dataApi.AddModel(new { model = callBackModel }, "SendRequest");

            try
            {
                var response = JsonConvert.DeserializeObject<Response>(a);

                if (response?.ok == true)
                {
                    return "true";
                }
            }
            catch { }

            return "false";
        }

    }
}
