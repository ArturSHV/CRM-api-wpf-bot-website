using DesktopApplication.Api;
using DesktopApplication.Helpers;
using DesktopApplication.Models;
using PropertyChanged;
using System.Collections.Generic;



namespace DesktopApplication.Controllers
{
    [AddINotifyPropertyChangedInterface]
    public class Controller<Model> 
        where Model : IModel, new()
    {
        public IEnumerable<Model> model { get; set; }

        IModel classModel = new Model();

        DataApi dataApi = new DataApi();

        MessageCreate messageCreate = new MessageCreate();


        /// <summary>
        /// Получение данных
        /// </summary>
        public IEnumerable<Model> GetModel()
        {
            var a = dataApi.GetModel(classModel.UrlGet);

            model = messageCreate.Response(a, messageCreate.ReturnListModelInView<Model>(a));

            return model;
        }


        /// <summary>
        /// Отредактировать данные
        /// </summary>
        /// <param name="model"></param>
        public string EditData(Model model)
        {
            string request = dataApi.EditModel(new {token = Token.token, model = model }, classModel.UrlEdit);

            var message = messageCreate.ReturnMessage(request);

            return message;
        }


        /// <summary>
        /// Добавить данные
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AddData(Model model)
        {
            string request = dataApi.AddModel(new {token = Token.token, model = model}, classModel.UrlAdd);

            var message = messageCreate.ReturnMessage(request);

            return message;
        }


        /// <summary>
        /// Удалить данные
        /// </summary>
        /// <param name="model"></param>
        public string DeleteData(Model model)
        {
            string request = dataApi.DeleteModel(new {token = Token.token, model = model.Id}, classModel.UrlDelete);

            var message = messageCreate.ReturnMessage(request);

            return message;
        }
    }

}
