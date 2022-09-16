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

        BlogsPageModel blogsPageModel;

        public BlogsPage()
        {
            InitializeComponent();
            blogsPageModel = new BlogsPageModel() { model = blogsController.GetModel() };
        }


        /// <summary>
        /// кнопка добавления блога
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnAddBlog_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    blogWindow = new AddNewBlogWindow();
                    blogWindow.ShowDialog();
                });
            });
        }


        /// <summary>
        /// кнопка обновления блогов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnRefreshBlog_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    DataContext = blogsPageModel;
                });
            });
        }


        /// <summary>
        /// кнопка удаления блога
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnDeleteBlog_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    if (dataGridBlogs.SelectedIndex >= 0)
                    {
                        BlogsModel blogsModel = dataGridBlogs.SelectedItem as BlogsModel;

                        var result = blogsController.DeleteData(blogsModel);

                        MessageBox.Show(result);

                        DataContext = blogsPageModel;
                    }
                    else
                        MessageBox.Show("Выберите блог");
                });
            });

        }

        /// <summary>
        /// кнопка редактирования блога
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnEditBlog_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    if (dataGridBlogs.SelectedIndex >= 0)
                    {
                        editBlogWindow = new EditBlogWindow(dataGridBlogs);

                        editBlogWindow.ShowDialog();
                    }
                    else
                        MessageBox.Show("Выберите блог");
                    
                });
            });
            
        }
    }
}
