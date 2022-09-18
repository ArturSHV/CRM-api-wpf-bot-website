using DesktopApplication.Controllers;
using PropertyChanged;
using System.Collections;
using System.Threading.Tasks;

namespace DesktopApplication.Models
{
    [AddINotifyPropertyChangedInterface]
    public class ServicesPageModel : IPageModel
    {
        Controller<ServicesModel> controller = new Controller<ServicesModel>();
        public IEnumerable model { get; set; }

        public async void PageModelCreator()
        {
            await Task.Run(() =>
            {
                model = controller.GetModel();
            });
        }
    }
}
