using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class AddNewRequest
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
