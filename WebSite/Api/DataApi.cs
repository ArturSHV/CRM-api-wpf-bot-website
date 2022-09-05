using WebSite.Models;
using static ClassLibrary.GetApiData;

namespace WebSite.Api
{
    public class DataApi
    {

        #region Раздел Блогов
        /// <summary>
        /// Получить определенный блог
        /// </summary>
        /// <returns></returns>
        public object? GetSelectedBlog(int id)
        {
            string url = @"https://localhost:44369/api/GetBlogs/" + id;

            object? a = GetData(url);

            return a;
        }


        /// <summary>
        /// Получить все блоги
        /// </summary>
        /// <returns></returns>
        public object? GetBlogs()
        {
            string url = @"https://localhost:44369/api/GetBlogs";

            object? a = PostData(url);

            return a;
        }


        /// <summary>
        /// Отредактировать блог
        /// </summary>
        /// <param name="callBackModel"></param>
        /// <returns></returns>
        public string? EditBlog(Blogs blog)
        {
            string url = @"https://localhost:44369/api/EditBlog";

            string? request = SendPostData(url, blog);

            return request;
        }


        /// <summary>
        /// Удалить блог
        /// </summary>
        /// <param name="callBackModel"></param>
        /// <returns></returns>
        public string? DeleteBlog(Blogs blog)
        {
            string url = @"https://localhost:44369/api/DeleteBlog";

            string? request = SendPostData(url, blog);

            return request;
        }


        /// <summary>
        /// Добавить блог
        /// </summary>
        /// <param name="callBackModel"></param>
        /// <returns></returns>
        public string? AddBlog(Blogs blog)
        {
            string url = @"https://localhost:44369/api/AddBlog";

            string? request = SendPostData(url, blog);

            return request;
        }
        #endregion


        #region Раздел Услуг
        /// <summary>
        /// Получить список услуг
        /// </summary>
        /// <returns></returns>
        public object? GetServices()
        {
            string url = @"https://localhost:44369/api/GetServices";

            object? a = PostData(url);

            return a;

        }


        /// <summary>
        /// Отредактировать услугу
        /// </summary>
        /// <param name="callBackModel"></param>
        /// <returns></returns>
        public string? EditService(Services service)
        {
            string url = @"https://localhost:44369/api/EditService";

            string? request = SendPostData(url, service);

            return request;
        }


        /// <summary>
        /// Удалить блог
        /// </summary>
        /// <param name="callBackModel"></param>
        /// <returns></returns>
        public string? DeleteService(Services service)
        {
            string url = @"https://localhost:44369/api/DeleteService";

            string? request = SendPostData(url, service);

            return request;
        }


        /// <summary>
        /// Добавить блог
        /// </summary>
        /// <param name="callBackModel"></param>
        /// <returns></returns>
        public string? AddService(Services service)
        {
            string url = @"https://localhost:44369/api/AddService";

            string? request = SendPostData(url, service);

            return request;
        }
        #endregion


        #region Раздел проектов
        /// <summary>
        /// Получить список проектов
        /// </summary>
        /// <returns></returns>
        public object? GetProjects()
        {
            string url = @"https://localhost:44369/api/GetProjects";

            object? a = PostData(url);

            return a;
        }


        /// <summary>
        /// Получить определенный проект
        /// </summary>
        /// <returns></returns>
        public object? GetSelectedProject(int id)
        {
            string url = @"https://localhost:44369/api/GetProjects/" + id;

            object? a = GetData(url);

            return a;
        }


        /// <summary>
        /// Отредактировать проект
        /// </summary>
        /// <param name="callBackModel"></param>
        /// <returns></returns>
        public string? EditProject(Projects project)
        {
            string url = @"https://localhost:44369/api/EditProject";

            string? request = SendPostData(url, project);

            return request;
        }


        /// <summary>
        /// Удалить блог
        /// </summary>
        /// <param name="callBackModel"></param>
        /// <returns></returns>
        public string? DeleteProject(Projects project)
        {
            string url = @"https://localhost:44369/api/DeleteProject";

            string? request = SendPostData(url, project);

            return request;
        }


        /// <summary>
        /// Добавить блог
        /// </summary>
        /// <param name="callBackModel"></param>
        /// <returns></returns>
        public string? AddProject(Projects project)
        {
            string url = @"https://localhost:44369/api/AddProject";

            string? request = SendPostData(url, project);

            return request;
        }
        #endregion


        #region Раздел Контактов
        /// <summary>
        /// Получить контакты
        /// </summary>
        /// <returns></returns>
        public object? GetContacts()
        {
            string url = @"https://localhost:44369/api/GetContacts";

            object? a = PostData(url);

            return a;
        }


        /// <summary>
        /// Отредактировать контакты
        /// </summary>
        /// <param name="callBackModel"></param>
        /// <returns></returns>
        public string? EditContacts(Contacts contacts)
        {
            string url = @"https://localhost:44369/api/EditContacts";

            string? request = SendPostData(url, contacts);

            return request;
        }
        #endregion


        #region Раздел Статусов
        /// <summary>
        /// Получить статусы
        /// </summary>
        /// <param name="callBackModel"></param>
        /// <returns></returns>
        public object? GetStatuses()
        {
            string url = @"https://localhost:44369/api/GetStatuses";

            object? a = PostData(url);

            return a;
        }


        /// <summary>
        /// Отредактировать статус
        /// </summary>
        /// <param name="callBackModel"></param>
        /// <returns></returns>
        public string? EditStatus(int id, string status)
        {
            string url = @"https://localhost:44369/api/EditStatus";

            string? request = SendPostData(url, new MessageStatus { id = id, status = status });

            return request;
        }
        #endregion


        /// <summary>
        /// Оставить заявку, возвращает результат
        /// </summary>
        /// <param name="callBackModel"></param>
        /// <returns></returns>
        public string? CallBack(CallBackModel callBackModel)
        {
            string url = @"https://localhost:44369/api/SendRequest";

            string? request = SendPostData(url, callBackModel);

            return request;
        }


        /// <summary>
        /// Получить сообщения
        /// </summary>
        /// <returns></returns>
        public object? GetMessages()
        {
            string url = @"https://localhost:44369/api/GetMessages";

            object? a = PostData(url);

            return a;
        }


        /// <summary>
        /// Получить главную страницу
        /// </summary>
        /// <returns></returns>
        public object? GetMainPage()
        {
            string url = @"https://localhost:44369/api/GetMainPage";

            object? a = PostData(url);

            return a;
        }


        /// <summary>
        /// Отредактировать главную страницу
        /// </summary>
        /// <param name="callBackModel"></param>
        /// <returns></returns>
        public string? EditMainPage(MainPageModel mainPageModel)
        {
            string url = @"https://localhost:44369/api/EditMainPage";

            string? request = SendPostData(url, mainPageModel);

            return request;
        }


        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <returns></returns>
        public string? RegistrationUser(Account account)
        {
            string url = @"https://localhost:44369/api/RegistrationUser";

            string? request = SendPostData(url, account);

            return request;
        }


        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <returns></returns>
        public string? GetUser(Account account)
        {
            string url = @"https://localhost:44369/api/GetUser";

            string? request = SendPostData(url, account);

            return request;
        }

    }
}
