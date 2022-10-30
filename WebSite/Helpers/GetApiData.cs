using Newtonsoft.Json;
using RestSharp;


namespace WebSite.Helpers
{
    public static class GetApiData
    {
        /// <summary>
        /// Получить поток stream
        /// </summary>
        /// <param name="request"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string? GetStream(this RestRequest request, string url)
        {
            RestClient client = new(url);

            var response = client.Execute(request);

            string? stream = response.Content;

            return stream;
        }


        /// <summary>
        /// Получить данные Post запроса
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string? PostData(string url)
        {
            RestRequest request = new(url, Method.Post);

            string? a = request.GetStream(url);

            return a;
        }


        /// <summary>
        /// Получить данные Get запроса
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string? GetData(string url)
        {
            RestRequest request = new(url, Method.Get);

            string? a = request.GetStream(url);

            return a;
        }


        /// <summary>
        /// Отправить данные Post методом
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="model">модель класса</param>
        /// <returns></returns>
        public static string? SendPostData<T>(string url, T model)
        {
            RestRequest request = new(url, Method.Post);

            string c = JsonConvert.SerializeObject(model);

            request.AddBody(c);

            string? a = request.GetStream(url);

            return a;
        }

    }
}
