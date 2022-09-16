namespace DesktopApplication.Models
{

    public class ProjectsModel : IModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        public string UrlGet { get => "https://localhost:44369/api/GetProjects"; }

        public string UrlEdit { get => "https://localhost:44369/api/EditProject"; }

        public string UrlDelete { get => "https://localhost:44369/api/DeleteProject"; }

        public string UrlAdd { get => "https://localhost:44369/api/AddProject"; }
    }
}
