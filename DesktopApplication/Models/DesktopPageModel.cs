using System.Collections;
using System.Collections.ObjectModel;
using DesktopApplication.Controllers;
using PropertyChanged;
using System.Threading.Tasks;


namespace DesktopApplication.Models
{
    public class DesktopPageModel
    {
        DesktopController controller = new DesktopController();
        public ObservableCollection<string> statuses { get; set; }
        public ObservableCollection<string> statusesForEdit { get; set; }
        public IEnumerable messages { get; set; }
        public int TextCountMessagesPeriod { get; set; }
        public int AllMessagesTextBlock { get; set; }

    }
}
