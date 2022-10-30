using System.ComponentModel.DataAnnotations;

namespace TelegramBot.Models
{

    
    public class CallBackModel : IModel
    {
        public string name { get; set; } 

        public string email { get; set; }

        public string text { get; set; }

        public string Title { get; set; }

        public string UrlGet => null;

        public string UrlAdd => "SendRequest";
    }
}
