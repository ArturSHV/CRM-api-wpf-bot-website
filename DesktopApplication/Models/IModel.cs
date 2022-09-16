namespace DesktopApplication.Models
{
    public interface IModel
    {
        /// <summary>
        /// Ссылка апи на получение данных
        /// </summary>
        string UrlGet { get; }

        /// <summary>
        /// Ссылка апи на редактирование данных
        /// </summary>
        string UrlEdit { get; }

        /// <summary>
        /// Ссылка апи на удаление данных
        /// </summary>
        string UrlDelete { get; }

        /// <summary>
        /// Ссылка апи на добавление данных
        /// </summary>
        string UrlAdd { get; }
    }
}
