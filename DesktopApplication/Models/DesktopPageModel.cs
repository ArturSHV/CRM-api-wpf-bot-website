using System.Collections;
using System.Collections.ObjectModel;

namespace DesktopApplication.Models
{
    public class DesktopPageModel
    {
        public ObservableCollection<string> statuses { get; set; }
        public ObservableCollection<string> statusesForEdit { get; set; }
        public IEnumerable messages { get; set; } 
        public int TextCountMessagesPeriod { get; set; }
        public int AllMessagesTextBlock { get; set; }
    }
}
