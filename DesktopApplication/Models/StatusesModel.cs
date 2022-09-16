namespace DesktopApplication.Models
{
    public class StatusesModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string urlGetStatuses { get => "https://localhost:44369/api/GetStatuses"; }
    }
}
