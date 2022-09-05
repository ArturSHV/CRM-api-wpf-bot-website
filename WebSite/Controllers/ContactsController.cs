using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSite.Api;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class ContactsController : Controller
    {
        DataApi dataApi = new();
        public IActionResult Index()
        {
            ViewData["title"] = "Контакты";

            var a = dataApi.GetContacts();
            if (a != null)
            {
                string c = JObject.FromObject(a).ToString();

                Contacts? contacts = JsonConvert.DeserializeObject<Contacts>(c);

                return View(contacts);
            }
            else
                return View();
        }
    }
}
