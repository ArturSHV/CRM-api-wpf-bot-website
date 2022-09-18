using DesktopApplication.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using static ClassLibrary.GetApiData;

namespace DesktopApplication.Controllers
{
    [AddINotifyPropertyChangedInterface]
    public class Controller<Model> 
        where Model : IModel, new()
    {
        public ObservableCollection<Model> forModel { get; set; }
        public IEnumerable<Model> model { get; set; }

        IModel classModel = new Model();


        /// <summary>
        /// Получение данных
        /// </summary>
        public ObservableCollection<Model> GetModel()
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

                forModel = JsonConvert.DeserializeObject<ObservableCollection<Model>>(oldString);

                model = forModel;
            }
            return forModel;
        }


        /// <summary>
        /// Отредактировать данные
        /// </summary>
        /// <param name="model"></param>
        public string EditData(Model model)
        {
            string request = SendPostData(classModel.UrlEdit, model);

            return request;
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
