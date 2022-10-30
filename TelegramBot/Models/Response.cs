namespace TelegramBot.Models
{
    public class Response
    {
        public bool ok { get; set; }
        public int status_code { get; set; }
        public object result { get; set; }
    }
}
