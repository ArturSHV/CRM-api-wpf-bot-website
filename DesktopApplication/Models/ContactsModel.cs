namespace DesktopApplication.Models
{
    public class ContactsModel : IModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MapScript { get; set; }

        public string UrlGet { get => "GetContacts"; } 

        public string UrlEdit { get => "EditContacts"; }

        public string UrlDelete => null;

        public string UrlAdd => null;
    }
}
