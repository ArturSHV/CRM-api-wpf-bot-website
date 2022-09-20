using Newtonsoft.Json;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Models;
using static ClassLibrary.GetApiData;

namespace TelegramBot
{
    public class Controller<Model>
        where Model : IModel, new()
    {
        public IEnumerable<Model> model { get; set; }
        public List<KeyboardButton[]> buttons = new List<KeyboardButton[]>();

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
        /// создание кнопок для чата телеграм
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public List<KeyboardButton[]> GetButtons(IEnumerable<Model> models)
        {
            foreach (var item in models)
            {
                buttons.Add(new KeyboardButton[] { item.Title });
            }
            buttons.Add(new KeyboardButton[] {"На главную"});
            return buttons;
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


    }
}
