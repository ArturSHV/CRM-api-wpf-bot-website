using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    
    public class MainPageModel
    {
        public int Id { get; set; }

        [Required]
        public string Placeholder { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ButtonText { get; set; }
    }
}
