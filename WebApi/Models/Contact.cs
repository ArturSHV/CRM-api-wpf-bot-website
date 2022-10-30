using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string MapScript { get; set; }
    }
}
