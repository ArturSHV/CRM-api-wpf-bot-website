namespace WebApi.Models
{
    public class AddNewRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Text { get; set; }
    }
}
