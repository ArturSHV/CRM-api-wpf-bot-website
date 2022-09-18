using DesktopApplication.Controllers;
using PropertyChanged;
using System.Collections;
using System.Threading.Tasks;

namespace DesktopApplication.Models
{
    [AddINotifyPropertyChangedInterface]
    public class ProjectsPageModel : IPageModel
    {
        Controller<ProjectsModel> controller = new Controller<ProjectsModel>();
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
