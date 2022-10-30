using System;

namespace DesktopApplication.Models
{

    public class MessagesModel
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public string name { get; set; }
        public string text { get; set; }
        public string contact { get; set; }
        public string status { get; set; }


        public string urlGetMessages { get => "GetMessages"; }
        public string urlEditMessage { get => "EditStatus"; }


        
    }
}
