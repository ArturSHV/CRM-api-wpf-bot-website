namespace WebSite.Models
{
    public class MessageWithStatus
    {
        public List<MessageStatus> messageStatus { get; set; }
        public List<Statuses> statuses { get; set; }
    }
}
