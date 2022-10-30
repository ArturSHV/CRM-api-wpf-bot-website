using System;

namespace DesktopApplication.Models
{
    public class BlogsModel : IModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }

        public string UrlGet { get => "GetBlogs"; }

        public string UrlEdit { get => "EditBlog"; }

        public string UrlDelete { get => "DeleteBlog"; }

        public string UrlAdd { get => "AddBlog"; }
    }
}
