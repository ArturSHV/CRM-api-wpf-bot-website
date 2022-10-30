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

            var a = dataApi.GetModel("GetContacts");
            try
            {
                var response = JsonConvert.DeserializeObject<Response>(a);

                if (response?.ok == true)
                {
                    var contactsString = JObject.Parse(a)["result"]?["items"]?.ToString();

                    var contacts = JsonConvert.DeserializeObject<Contacts>(contactsString);

                    return View(contacts);
                }

            }
            catch { }

            return View();
        }
    }
}
