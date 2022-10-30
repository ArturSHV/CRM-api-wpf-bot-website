using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Statuses
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
