namespace WebSite.Models
{
    public class Contacts : IModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MapScript { get; set; }
    }
}
