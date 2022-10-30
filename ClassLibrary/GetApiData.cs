using Newtonsoft.Json;
using RestSharp;


namespace ClassLibrary
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
            RestClient client = new (url);

            var response = client.Execute(request);

            string? stream = response.Content;

            return stream;
        }


        /// <summary>
        /// Получить данные Post запроса
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static object? PostData(string url)
        {
            RestRequest request = new(url, Method.Post);

            var a = request.GetStream(url);

            return a;
        }


        /// <summary>
        /// Получить данные Get запроса
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static object? GetData(string url)
        {
            RestRequest request = new(url, Method.Get);

            var a = request.GetStream(url);

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