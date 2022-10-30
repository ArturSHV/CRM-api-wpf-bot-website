using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSite.Models;
using WebSite.Api;
using WebSite.Helpers;
using System.Net;
using static WebSite.Helpers.FileHelper;



namespace WebSite.Controllers
{
    public class AdminController : Controller
    {
        
        DataApi dataApi = new();


        /// <summary>
        /// Проверка авторизации
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private bool GiveAccess()
        {
            CheckAuthorization checkAuthorization = new(); //класс проверки куки

            string Host = Dns.GetHostName(); //получить текущий хост
            string IP = Dns.GetHostByName(Host).AddressList[1].ToString(); //получить текущий ip
            string userAgent = HttpContext.Request.Headers["User-Agent"]; //получить текущий user-agent

            Authorize sessionAuthorize = checkAuthorization.Check(HttpContext);
            var authorize = new Authorize() { Host = Host, IP = IP, UserAgent = userAgent};

            if ((sessionAuthorize.UserAgent == authorize.UserAgent) &&
                (sessionAuthorize.Host == authorize.Host) &&
                (sessionAuthorize.Role == "Администратор") &&
                (sessionAuthorize.IP == authorize.IP))
            {
                return true;
            }
            else
                return false;
                
        }

        /// <summary>
        /// получить токен с куки
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private string? GetTokenFromCookie(HttpContext httpContext)
        {
            string token = "";
            if (httpContext.Session.Keys.Contains("Token"))
            {
                if (!String.IsNullOrEmpty(httpContext.Session.GetString("Token")))
                {
                    token = httpContext.Session.GetString("Token");
                }
            }

            return token;
        }


        /// <summary>
        /// Действия для моделей
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <returns></returns>
        private IActionResult ReturnModelInView<T>(string responseFromServer)
            where T : IModel
        {
            var modelString = JObject.Parse(responseFromServer)["result"]?["items"]?.ToString();

            var model = JsonConvert.DeserializeObject<T>(modelString);

            return View(model);
        }


        /// <summary>
        /// Действие для коллекций
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <returns></returns>
        private IActionResult ReturnListModelInView<T>(string responseFromServer)
        {
            var modelString = JObject.Parse(responseFromServer)["result"]?["items"]?.ToString();

            var model = JsonConvert.DeserializeObject<List<T>>(modelString);

            return View(model);
        }


        /// <summary>
        /// Проверка ответа с сервера и возврат результата
        /// </summary>
        /// <param name="a"></param>
        /// <param name="actionResult"></param>
        /// <returns></returns>
        private IActionResult ResponseHandler(string responseFromServer, IActionResult actionResult)
        {
            string? message;

            int? status;

            try
            {
                var response = JsonConvert.DeserializeObject<Response>(responseFromServer);

                if (response?.ok == true)
                {
                    return actionResult;
                }
                else
                {
                    status = JObject.Parse(responseFromServer)["status_code"]?.Value<int>();
                    message = JObject.Parse(responseFromServer)["result"]?.Value<string>();
                }

            }
            catch
            {
                status = 400;
                message = "Bad Request";
            }

            return Redirect($"/Error/{message}/{status}");
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

            var a = dataApi.GetModel("GetBlogs");

            return ResponseHandler(a, ReturnListModelInView<Blogs>(a));

        }


        /// <summary>
        /// Вывод определенного блога
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{controller}/{action}/{id}")]
        public IActionResult ViewBlog(int id)
        {
            if (GiveAccess() == false)
                return Redirect("/Admin/Login");

            var a = dataApi.GetSelectedModel("GetBlogs", id);

            return ResponseHandler(a, ReturnModelInView<Blogs>(a));
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

            var token = GetTokenFromCookie(HttpContext);

            var a = dataApi.EditModel(new { token = token, model = blog}, "EditBlog");

            return ResponseHandler(a, Redirect($"/Admin/ViewBlog/{blog.Id}"));

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
        public IActionResult AddBlog(Blogs blog, IFormFile formFile)
        {
            if (GiveAccess() == false)
                return Redirect("/Admin/Login");

            if (formFile != null)
            {
                blog.CreateDate = DateTime.Now;

                blog.Image = GetFile(formFile);

                var token = GetTokenFromCookie(HttpContext);

                var a = dataApi.AddModel(new { token = token, model = blog }, "AddBlog");

                return ResponseHandler(a, Redirect("/Admin/Blogs"));

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

            string? token = GetTokenFromCookie(HttpContext);

            string? a = dataApi.DeleteModel(new { token = token, model = Id }, "DeleteBlog");

            return ResponseHandler(a, Redirect("/Admin/Blogs"));

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

            var a = dataApi.GetModel("GetProjects");

            return ResponseHandler(a, ReturnListModelInView<Projects>(a));
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

            var a = dataApi.GetSelectedModel("GetProjects", id);

            return ResponseHandler(a, ReturnModelInView<Projects>(a));

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

            var token = GetTokenFromCookie(HttpContext);

            var a = dataApi.EditModel(new { token = token, model = project }, "EditProject");

            return ResponseHandler(a, Redirect($"/Admin/ViewProject/{project.Id}"));

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

                var token = GetTokenFromCookie(HttpContext);

                var a = dataApi.AddModel(new { token = token, model = project }, "AddProject");

                return ResponseHandler(a, Redirect("/Admin/Projects"));

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

            var token = GetTokenFromCookie(HttpContext);

            var a = dataApi.DeleteModel(new { token = token, model = Id }, "DeleteProject");

            return ResponseHandler(a, Redirect("/Admin/Projects"));

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

            var a = dataApi.GetModel("GetServices");

            return ResponseHandler(a, ReturnListModelInView<Services>(a));
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

            var token = GetTokenFromCookie(HttpContext);

            var a = dataApi.EditModel(new { token = token, model = service }, "EditService");

            return ResponseHandler(a, Redirect("/Admin/Services"));

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

            var token = GetTokenFromCookie(HttpContext);

            var a = dataApi.AddModel(new { token = token, model = service }, "AddService");

            return ResponseHandler(a, Redirect("/Admin/Services"));

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

            var token = GetTokenFromCookie(HttpContext);

            var a = dataApi.DeleteModel(new { token = token, model = Id }, "DeleteService");

            return ResponseHandler(a, Redirect("/Admin/Services"));
        }
        #endregion


        #region Раздел Рабочий стол

        /// <summary>
        /// Создание MessageWithStatus 
        /// </summary>
        /// <param name="data1"></param>
        /// <param name="data2"></param>
        /// <returns></returns>
        private MessageWithStatus? MessageWithStatusCreate(string data1, string data2)
        {
            try
            {
                var model1 = JsonConvert.DeserializeObject<Response>(data1);

                var model2 = JsonConvert.DeserializeObject<Response>(data2);

                if ((model1?.ok == true) && (model2?.ok == true))
                {
                    var modelStringData1 = JObject.Parse(data1)["result"]?["items"]?.ToString();

                    var modelStringData2 = JObject.Parse(data2)["result"]?["items"]?.ToString();

                    var modelMessages = JsonConvert.DeserializeObject<List<MessageStatus>>(modelStringData1);

                    var modelStatuses = JsonConvert.DeserializeObject<List<Statuses>>(modelStringData2);

                    var messageWithStatus = new MessageWithStatus() { messageStatus = modelMessages, statuses = modelStatuses };

                    return messageWithStatus;
                }

            }
            catch { }

            return null;
        }


        /// <summary>
        /// Рабочий стол
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if (GiveAccess() == false)
                return Redirect("/Admin/Login");

            var token = GetTokenFromCookie(HttpContext);

            var messages = dataApi.GetModel(new { token = token }, "GetMessages");

            var statuses = dataApi.GetModel(new { token = token }, "GetStatuses");

            var messageWithStatus = MessageWithStatusCreate(messages, statuses);

            if (messageWithStatus != null)
                return View(messageWithStatus);
            
            else
                return Redirect("/Error/Not Found/404");
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

            var token = GetTokenFromCookie(HttpContext);

            var messages = dataApi.GetModel(new { token = token}, "GetMessages");

            var statuses = dataApi.GetModel(new { token = token }, "GetStatuses");

            var messageWithStatus = MessageWithStatusCreate(messages, statuses);

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

            var token = GetTokenFromCookie(HttpContext);

            var messages = dataApi.GetModel(new { token = token }, "GetMessages");

            var statuses = dataApi.GetModel(new { token = token }, "GetStatuses");

            var messageWithStatus = MessageWithStatusCreate(messages, statuses);

            MessageWithStatus? s = null;

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

            var token = GetTokenFromCookie(HttpContext);

            var messages = dataApi.GetModel(new { token = token }, "GetMessages");

            var statuses = dataApi.GetModel(new { token = token }, "GetStatuses");

            var messageWithStatus = MessageWithStatusCreate(messages, statuses);

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
            var token = GetTokenFromCookie(HttpContext);

            var model = new { Id = id, Status = status };

            string? a = dataApi.EditModel(new {token = token, model = model}, "EditStatus");

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

            var a = dataApi.GetModel("GetMainPage");

            return ResponseHandler(a, ReturnModelInView<MainPageModel>(a));
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

            if (formFile != null)
            {
                string extension = Path.GetExtension(formFile.FileName);

                if ((extension == "png") || (extension == "jpg") || (extension == "jpeg"))
                {
                    string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", "header.jpg");

                    using var stream = new FileStream(SavePath, FileMode.Create);
                    await formFile.CopyToAsync(stream);
                }
                else
                    return Redirect("/Error/Допустимые форматы загрузки .jpg, .png, .jpeg/400");
            }

            var token = GetTokenFromCookie(HttpContext);

            var a = dataApi.EditModel(new {model = mainPageModel, token = token }, "EditMainPage");

            return ResponseHandler(a, Redirect("/Admin/Home"));
        }
        #endregion


        #region Раздел контактов
        /// <summary>
        /// Получить Контакты
        /// </summary>
        /// <returns></returns>
        public IActionResult Contacts()
        {
            if (GiveAccess() == false)
                return Redirect("/Admin/Login");

            var a = dataApi.GetModel("GetContacts");

            return ResponseHandler(a, ReturnModelInView<Contacts>(a));
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

            var token = GetTokenFromCookie(HttpContext);

            var a = dataApi.EditModel(new {token = token, model = contacts }, "EditContacts");

            return ResponseHandler(a, Redirect("/Admin/Contacts"));

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

            var a = dataApi.GetToken(account);

            try
            {
                var response = JsonConvert.DeserializeObject<Response>(a);

                if (response?.ok == true)
                {
                    var token = JObject.Parse(a)["result"]?["token"]?.Value<string>();

                    var role = JObject.Parse(a)["result"]?["role"]?.Value<string>();

                    string Host = Dns.GetHostName();

                    string IP = Dns.GetHostByName(Host).AddressList[1].ToString();

                    string userAgent = HttpContext.Request.Headers["User-Agent"];

                    HttpContext.Session.SetString("Login", account.Login); //установка куки
                    HttpContext.Session.SetString("Role", role); //установка куки
                    HttpContext.Session.SetString("Host", Host); //установка куки
                    HttpContext.Session.SetString("IP", IP); //установка куки
                    HttpContext.Session.SetString("userAgent", userAgent); //установка куки
                    HttpContext.Session.SetString("Token", token); //установка куки
                    return Redirect("/Admin");
                }
                else
                {
                    var description = JObject.Parse(a)["result"]?["description"]?.Value<string>();

                    ViewData["Message"] = description;

                    return View(account);
                }

            }
            catch { }

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
        
    }
}
