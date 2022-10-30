using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    
    public class Message
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string Email { get; set; }
        public string Status { get; set; }
    }
}
