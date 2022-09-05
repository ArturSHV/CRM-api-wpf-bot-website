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

            var a = dataApi.GetMainPage();

            if (a != null)
            {
                string c = JObject.FromObject(a).ToString();

                MainPageModel? mainPageModel = JsonConvert.DeserializeObject<MainPageModel>(c);

                return View(mainPageModel);
            }
            else
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
            var a = dataApi.CallBack(callBackModel);

            if (a != null)
                return "true";
            
            else
                return "false";
        }

    }
}
