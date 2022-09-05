namespace WebSite.Models
{
    public class Authorize
    {
        public string Login { get; set; }
        public int Role { get; set; }
        public string Host { get; set; }
        public string IP { get; set; }
        public string UserAgent { get; set; }
    }
}
