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

            var a = dataApi.GetProjects();

            if (a != null)
            {
                string c = JArray.FromObject(a).ToString();

                List<Projects>? projects = JsonConvert.DeserializeObject<List<Projects>>(c);

                return View(projects);
            }
            else
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
            var a = dataApi.GetSelectedProject(id);

            if (a != null)
            {
                string c = JObject.FromObject(a).ToString();

                Projects? project = JsonConvert.DeserializeObject<Projects>(c);

                ViewData["title"] = project.Title;

                return View(project);
            }
            else
                return Redirect("/NotFound/");

        }


    }
}
