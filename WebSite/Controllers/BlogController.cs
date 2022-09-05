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

        var a = dataApi.GetBlogs();

        if (a != null)
        {
            string c = JArray.FromObject(a).ToString();

            List<Blogs>? blogs = JsonConvert.DeserializeObject<List<Blogs>>(c);

            //var selectedUser = user.Where(x => x.Id == 4).FirstOrDefault(); //.Equals("jenmay")

            return View(blogs);
        }
        else
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
        var a = dataApi.GetSelectedBlog(id);

        if (a != null)
        {
            string c = JObject.FromObject(a).ToString();

            Blogs? blog = JsonConvert.DeserializeObject<Blogs>(c);

            ViewData["title"] = blog.Title;

            return View(blog);
        }
        else
            return Redirect("/NotFound/");

    }

}

