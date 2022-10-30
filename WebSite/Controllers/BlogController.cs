using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSite.Api;
using WebSite.Models;

namespace WebSite.Controllers;
public class BlogController : Controller
{
    DataApi dataApi = new DataApi();


    /// <summary>
        /// Вывод всех блогов
        /// </summary>
        /// <returns></returns>
    public IActionResult Index()
    {
        ViewData["title"] = "Блоги";

        var a = dataApi.GetModel("GetBlogs");

        try
        {
            var response = JsonConvert.DeserializeObject<Response>(a);

            if (response?.ok == true)
            {
                var blogsString = JObject.Parse(a)["result"]?["items"]?.ToString();

                var blogs = JsonConvert.DeserializeObject<List<Blogs>>(blogsString);

                return View(blogs);
            }

        }
        catch { }

        return View();
    }


    /// <summary>
    /// Вывод определенного проекта
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{controller}/{action}/{id}")]
    public IActionResult View(int id)
    {
        var a = dataApi.GetSelectedModel("GetBlogs", id);

        try
        {
            var response = JsonConvert.DeserializeObject<Response>(a);

            if (response?.ok == true)
            {
                var blogString = JObject.Parse(a)["result"]?["items"]?.ToString();

                var blog = JsonConvert.DeserializeObject<Blogs>(blogString);

                ViewData["title"] = blog?.Title;

                return View(blog);
            }

        }
        catch { }

        return Redirect("/NotFound/");

    }

}

