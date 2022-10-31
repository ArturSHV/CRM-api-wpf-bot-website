using TelegramBot.Helpers;
using static TelegramBot.Helpers.GetApiData;


namespace TelegramBot.Api
{
    public class DataApi
    {
        
        /// <summary>
        /// Добавить блог
        /// </summary>
        /// <param name="callBackModel"></param>
        /// <returns></returns>
        public string AddModel<T>(T model, string methodName)
        {
            string url = $"{SettingsHelpers.ReturnHostString()}api/{methodName}";

            var request = SendPostData(url, model);

            return request;
        }


        /// <summary>
        /// Получить список проектов
        /// </summary>
        /// <returns></returns>
        public string GetModel(string methodName)
        {
            string url = $"{SettingsHelpers.ReturnHostString()}api/{methodName}";

            var a = PostData(url);

            return a;
        }

        /// <summary>
        /// Получить определенный проект
        /// </summary>
        /// <returns></returns>
        public string GetSelectedModel(string methodName, int id)
        {
            string url = $"{SettingsHelpers.ReturnHostString()}api/{methodName}/{id}";

            var a = GetData(url);

            return a;
        }


    }
}
