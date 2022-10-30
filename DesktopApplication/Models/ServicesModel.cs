namespace DesktopApplication.Models
{
    public class ServicesModel : IModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string UrlGet { get => "GetServices"; }

        public string UrlEdit { get => "EditService"; }

        public string UrlDelete { get => "DeleteService"; }

        public string UrlAdd { get => "AddService"; }
    }
}
