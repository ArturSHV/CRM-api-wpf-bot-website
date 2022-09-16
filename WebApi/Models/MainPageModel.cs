using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    [Keyless]
    public class MainPageModel
    {
        public string Placeholder { get; set; }
        public string Title { get; set; }
        public string ButtonText { get; set; }
    }
}
