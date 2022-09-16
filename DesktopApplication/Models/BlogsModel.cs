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

        public string UrlGet { get => "https://localhost:44369/api/GetBlogs"; }

        public string UrlEdit { get => "https://localhost:44369/api/EditBlog"; }

        public string UrlDelete { get => "https://localhost:44369/api/DeleteBlog"; }

        public string UrlAdd { get => "https://localhost:44369/api/AddBlog"; }
    }
}
