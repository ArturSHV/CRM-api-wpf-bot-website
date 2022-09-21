using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Models;

namespace TelegramBot
{
    class Program
    {
        static string textResponse { get; set; }
        static ReplyKeyboardMarkup replyKeyboardMarkup { get; set; }
        static string location { get; set; } //хранит в какой категории находится клиент

        static IEnumerable<ServicesModel> services { get; set; }
        static IEnumerable<ProjectsModel> projects { get; set; }
        static IEnumerable<BlogsModel> blogs { get; set; }

        static string errorMessage = "Ошибка запроса";

        static int adminChatId = 1887400468;



        public static void Main(string[] args)
        {
            var client = new TelegramBotClient("5760371563:AAE90o0_TYSnkNgYQJtkQnH-plnym2dU0yQ");

            client.StartReceiving(Update, Error);

            Console.ReadLine();
        }

        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            ResponseModel responseModel = new ResponseModel();

            var message = update.Message;
            if (message != null)
            {
                if (message.Text != null)
                {
                    Console.WriteLine($"{message.Chat.FirstName} : {message.Text} : {message.Chat.Id}");

                    responseModel = Response(update);

                    await botClient.SendTextMessageAsync(message.Chat.Id, responseModel.textResponse,  
                        replyMarkup: responseModel.replyKeyboardMarkup);

                    if (responseModel.textResponse.Contains("Ваше сообщение принято"))
                        await botClient.SendTextMessageAsync(adminChatId, $"Поступило сообщение: {message.Text} от {message.Chat.Username}");

                    return;
                }
                else
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Бот поддерживает только текст",
                           replyMarkup: new ReplyKeyboardMarkup("На главную") { ResizeKeyboard = true });
                    return;
                }    
            }
            #region MyRegion
            //if (message.Photo != null)
            //{
            //    await botClient.SendTextMessageAsync(message.Chat.Id, "получил фото");
            //    return;
            //}

            //if (message.Document != null)
            //{
            //    await botClient.SendTextMessageAsync(message.Chat.Id, "получил документ");
            //    var field = update.Message.Document.FileId;
            //    var fileInfo = await botClient.GetFileAsync(field);
            //    var filePath = fileInfo.FilePath;

            //    string destinationFilePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\{message.Document.FileName}";
            //    await using FileStream fileStream = System.IO.File.OpenWrite(destinationFilePath);
            //    await botClient.DownloadFileAsync(filePath, fileStream);
            //    fileStream.Close();


            //    return;
            //} 
            #endregion
        }

        /// <summary>
        /// метод обрабатывает входящие сообщения и возвращает результат
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        static ResponseModel Response(Update update)
        {
            string message = update.Message.Text;

            message = (message == "На главную" ? "/start" : message);
            message = (message == "Назад" ? location : message);

            ResponseModel responseModel = new ResponseModel() { textResponse = errorMessage, 
                replyKeyboardMarkup = new("На главную"){ ResizeKeyboard = true }};

            string[] categories = new string[] { "Оставить заявку", "Наши услуги", "Наш блог", "Наши проекты" };

            if (message != null)
            {
                switch (message.ToLower())
                {
                    case "/start":
                        replyKeyboardMarkup = new(
                        new[]
                        {
                        new KeyboardButton[] { categories[0]},
                        new KeyboardButton[] { categories[1]},
                        new KeyboardButton[] { categories[2]},
                        new KeyboardButton[] { categories[3]},
                        })
                        {
                            ResizeKeyboard = true
                        };
                        textResponse = "Привет. Ты находишься в чат-боте компании SkillProfi. " +
                            "Нажми любую кнопку снизу. ";
                        responseModel = new ResponseModel() { textResponse = textResponse, replyKeyboardMarkup = replyKeyboardMarkup };
                        break;


                    case "оставить заявку":
                        replyKeyboardMarkup = new(
                        new[]{ new KeyboardButton[] { "На главную"} }){ ResizeKeyboard = true };

                        textResponse = "Введите ваше сообщение: ";
                        responseModel = new ResponseModel() { textResponse = textResponse, replyKeyboardMarkup = replyKeyboardMarkup };
                        location = message;
                        break;


                    case "наши услуги":

                        Controller<ServicesModel> serviceController = new Controller<ServicesModel>();

                        services = serviceController.GetModel();

                        var buttonsService = serviceController.GetButtons(services);

                        replyKeyboardMarkup = new(buttonsService){ ResizeKeyboard = true };

                        textResponse = $"{message}: ";
                        responseModel = new ResponseModel() { textResponse = textResponse, replyKeyboardMarkup = replyKeyboardMarkup };
                        location = message;
                        break;


                    case "наши проекты":

                        Controller<ProjectsModel> projectsController = new Controller<ProjectsModel>();

                        projects = projectsController.GetModel();

                        var buttonsProjects = projectsController.GetButtons(projects);

                        replyKeyboardMarkup = new(buttonsProjects) { ResizeKeyboard = true };

                        textResponse = $"{message}: ";
                        responseModel = new ResponseModel() { textResponse = textResponse, replyKeyboardMarkup = replyKeyboardMarkup };
                        location = message;
                        break;


                    case "наш блог":

                        Controller<BlogsModel> blogsController = new Controller<BlogsModel>();

                        blogs = blogsController.GetModel();

                        var buttonsBlogs = blogsController.GetButtons(blogs);

                        replyKeyboardMarkup = new(buttonsBlogs) { ResizeKeyboard = true };

                        textResponse = $"{message}: ";
                        responseModel = new ResponseModel() { textResponse = textResponse, replyKeyboardMarkup = replyKeyboardMarkup };
                        location = message;
                        break;


                    default:

                        //Оставить заявку
                        if (location == categories[0])
                        {
                            Controller<CallBackModel> callBackController = new Controller<CallBackModel>();

                            CallBackModel callBackModel = new CallBackModel() { email = $"{update.Message.Chat.Username}[telegramBot]", 
                                name = update.Message.Chat.Username, text = message};

                            var st = callBackController.AddData(callBackModel);

                            replyKeyboardMarkup = new(
                            new[] { new KeyboardButton[] { "На главную" } }){ ResizeKeyboard = true };

                            textResponse = (st != null ? "Ваше сообщение принято! С вами свяжется наш менеджер." : "Ошибка соединения с сервером");
                            location = "";
                            responseModel = new ResponseModel() { textResponse = textResponse, replyKeyboardMarkup = replyKeyboardMarkup };
                        }

                        //Наши услуги
                        else if (location == categories[1])
                        {
                            replyKeyboardMarkup = new(
                            new[]
                            {
                            new KeyboardButton[] { "Назад" }
                            })
                            {
                                ResizeKeyboard = true
                            };

                            textResponse = (services.FirstOrDefault(x => x.Title == message) != null ?
                                services.FirstOrDefault(x => x.Title == message).Description : errorMessage);

                            textResponse = Regex.Replace(textResponse, "<.*?>", String.Empty);

                            responseModel = new ResponseModel() { textResponse = textResponse, replyKeyboardMarkup = replyKeyboardMarkup };
                        }

                        //Наши блоги
                        else if (location == categories[2])
                        {
                            replyKeyboardMarkup = new(
                            new[]
                            {
                            new KeyboardButton[] { "Назад"}
                            })
                            {
                                ResizeKeyboard = true
                            };


                            textResponse = (blogs.FirstOrDefault(x => x.Title == message) != null ? 
                                blogs.FirstOrDefault(x => x.Title == message).Description : errorMessage);

                            textResponse = Regex.Replace(textResponse, "<.*?>", String.Empty);

                            responseModel = new ResponseModel() { textResponse = textResponse, replyKeyboardMarkup = replyKeyboardMarkup };
                        }

                        //Наши проекты
                        else if (location == categories[3])
                        {
                            replyKeyboardMarkup = new(
                            new[]
                            {
                            new KeyboardButton[] { "Назад" }
                            })
                            {
                                ResizeKeyboard = true
                            };

                            textResponse = (projects.FirstOrDefault(x => x.Title == message) != null ?
                                projects.FirstOrDefault(x => x.Title == message).Description : errorMessage);

                            textResponse = Regex.Replace(textResponse, "<.*?>", String.Empty);

                            responseModel = new ResponseModel() { textResponse = textResponse, replyKeyboardMarkup = replyKeyboardMarkup };
                        }
                        else
                        {
                            replyKeyboardMarkup = new(
                            new[]
                            {
                            new KeyboardButton[] { "На главную"}
                            })
                            {
                                ResizeKeyboard = true
                            };
                            textResponse = errorMessage;
                            responseModel = new ResponseModel() { textResponse = textResponse, replyKeyboardMarkup = replyKeyboardMarkup };
                        }
                        break;
                }
            }
            return responseModel;
        }


        async static Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken token)
        {
            Console.WriteLine(exception.Message);
        }
    }
}




