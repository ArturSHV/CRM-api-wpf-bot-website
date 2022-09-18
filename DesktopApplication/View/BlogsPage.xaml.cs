using DesktopApplication.Controllers;
using DesktopApplication.Models;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DesktopApplication.View
{
    /// <summary>
    /// Логика взаимодействия для ProjectsPage.xaml
    /// </summary>
    public partial class BlogsPage : Page
    {
        Controller<BlogsModel> blogsController = new Controller<BlogsModel>();
        AddNewBlogWindow blogWindow;
        EditBlogWindow editBlogWindow;

        BlogsPageModel blogsPageModel = new BlogsPageModel();

        public BlogsPage()
        {
            InitializeComponent();
            DataContext = blogsPageModel;
        }


        /// <summary>
        /// кнопка добавления блога
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddBlog_Click(object sender, RoutedEventArgs e)
        {
            blogWindow = new AddNewBlogWindow();
            blogWindow.ShowDialog();

            blogsPageModel.PageModelCreator();
        }


        /// <summary>
        /// кнопка обновления блогов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefreshBlog_Click(object sender, RoutedEventArgs e)
        {
            blogsPageModel.PageModelCreator();
        }


        /// <summary>
        /// кнопка удаления блога
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteBlog_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridBlogs.SelectedIndex >= 0)
            {
                BlogsModel blogsModel = dataGridBlogs.SelectedItem as BlogsModel;

                var result = blogsController.DeleteData(blogsModel);

                blogsPageModel.PageModelCreator();

                MessageBox.Show(result);
            }
            else
                MessageBox.Show("Выберите блог");

        }

        /// <summary>
        /// кнопка редактирования блога
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditBlog_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridBlogs.SelectedIndex >= 0)
            {
                editBlogWindow = new EditBlogWindow(dataGridBlogs);

                editBlogWindow.ShowDialog();
            }
            else
                MessageBox.Show("Выберите блог");
        }
    }
}
