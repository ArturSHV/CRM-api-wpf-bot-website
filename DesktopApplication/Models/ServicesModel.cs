namespace DesktopApplication.Models
{
    public class ServicesModel : IModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string UrlGet { get => "https://localhost:44369/api/GetServices"; }

        public string UrlEdit { get => "https://localhost:44369/api/EditService"; }

        public string UrlDelete { get => "https://localhost:44369/api/DeleteService"; }

        public string UrlAdd { get => "https://localhost:44369/api/AddService"; }
    }
}
