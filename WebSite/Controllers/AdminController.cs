using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSite.Models;
using WebSite.Api;
using WebSite.Helpers;
using System.Net;

namespace WebSite.Controllers
{
   
    public class AdminController : Controller
    {
        CheckAuthorization checkAuthorization = new();
        Authorize? authorize;
        DataApi dataApi = new();


        /// <summary>
        /// Проверка авторизации
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private bool GiveAccess()
        {
            string Host = Dns.GetHostName();
            string IP = Dns.GetHostByName(Host).AddressList[1].ToString();
            string userAgent = HttpContext.Request.Headers["User-Agent"];

            Authorize sessionAuthorize = checkAuthorization.Check(HttpContext);
            authorize = new Authorize() { Host = Host, IP = IP, UserAgent = userAgent, Login = sessionAuthorize.Login};

            if ((sessionAuthorize.UserAgent == authorize.UserAgent) &&
                (sessionAuthorize.Host == authorize.Host) &&
                (sessionAuthorize.Login == authorize.Login) &&
                (sessionAuthorize.Role == 2) &&
                (sessionAuthorize.IP == authorize.IP))
            {
                return true;
            }
            else
                return false;
                
        }


        /// <summary>
        /// Создание MessageWithStatus
        /// </summary>
        /// <param name="a"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        private MessageWithStatus? CreateMWS(object? a, object? status)
        {
            MessageWithStatus messageWithStatus = null;
            if ((a != null) && (status != null))
            {
                string c = JArray.FromObject(a).ToString();
                string s = JArray.FromObject(status).ToString();

                List<MessageStatus>? messageStatuses = JsonConvert.DeserializeObject<List<MessageStatus>>(c);
                List<Statuses>? statuses = JsonConvert.DeserializeObject<List<Statuses>>(s);

                messageWithStatus = new MessageWithStatus() { messageStatus = messageStatuses, statuses = statuses };
            }

            return messageWithStatus;
        }
        

        #region Раздел Блогов
        /// <summary>
        /// Вывод всех блогов
        /// </summary>
        /// <returns></returns>
        public IActionResult Blogs()
        {
            if (GiveAccess() == false)
                return Redirect("/Admin/Login");

            var a = dataApi.GetBlogs();

            if (a != null)
            {
                string c = JArray.FromObject(a).ToString();

                List<Blogs>? blogs = JsonConvert.DeserializeObject<List<Blogs>>(c);

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
        public IActionResult ViewBlog(int id)
        {
            if (GiveAccess() == false)
                return Redirect("/Admin/Login");

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


        /// <summary>
        /// Отредактировать блог
        /// </summary>
        /// <param name="blog"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditBlog(Blogs blog, IFormFile formFile)
        {
            if (GiveAccess() == false)
                return Redirect("/Admin/Login");

            if (formFile != null)
            {
                var filePath = Path.GetTempFileName() + formFile.FileName;

                using (var stream = System.IO.File.Create(filePath))
                {
                    await formFile.CopyToAsync(stream);
                }

                blog.Image = filePath;
            }

            string? a = dataApi.EditBlog(blog);

            return Redirect($"/Admin/ViewBlog/{blog.Id}");


        }


        /// <summary>
        /// Вьюшка добавить блог
        /// </summary>
        /// <returns></returns>
        public IActionResult AddBlog()
        {
            if (GiveAccess() == false)
                return Redirect("/Admin/Login");

            return View();
        }


        /// <summary>
        /// Обработчик добавить блог
        /// </summary>
        /// <param name="blog"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddBlog(Blogs blog, IFormFile formFile)
        {
            if (GiveAccess() == false)
                return Redirect("/Admin/Login");

            if (formFile != null)
            {
                string filePath = Path.GetTempFileName() + formFile.FileName;

                using (var stream = System.IO.File.Create(filePath))
                {
                    await formFile.CopyToAsync(stream);
                }

                blog.CreateDate = DateTime.Now;
                blog.Image = filePath;
                string? a = dataApi.AddBlog(blog);

                return Redirect("/Admin/Blogs");
            }
            else
            {
                ViewBag.Message = "Загрузите картинку!";
                return View();
            }
        }


        /// <summary>
        /// Удалить блог
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteBlog(int Id)
        {
            if (GiveAccess() == false)
                return Redirect("/Admin/Login");

            Blogs blogs = new() { Id = Id };

            string? a = dataApi.DeleteBlog(blogs);

            return Redirect("/Admin/Blogs");

        }
        #endregion


        #region Раздел проектов
        /// <summary>
        /// Все проекты
        /// </summary>
        /// <returns></returns>
        public IActionResult Projects()
        {
            if (GiveAccess() == false)
                return Redirect("/Admin/Login");

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


        /// <summary>
        /// Вывод определенного проекта
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{controller}/{action}/{id}")]
        public IActionResult ViewProject(int id)
        {
            if (GiveAccess() == false)
                return Redirect("/Admin/Login");

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


        /// <summary>
        /// Отредактировать проект
        /// </summary>
        /// <param name="project"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditProject(Projects project, IFormFile formFile)
        {
            if (GiveAccess() == false)
                return Redirect("/Admin/Login");

            if (formFile != null)
            {
                string filePath = Path.GetTempFileName() + formFile.FileName;

                using (var stream = System.IO.File.Create(filePath))
                {
                    await formFile.CopyToAsync(stream);
                }

                project.Image = filePath;
            }

            var a = dataApi.EditProject(project);

            return Redirect($"/Admin/ViewProject/{project.Id}");

        }
        

        /// <summary>
        /// Вьюшка добавить проект
        /// </summary>
        /// <returns></returns>
        public IActionResult AddProject()
        {
            if (GiveAccess() == false)
                return Redirect("/Admin/Login");

            return View();
        }


        /// <summary>
        /// Обработчик добавить проект
        /// </summary>
        /// <param name="blog"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddProject(Projects project, IFormFile formFile)
        {
            if (GiveAccess() == false)
                return Redirect("/Admin/Login");

            if (formFile != null)
            {
                string filePath = Path.GetTempFileName() + formFile.FileName;

                using (var stream = System.IO.File.Create(filePath))
                {
                    await formFile.CopyToAsync(stream);
                }

                project.Image = filePath;
                string? a = dataApi.AddProject(project);

                return Redirect("/Admin/Projects");
            }
            else
            {
                ViewBag.Message = "Загрузите картинку!";
                return View();
            }
        }


        /// <summary>
        /// Удалить проект
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteProject(int Id)
        {
            if (GiveAccess() == false)
                return Redirect("/Admin/Login");

            Projects projects = new() { Id = Id };
            
            string? a = dataApi.DeleteProject(projects);

            return Redirect("/Admin/Projects");

        }
        #endregion


        #region Раздел Услуг
        /// <summary>
        /// Все услуги
        /// </summary>
        /// <returns></returns>
        public IActionResult Services()
        {
            if (GiveAccess() == false)
                return Redirect("/Admin/Login");

            var a = dataApi.GetServices();

            if (a != null)
            {
                string? c = a.ToString();

                List<Services>? services = JsonConvert.DeserializeObject<List<Services>>(c);

                return View(services);
            }
            else
                return View();
        }


        /// <summary>
        /// Редактировать услугу
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditService(Services service)
        {
            if (GiveAccess() == false)
                return Redirect("/Admin/Login");

            string? a = dataApi.EditService(service);

            return Redirect("/Admin/Services");

        }


        /// <summary>
        /// Вьюшка добавить услугу
        /// </summary>
        /// <returns></returns>
        public IActionResult AddService()
        {
            if (GiveAccess() == false)
                return Redirect("/Admin/Login");

            return View();
        }


        /// <summary>
        /// обработчик добавить услугу
        /// </summary>
        /// <param name="blog"></param>
        /// <param name="formfile"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddService(Services service)
        {
            if (GiveAccess() == false)
                return Redirect("/Admin/Login");

            string? a = dataApi.AddService(service);

            return Redirect("/Admin/Services");

        }


        /// <summary>
        /// Удалить услугу
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteService(int Id) 
        {
            if (GiveAccess() == false)
                return Redirect("/Admin/Login");

            Services services = new() { Id = Id };

            string? a = dataApi.DeleteService(services);

            return Redirect("/Admin/Services");

        }
        #endregion


        #region Раздел Рабочий стол
        /// <summary>
        /// Рабочий стол
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if (GiveAccess() == false)
                return Redirect("/Admin/Login");

            object? a = dataApi.GetMessages();
            object? status = dataApi.GetStatuses();

            MessageWithStatus? messageWithStatus = CreateMWS(a, status);

            if (messageWithStatus != null)
            {
                return View(messageWithStatus);
            }
            else
                return View();
        }


        /// <summary>
        /// Фильтр заявок по дню, неделе, месяцу
        /// </summary>
        /// <param name="today"></param>
        /// <param name="yesterday"></param>
        /// <param name="week"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpPost]
        public MessageWithStatus? TableData(DateTime today, DateTime yesterday, DateTime week, DateTime month)
        {
            DateTime nullDate = new(0001, 01, 01, 0, 00, 00);
            var a = dataApi.GetMessages();
            var status = dataApi.GetStatuses();

            MessageWithStatus? messageWithStatus = CreateMWS(a, status);

            MessageWithStatus? s = null;

            if (messageWithStatus != null)
            {
                if (today != nullDate)
                {
                    s = new()
                    {
                        messageStatus = messageWithStatus.messageStatus
                        .Where(x => x.date.ToShortDateString() == today.ToShortDateString()).ToList(),
                        statuses = messageWithStatus.statuses
                    };
                }

                if (yesterday != nullDate)
                {
                    s = new()
                    {
                        messageStatus = messageWithStatus.messageStatus
                        .Where(x => x.date.ToShortDateString() == yesterday.ToShortDateString()).ToList(),
                        statuses = messageWithStatus.statuses
                    };
                }

                if (week != nullDate)
                {
                    s = new()
                    {
                        messageStatus = messageWithStatus.messageStatus
                        .Where(x => x.date >= week).ToList(),
                        statuses = messageWithStatus.statuses
                    };
                }

                if (month != nullDate)
                {
                    s = new()
                    {
                        messageStatus = messageWithStatus.messageStatus
                        .Where(x => x.date >= month).ToList(),
                        statuses = messageWithStatus.statuses
                    };
                }
            }

            return s;

        }


        /// <summary>
        /// Отфильтровать по статусу заявки
        /// </summary>
        /// <param name="statusName"></param>
        /// <returns></returns>
        [HttpPost]
        public MessageWithStatus? AddFilter(string statusName)
        {
            DateTime nullDate = new DateTime(0001, 01, 01, 0, 00, 00);
            object? a = dataApi.GetMessages();
            object? status = dataApi.GetStatuses();
            MessageWithStatus? s = null;

            MessageWithStatus? messageWithStatus = CreateMWS(a, status);

            if (messageWithStatus != null)
            {
                if (statusName == "Все")
                    return messageWithStatus;

                s = new()
                {
                    messageStatus = messageWithStatus.messageStatus
                        .Where(x => x.status == statusName).ToList(),
                    statuses = messageWithStatus.statuses
                };
            }

            return s;
        }


        /// <summary>
        /// Фильтр заявок по определенной дате
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        [HttpPost]
        public MessageWithStatus? TableDataPeriod(DateTime date1, DateTime date2)
        {
            DateTime nullDate = new(0001, 01, 01, 0, 00, 00);
            object? a = dataApi.GetMessages();
            object? status = dataApi.GetStatuses();

            MessageWithStatus? messageWithStatus = CreateMWS(a, status);

            MessageWithStatus? s = null;

            if (messageWithStatus != null)
            {
                if ((date1 != nullDate) && (date2 != nullDate))
                {
                    s = new()
                    {
                        messageStatus = messageWithStatus.messageStatus
                        .Where(x => ((x.date >= date1) && (x.date <= date2))).ToList(),
                        statuses = messageWithStatus.statuses
                    };
                }

                if ((date1 != nullDate) && (date2 == nullDate))
                {
                    s = new()
                    {
                        messageStatus = messageWithStatus.messageStatus
                        .Where(x => x.date >= date1).ToList(),
                        statuses = messageWithStatus.statuses
                    };
                }

                if ((date1 == nullDate) && (date2 != nullDate))
                {
                    s = new()
                    {
                        messageStatus = messageWithStatus.messageStatus
                        .Where(x => x.date <= date2).ToList(),
                        statuses = messageWithStatus.statuses
                    };
                }
            }

            return s;

        }


        /// <summary>
        /// Изменить статус заявки
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPost]
        public string EditStatus(int id, string status)
        {
            string? a = dataApi.EditStatus(id, status);
            return "Успешно";
        }
        #endregion


        #region Главная страница в админке
        /// <summary>
        /// Главная страница в админке
        /// </summary>
        /// <returns></returns>
        public IActionResult Home()
        {
            if (GiveAccess() == false)
                return Redirect("/Admin/Login");

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
        /// Обработчик редактирования главной страницы
        /// </summary>
        /// <param name="mainPageModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditMainPage(MainPageModel mainPageModel, IFormFile formFile)
        {
            if (GiveAccess() == false)
                return Redirect("/Admin/Login");


            var a = dataApi.EditMainPage(mainPageModel);

            if (formFile != null)
            {
                var indexOfExtension = formFile.FileName.LastIndexOf('.');

                string extension = formFile.FileName[(indexOfExtension + 1)..];

                if ((extension == "png") || (extension == "jpg") || (extension == "jpeg"))
                {
                    string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", "header.jpg");

                    using var stream = new FileStream(SavePath, FileMode.Create);
                    await formFile.CopyToAsync(stream);
                }
                else
                    return BadRequest("Допустимые форматы загрузки .jpg, .png, .jpeg");
            }

            return Redirect("/Admin/Home");
        }
        #endregion


        #region Раздел контактов
        /// <summary>
        /// Контакты
        /// </summary>
        /// <returns></returns>
        public IActionResult Contacts()
        {
            if (GiveAccess() == false)
                return Redirect("/Admin/Login");

            var a = dataApi.GetContacts();

            if (a != null)
            {
                var c = a.ToString();

                Contacts? contacts = JsonConvert.DeserializeObject<Contacts>(c);

                return View(contacts);
            }
            else
                return View();
        }


        /// <summary>
        /// Отредактировать контакты
        /// </summary>
        /// <param name="contacts"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditContacts(Contacts contacts)
        {
            if (GiveAccess() == false)
                return Redirect("/Admin/Login");

            string? a = dataApi.EditContacts(contacts);

            return Redirect("/Admin/Contacts");


        }
        #endregion


        #region Раздел авторизации
        /// <summary>
        /// Авторизация администратора
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            if (GiveAccess() == true)
                return Redirect("/Admin");

            return View();
        }


        /// <summary>
        /// Обработчик авторизации
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(Account account)
        {
            if (GiveAccess() == true)
                return Redirect("/Admin");

            var a = dataApi.GetUser(account);

            if (a != null)
            {
                if (a.ToString() == "")
                {
                    ViewData["Message"] = "Неправильная пара логин-пароль";

                }
                else
                {
                    Account? getAccount = JsonConvert.DeserializeObject<Account>(a.ToString());

                    string Host = Dns.GetHostName();
                    string IP = Dns.GetHostByName(Host).AddressList[1].ToString();
                    string userAgent = HttpContext.Request.Headers["User-Agent"];

                    HttpContext.Session.SetString("Login", account.Login); //установка куки
                    HttpContext.Session.SetString("Role", getAccount.IdRole.ToString()); //установка куки
                    HttpContext.Session.SetString("Host", Host); //установка куки
                    HttpContext.Session.SetString("IP", IP); //установка куки
                    HttpContext.Session.SetString("userAgent", userAgent); //установка куки
                    return Redirect("/Admin");
                }
            }
            else
                ViewData["Message"] = "Ошибка соединения с сервером. Попробуйте немного позже повторить запрос.";

            return View(account);
        }


        /// <summary>
        /// Выход из аккаунта
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); //очистка куки

            return Redirect("/");
        }
        #endregion


        #region Раздел регистрации
        /// <summary>
        /// Страница регистрации
        /// </summary>
        /// <returns></returns>
        public IActionResult Registration()
        {
            if (GiveAccess() == true)
                return Redirect("/Admin");

            return View();

        }


        /// <summary>
        /// Обработчик регистрации
        /// </summary>
        /// <param name="registrationModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Registration(Account account)
        {
            if (GiveAccess() == true)
                return Redirect("/Admin");

            var a = dataApi.RegistrationUser(account);

            if (a.ToString() != "")
            {
                ViewData["Message"] = "Аккаунт успешно зарегистрирован";

            }
            else
            {
                ViewData["Message"] = "Аккаунт с таким логином уже существует!";
            }
            return View(account);
        } 
        #endregion
    }
}
