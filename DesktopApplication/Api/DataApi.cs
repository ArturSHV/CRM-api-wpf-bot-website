using DesktopApplication.Models;
using static DesktopApplication.Helpers.GetApiData;

namespace DesktopApplication.Api
{
    public class DataApi
    {
        /// <summary>
        /// Получить токен
        /// </summary>
        /// <returns></returns>
        public string GetToken(Account account)
        {
            string url = $@"https://localhost:44369/api/Authorize/";

            var a = SendPostData(url, account);

            return a;
        }


        /// <summary>
        /// Отредактировать блог
        /// </summary>
        /// <param name="callBackModel"></param>
        /// <returns></returns>
        public string EditModel<T>(T model, string methodName)
        {
            string url = $@"https://localhost:44369/api/{methodName}";

            var request = SendPostData(url, model);

            return request;
        }


        /// <summary>
        /// Удалить блог
        /// </summary>
        /// <param name="callBackModel"></param>
        /// <returns></returns>
        public string DeleteModel<T>(T model, string methodName)
        {
            string url = $@"https://localhost:44369/api/{methodName}";

            var request = SendPostData(url, model);

            return request;
        }


        /// <summary>
        /// Добавить блог
        /// </summary>
        /// <param name="callBackModel"></param>
        /// <returns></returns>
        public string AddModel<T>(T model, string methodName)
        {
            string url = $@"https://localhost:44369/api/{methodName}";

            var request = SendPostData(url, model);

            return request;
        }


        /// <summary>
        /// Получить список проектов
        /// </summary>
        /// <returns></returns>
        public string GetModel(string methodName)
        {
            string url = $@"https://localhost:44369/api/{methodName}";

            var a = PostData(url);

            return a;
        }

        /// <summary>
        /// Получить данные при помощи токена
        /// </summary>
        /// <param name="callBackModel"></param>
        /// <returns></returns>
        public string GetModel<T>(T model, string methodName)
        {
            string url = $@"https://localhost:44369/api/{methodName}";

            var request = SendPostData(url, model);

            return request;
        }


        /// <summary>
        /// Получить определенный проект
        /// </summary>
        /// <returns></returns>
        public string GetSelectedModel(string methodName, int id)
        {
            string url = $@"https://localhost:44369/api/{methodName}/{id}";

            var a = GetData(url);

            return a;
        }


    }
}
