using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Api;
using TelegramBot.Helpers;
using TelegramBot.Models;

namespace TelegramBot
{
    public class Controller<Model>
        where Model : IModel, new()
    {
        public IEnumerable<Model> model { get; set; }
        public List<KeyboardButton[]> buttons = new List<KeyboardButton[]>();

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
        /// создание кнопок для чата телеграм
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public List<KeyboardButton[]> GetButtons(IEnumerable<Model> models)
        {
            if (models != null)
            {
                foreach (var item in models)
                {
                    buttons.Add(new KeyboardButton[] { item.Title });
                }
            }

            buttons.Add(new KeyboardButton[] { "На главную" });

            return buttons;
        }


        /// <summary>
        /// Добавить данные
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AddData(Model model)
        {
            string request = dataApi.AddModel(new { model = model }, classModel.UrlAdd);

            var message = messageCreate.ReturnMessage(request);

            return message;
        }


    }
}
