using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSite.Api;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class ProjectController : Controller
    {
        DataApi dataApi = new();


        /// <summary>
        /// Вывод всех проектов
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewData["title"] = "Проекты";

            var a = dataApi.GetModel("GetProjects");

            try
            {
                var response = JsonConvert.DeserializeObject<Response>(a);

                if (response?.ok == true)
                {
                    var projectsString = JObject.Parse(a)["result"]?["items"]?.ToString();

                    var projects = JsonConvert.DeserializeObject<List<Projects>>(projectsString);

                    return View(projects);
                }

            }
            catch {}

            return View();

        }


        [Route("{controller}/{action}/{id}")]
        /// <summary>
        /// Вывод определенного проекта
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult View([FromRoute] int id)
        {
            var a = dataApi.GetSelectedModel("GetProjects", id);

            try
            {
                var response = JsonConvert.DeserializeObject<Response>(a);

                if (response?.ok == true)
                {
                    var projectString = JObject.Parse(a)["result"]?["items"]?.ToString();

                    var project = JsonConvert.DeserializeObject<Projects>(projectString);

                    ViewData["title"] = project?.Title;

                    return View(project);
                }    
                
            }
            catch { }

            return Redirect("/NotFound/");

        }


    }
}
