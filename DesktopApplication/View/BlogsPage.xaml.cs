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
        Controller<BlogsModel> controller = new Controller<BlogsModel>();
        AddNewBlogWindow blogWindow;
        EditBlogWindow editBlogWindow;

        public BlogsPage()
        {
            InitializeComponent();
            DataContext = controller;
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

            controller.GetModel();
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
                var a = controller.GetModel();
                if (a == null)
                    MessageBox.Show("Нет соединения с сервером");
            });
            
           
        }


        /// <summary>
        /// кнопка удаления блога
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnDeleteBlog_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridBlogs.SelectedIndex >= 0)
            {
                BlogsModel blogsModel = dataGridBlogs.SelectedItem as BlogsModel;

                await Task.Run(() =>
                {
                    var result = controller.DeleteData(blogsModel);

                    if (result != null)
                    {
                        controller.GetModel();

                        MessageBox.Show(result);
                    }
                    else
                        MessageBox.Show("Нет соединения с сервером");
                });

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
