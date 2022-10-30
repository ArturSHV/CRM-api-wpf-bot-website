using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Blog 
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
