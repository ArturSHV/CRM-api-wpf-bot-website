using System.ComponentModel.DataAnnotations;

namespace WebSite.Models
{

    
    public class CallBackModel
    {
        [Required(ErrorMessage = "Введите Имя")]
        public string name { get; set; } 

        [Required(ErrorMessage = "Введите Email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Введите Сообщение")]
        public string text { get; set; }
    }
}
