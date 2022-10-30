using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Account
    {
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
        public string Salt { get; set; }
        public int IdRole { get; set; }
    }
}
