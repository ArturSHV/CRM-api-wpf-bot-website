using DesktopApplication.Models;
using Newtonsoft.Json;
using PropertyChanged;
using System.Collections.Generic;
using static ClassLibrary.GetApiData;

namespace DesktopApplication.Controllers
{
    [AddINotifyPropertyChangedInterface]
    public class Controller<Model> 
        where Model : IModel, new()
    {
        public IEnumerable<Model> model { get; set; }

        IModel classModel = new Model();


        /// <summary>
        /// Получение данных
        /// </summary>
        public IEnumerable<Model> GetModel()
        {
            object a = PostData(classModel.UrlGet);

            if (a != null)
            {
                var oldString = a.ToString();
                if (a.ToString().IndexOf('[') == -1)
                {
                    oldString = oldString.Insert(0, "[");
                    oldString = oldString.Insert(oldString.Length, "]");
                }

                model = JsonConvert.DeserializeObject<IEnumerable<Model>>(oldString);
            }
            return model;
        }


        /// <summary>
        /// Отредактировать данные
        /// </summary>
        /// <param name="model"></param>
        public string EditData(Model model)
        {
            string request = SendPostData(classModel.UrlEdit, model);

            return request; //null при отсутствии соединения
        }


        /// <summary>
        /// Добавить данные
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AddData(Model model)
        {
            string request = SendPostData(classModel.UrlAdd, model);

            return request;
        }


        /// <summary>
        /// Удалить данные
        /// </summary>
        /// <param name="model"></param>
        public string DeleteData(Model model)
        {
            string request = SendPostData(classModel.UrlDelete, model);

            return request;
        }
    }

}
