using Newtonsoft.Json;
using RestSharp;


namespace ClassLibrary
{
    public static class GetApiData
    {

        /// <summary>
        /// получить данные
        /// </summary>
        /// <param name="request"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        private static object GetData(this RestRequest request, string url)
        {
            string stream = GetStream(request, url);
            if (!string.IsNullOrEmpty(stream))
            {
                object data = JsonConvert.DeserializeObject(stream);

                return data;
            }
            else
                return null;

        }


        /// <summary>
        /// Получить поток stream
        /// </summary>
        /// <param name="request"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string GetStream(this RestRequest request, string url)
        {
            RestClient client = new RestClient(url);

            var response = client.Execute(request);

            string stream = response.Content;

            return stream;
        }


        /// <summary>
        /// Отправка запроса. Возвращает Request
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static RestRequest SendRequest(string url, Method method)
        {
            RestRequest request = new RestRequest(url, method);

            //request.AddHeader("Content-Type", "multipart/form-data");

            return request;
        }


        /// <summary>
        /// Получить данные Post запроса
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static object PostData(string url)
        {
            var a = SendRequest(url, Method.Post).GetData(url);
            return a;
        }


        /// <summary>
        /// Получить данные Get запроса
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static object GetData(string url)
        {
            var a = SendRequest(url, Method.Get).GetData(url);
            return a;
        }


        /// <summary>
        /// Отправить данные Post методом
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="model">модель класса</param>
        /// <returns></returns>
        public static string SendPostData<T>(string url, T model)
        {
            RestRequest request = SendRequest(url, Method.Post);

            string c = JsonConvert.SerializeObject(model);

            request.AddBody(c);

            string a = request.GetStream(url);

            return a;
        }

    }
}