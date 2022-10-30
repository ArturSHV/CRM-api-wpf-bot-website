using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class MessageStatus
    {
        public int Id { get; set; }
        public DateTime date { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string Contact { get; set; }
        public string Status { get; set; }
    }
}
