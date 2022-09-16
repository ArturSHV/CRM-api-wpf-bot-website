namespace DesktopApplication.Models
{
    public class ContactsModel : IModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MapScript { get; set; }

        public string UrlGet => @"https://localhost:44369/api/GetContacts";

        public string UrlEdit => @"https://localhost:44369/api/EditContacts";

        public string UrlDelete => null;

        public string UrlAdd => null;
    }
}
