using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

    }
}
