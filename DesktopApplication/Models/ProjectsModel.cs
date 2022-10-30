namespace DesktopApplication.Models
{

    public class ProjectsModel : IModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        public string UrlGet { get => "GetProjects"; }

        public string UrlEdit { get => "EditProject"; }

        public string UrlDelete { get => "DeleteProject"; }

        public string UrlAdd { get => "AddProject"; }
    }
}
