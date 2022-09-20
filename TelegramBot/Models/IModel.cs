namespace TelegramBot.Models
{
    public interface IModel
    {
        string Title { get; set; }
        /// <summary>
        /// Ссылка апи на получение данных
        /// </summary>
        string UrlGet { get; }


        /// <summary>
        /// Ссылка апи на добавление данных
        /// </summary>
        string UrlAdd { get; }
    }
}
