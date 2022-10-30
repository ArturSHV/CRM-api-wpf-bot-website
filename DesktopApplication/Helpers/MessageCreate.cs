using DesktopApplication.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApplication.Helpers
{
    public class MessageCreate
    {

        /// <summary>
        /// Преобразует ответ с сервера в модель Response
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="responseFromServer"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public T Response<T>(string responseFromServer, T action)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<Response>(responseFromServer);

                if (response?.ok == true)
                {
                    return action;
                }
                
            }
            catch
            {
               
            }

            return default(T);
        }


        /// <summary>
        /// Преобразует json  ответ с сервера в модель
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="responseFromServer"></param>
        /// <returns></returns>
        public ObservableCollection<T> ReturnListModelInView<T>(string responseFromServer)
        {
            try
            {
                var modelString = JObject.Parse(responseFromServer)["result"]?["items"]?.ToString();

                try
                {
                    var model = JsonConvert.DeserializeObject<ObservableCollection<T>>(modelString);
                    return model;
                }
                catch
                {
                    var model = JsonConvert.DeserializeObject<T>(modelString);
                    return new ObservableCollection<T>{model};
                }
            }
            catch 
            {
                return null;
            }

            
        }

        /// <summary>
        /// Возвращает сообщение с сервера о результате операции
        /// </summary>
        /// <param name="responseFromServer"></param>
        /// <returns></returns>
        public string ReturnMessage(string responseFromServer)
        {
            try
            {
                var message = JObject.Parse(responseFromServer)["result"]?["description"]?.Value<string>();

                return message;
            }
            catch
            {
                return "Нет соединения с сервером";
            }


        }
    }
}
