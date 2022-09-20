namespace TelegramBot.Models
{
    public class ServicesModel : IModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string UrlGet { get => "https://localhost:44369/api/GetServices"; }

        public string UrlAdd => null;
    }
}
