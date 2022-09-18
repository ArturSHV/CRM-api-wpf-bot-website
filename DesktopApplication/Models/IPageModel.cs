using System.Collections;
using System.Collections.Generic;

namespace DesktopApplication.Models
{
    public interface IPageModel
    {
        IEnumerable model { get; set; }
        void PageModelCreator();
    }

}
