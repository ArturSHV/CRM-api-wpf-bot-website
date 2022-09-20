using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApi.Data;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class AddNewInformationController : Controller
    {
        IWebHostEnvironment _appEnvironment;

        public AddNewInformationController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
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
        /// Добавить в бд запрос клиента на обрутную связь. Возвращает результат в теле ответа
        /// </summary>
        /// <param name="json"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/SendRequest")]
        [HttpPost]
        public async Task<IActionResult> SendRequest([FromBody] AddNewRequest json, [FromServices] DataContext context)
        {
            try
            {
                await context.Messages.AddAsync(new Message { Date = DateTime.Now, Text = json.Text, Email = json.Email, Status = "Получена" });

                if (context.Users.FirstOrDefault(x=>x.Email == json.Email) == null)
                    await context.Users.AddAsync(new User { Email = json.Email, Name = json.Name });

                await context.SaveChangesAsync();

                return Ok("Запрос успешно отправлен");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// Создание пути к картинке для записи в БД
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private string NewPath(string filePath)
        {
            int index = filePath.LastIndexOf(@"\");

            string imageFoulder = "https://localhost:44369/img/";

            string directoryPath = imageFoulder + filePath[(index + 1)..];

            return directoryPath;
        }


        /// <summary>
        /// Скачивание файла в папку Api. Возврашает путь к файлу
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        private async Task<string> GetFile(string uploadedFile) 
        {
            
            string directoryPath = Path.Combine(_appEnvironment.ContentRootPath, "wwwroot/img"); 

            try
            {
                var indexOfExtension = uploadedFile.LastIndexOf('.');

                string extension = uploadedFile[(indexOfExtension + 1)..];

                if ((extension == "png") || (extension == "jpg") || (extension == "jpeg"))

                {
                    string newNameImage = HashPasswordHelper.RandomString(30) + "." + extension;

                    string filePath = Path.Combine(directoryPath, newNameImage);

                    await Task.Run(() => {
                        WebClient webClient = new WebClient();
                        webClient.DownloadFile(uploadedFile, filePath);
                    });

                    directoryPath = NewPath(filePath);

                    return directoryPath;
                }

                else
                    return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            

            
        }


        #region Раздел Блогов
        /// <summary>
        /// Изменить блог
        /// </summary>
        /// <param name="json"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/EditBlog")]
        [HttpPost]
        public async Task<IActionResult> EditBlog([FromBody] Blog blog, [FromServices] DataContext context)
        {
            string? directoryPath = blog.Image;

            var checkImage = context.Blogs.FirstOrDefault(x => x.Image == blog.Image);
            if (checkImage == null)
            {
                directoryPath = await GetFile(blog.Image);
            }

            try
            {
                Blog? a = context.Blogs.FirstOrDefault(x => x.Id == blog.Id);

                if (a != null)
                {
                    a.Title = blog.Title;
                    a.Image = directoryPath;
                    a.Description = blog.Description;
                    a.CreateDate = blog.CreateDate;
                    await context.SaveChangesAsync();

                    return Ok("Блог успешно изменен");
                }
                else
                    return BadRequest($"Блога с id={blog.Id} не существует");
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Удалить блог
        /// </summary>
        /// <param name="json"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/DeleteBlog")]
        [HttpPost]
        public async Task<IActionResult> DeleteBlog([FromBody] Blog blog, [FromServices] DataContext context)
        {
            try
            {
                Blog? a = context.Blogs.FirstOrDefault(x => x.Id == blog.Id);

                if (a != null)
                {
                    context.Blogs.Remove(a);

                    await context.SaveChangesAsync();

                    return Ok("Блог успешно удален");
                }
                else
                    return BadRequest($"Блога с id={blog.Id} не существует");
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Добавить блог
        /// </summary>
        /// <param name="json"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/AddBlog")]
        [HttpPost]
        public async Task<IActionResult> AddBlog([FromBody] Blog blog, [FromServices] DataContext context)
        {
            string directoryPath = await GetFile(blog.Image);
            blog.Image = directoryPath;

            try
            {
                await context.Blogs.AddAsync(blog);

                await context.SaveChangesAsync();

                return Ok("Блог успешно добавлен");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        #endregion


        #region Раздел Проектов
        /// <summary>
        /// Изменить проекты
        /// </summary>
        /// <param name="json"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/EditProject")]
        [HttpPost]
        public async Task<IActionResult> EditProject([FromBody] Project project, [FromServices] DataContext context)
        {
            string directoryPath = project.Image;

            var checkImage = context.Projects.FirstOrDefault(x => x.Image == project.Image);
            if (checkImage == null)
            {
                directoryPath = await GetFile(project.Image);
            }

            try
            {
                Project? a = context.Projects.FirstOrDefault(x => x.Id == project.Id);

                if (a != null)
                {
                    a.Title = project.Title;
                    a.Image = directoryPath;
                    a.Description = project.Description;

                    await context.SaveChangesAsync();

                    return Ok("Проект успешно отредактирован");
                }
                else
                    return BadRequest($"Проекта с id={project.Id} не существует");
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Удалить Проект
        /// </summary>
        /// <param name="json"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/DeleteProject")]
        [HttpPost]
        public async Task<IActionResult> DeleteProject([FromBody] Project project, [FromServices] DataContext context)
        {
            try
            {
                Project? a = context.Projects.FirstOrDefault(x => x.Id == project.Id);

                if (a != null)
                {
                    context.Projects.Remove(a);

                    await context.SaveChangesAsync();

                    return Ok("Проект успешно удален");
                }
                else
                    return BadRequest($"Проект с id={project.Id} не существует");
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Добавить Проект
        /// </summary>
        /// <param name="json"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/AddProject")]
        [HttpPost]
        public async Task<IActionResult> AddProject([FromBody] Project project, [FromServices] DataContext context)
        {
            string directoryPath = await GetFile(project.Image);

            project.Image = directoryPath;

            try
            {
                await context.Projects.AddAsync(project);

                await context.SaveChangesAsync();

                return Ok("Проект успешно добавлен");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        #endregion


        #region Раздел Услуг
        /// <summary>
        /// Изменить услуги
        /// </summary>
        /// <param name="json"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/EditService")]
        [HttpPost]
        public async Task<IActionResult> EditService([FromBody] Service service, [FromServices] DataContext context)
        {
            try
            {
                Service? a = context.Services.FirstOrDefault(x => x.Id == service.Id);

                if (a != null)
                {
                    a.Title = service.Title;
                    a.Description = service.Description;

                    await context.SaveChangesAsync();

                    return Ok("Услуга успешно отредактирована");
                }
                else
                    return BadRequest($"Услуги с id={service.Id} не существует");
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Удалить услугу
        /// </summary>
        /// <param name="json"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/DeleteService")]
        [HttpPost]
        public async Task<IActionResult> DeleteService([FromBody] Service service, [FromServices] DataContext context)
        {
            try
            {
                Service? a = context.Services.FirstOrDefault(x => x.Id == service.Id);

                if (a != null)
                {
                    context.Services.Remove(a);

                    await context.SaveChangesAsync();

                    return Ok("Услуга успешно удалена");
                }
                else
                    return BadRequest($"Услуга с id={service.Id} не существует");
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Добавить услугу
        /// </summary>
        /// <param name="json"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/AddService")]
        [HttpPost]
        public async Task<IActionResult> AddService([FromBody] Service service, [FromServices] DataContext context)
        {
            try
            {
                await context.Services.AddAsync(service);

                await context.SaveChangesAsync();

                return Ok("Услуга успешно добавлена");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        } 
        #endregion


        /// <summary>
        /// Изменить контакты
        /// </summary>
        /// <param name="json"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/EditContacts")]
        [HttpPost]
        public async Task<IActionResult> EditContacts([FromBody] Contact contact, [FromServices] DataContext context)
        {
            try
            {
                Contact? a = context.Contacts.FirstOrDefault();

                if (a != null)
                {
                    a.Title = contact.Title;
                    a.Description = contact.Description;

                    await context.SaveChangesAsync();

                    return Ok("Контакты успешно отредактированы");
                }
                else
                    return BadRequest($"Контакты не существуют");
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// Изменить статус
        /// </summary>
        /// <param name="json"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/EditStatus")]
        [HttpPost]
        public async Task<IActionResult> EditStatus([FromBody] MessageStatus messageStatus, [FromServices] DataContext context)
        {
            try
            {
                Message? a = context.Messages.FirstOrDefault(x=>x.Id== messageStatus.Id);

                if (a != null)
                {
                    a.Status = messageStatus.Status;

                    await context.SaveChangesAsync();

                    return Ok("Статус успешно отредактирован");
                }
                else
                    return BadRequest($"Сообщения с id={messageStatus.Id} не существует");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// Изменить главную страницу
        /// </summary>
        /// <param name="json"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("/api/EditMainPage")]
        [HttpPost]
        public async Task<IActionResult> EditMainPage([FromBody] MainPageModel mainPageModel, [FromServices] DataContext context)
        {
            try
            {
                MainPageModel? a = context.MainPageModels.FirstOrDefault();

                if (a != null)
                {
                    a.Title = mainPageModel.Title;
                    a.Placeholder = mainPageModel.Placeholder;
                    a.ButtonText = mainPageModel.ButtonText;

                    await context.SaveChangesAsync();

                    return Ok("Главная страница успешно отредактирована");
                }
                else
                    return BadRequest($"Информации о главной странице не существует");
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="context"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        /// 
        [Route("/api/RegistrationUser")]
        [HttpPost]
        public async Task<IActionResult> RegistrationUser([FromServices] DataContext context, [FromBody] Account account)
        {
            Account? a = context.Accounts.FirstOrDefault(x => x.Login == account.Login);

            if (a == null)
            {
                string salt = HashPasswordHelper.RandomString(10);

                string password = HashPasswordHelper.HashPassword(account.Password + salt);

                Account newAccount = new() { Login = account.Login, Password = password, IdRole = 1, Salt = salt };

                await context.Accounts.AddAsync(newAccount);

                await context.SaveChangesAsync();

                return Ok("Аккаунт успешно зарегистрирован");
            }
            else
                return BadRequest();
        }

    }
}
