using DesktopApplication.Controllers;
using PropertyChanged;
using System.Collections;
using System.Threading.Tasks;

namespace DesktopApplication.Models
{
    [AddINotifyPropertyChangedInterface]
    public class BlogsPageModel : IPageModel
    {
        Controller<BlogsModel> controller = new Controller<BlogsModel>();

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
