using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class GetInformationController : Controller
    {
        List<Project> projects = new List<Project>();
        List<Service> services = new List<Service>();
        List<Blog> blogs = new List<Blog>();
        List<MessageStatus> messageStatuses = new List<MessageStatus>();
        List<Statuses> statuses = new List<Statuses>();

       
        /// <summary>
        /// Заполнить массив данных
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="table"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private IActionResult FillArray<T1>(List<T1> table, List<T1> list)
        {
            try
            {
                foreach (var item in table)
                {
                    list.Add(item);
                }

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Возвращает результат страницы
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        private IActionResult ReturnObjectResult<T>(T a)
        {
            if (a == null)
                return BadRequest();
            else
                return Ok(a);
        }


        /// <summary>
        /// Получить список всех проектов. Возвращает результат в теле ответа
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/GetProjects")]
        [HttpPost]
        public IActionResult GetProjects([FromServices] DataContext context)
        {
            var a = context.Projects.ToList();
            return FillArray(a,projects);
        }


        /// <summary>
        /// Получить список услуг. Возвращает результат в теле ответа
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/GetServices")]
        [HttpPost]
        public IActionResult GetServices([FromServices] DataContext context)
        {
            var a = context.Services.ToList();
            return FillArray(a, services);
        }


        /// <summary>
        /// Получить список блогов. Возвращает результат в теле ответа
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/GetBlogs")]
        [HttpPost]
        public IActionResult GetBlogs([FromServices] DataContext context)
        {
            var a = context.Blogs.ToList();
            return FillArray(a, blogs);
        }


        /// <summary>
        /// Получить список контактов. Возвращает результат в теле ответа
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/GetContacts")]
        [HttpPost]
        public IActionResult GetContacts([FromServices] DataContext context)
        {
            var a = context.Contacts.FirstOrDefault();
            return ReturnObjectResult(a);
        }

       
        /// <summary>
        /// Вывод определенного блога
        /// </summary>
        /// <param name="id"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/GetBlogs/{id}")]
        [HttpGet]
        public IActionResult GetBlogs(int id, [FromServices] DataContext context)
        {
            var a = context.Blogs.FirstOrDefault(x=> x.Id == id);
            return ReturnObjectResult(a);
        }


        /// <summary>
            /// Вывод определенного проекта
            /// </summary>
            /// <param name="id"></param>
            /// <param name="context"></param>
            /// <returns></returns>
        [Route("/api/GetProjects/{id}")]
        [HttpGet]
        public IActionResult GetProjects(int id, [FromServices] DataContext context)
        {
            var a = context.Projects.FirstOrDefault(x => x.Id == id);
            return ReturnObjectResult(a);
        }


        /// <summary>
        /// Вывод определенной услуги
        /// </summary>
        /// <param name="id"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/GetServices/{id}")]
        [HttpGet]
        public IActionResult GetServices(int id, [FromServices] DataContext context)
        {
            var a = context.Services.FirstOrDefault(x => x.Id == id);
            return ReturnObjectResult(a);
        }


        /// <summary>
        /// Получить список сообщений
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/GetMessages")]
        [HttpPost]
        public IActionResult GetMessages([FromServices] DataContext context)
        {
            var a = context.Messages.Join(context.Users,
                message => message.Email,
                user => user.Email,
                (message,user) => new MessageStatus
                {
                    Id = message.Id,
                    date = message.Date,
                    Name = user.Name,
                    Text = message.Text,
                    Contact = message.Email,
                    Status = message.Status

                }
                ).OrderBy(x=>x.Id).ToList();

            return FillArray(a, messageStatuses);
        }


        /// <summary>
        /// Получить список статусов
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/GetStatuses")]
        [HttpPost]
        public IActionResult GetStatuses([FromServices] DataContext context)
        {
            var a = context.Status.ToList();

            return FillArray(a, statuses);
        }


        /// <summary>
        /// Получить информацию о главной странице
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/GetMainPage")]
        [HttpPost]
        public IActionResult GetMainPage([FromServices] DataContext context)
        {
            var a = context.MainPageModels.FirstOrDefault();

            return ReturnObjectResult(a);
        }


        /// <summary>
        /// Проверить пользователя
        /// </summary>
        /// <returns></returns>
        [Route("/api/GetUser")]
        [HttpPost]
        public IActionResult GetUser([FromServices] DataContext context, [FromBody] Account account)
        {
            string password = "";

            var a = context.Accounts.FirstOrDefault(x => x.Login == account.Login);

            if (a != null)
            {
                password = HashPasswordHelper.HashPassword(account.Password + a.Salt);
            }

            var c = context.Accounts.FirstOrDefault(x => (x.Login == account.Login) && (x.Password == password));

            if (c != null)
                return Ok(new Account { IdRole = c.IdRole, Login = c.Login });
            else
                return BadRequest();
        }
        
    }


}
