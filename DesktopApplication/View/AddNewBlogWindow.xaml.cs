using DesktopApplication.Api;
using DesktopApplication.Controllers;
using DesktopApplication.Models;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DesktopApplication.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewProjectWindow.xaml
    /// </summary>
    public partial class AddNewBlogWindow : Window
    {
        Controller<BlogsModel> blogsController;

        string filePath = "";

        public AddNewBlogWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// сохранить новый блог
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveNewBlog_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Title.Text) &&
                !string.IsNullOrEmpty(Description.Text) &&
                !string.IsNullOrEmpty(filePath))
            {

                BlogsModel selectedBlog = new BlogsModel()
                {
                    Title = Title.Text,
                    Description = Description.Text,
                    Image = filePath,
                    CreateDate = DateTime.Now
                };

                blogsController = new Controller<BlogsModel>();

                blogsController.AddData(selectedBlog);

                Close();
            }
            else
                MessageBox.Show("Заполните все данные");
        }

        /// <summary>
        /// загрузить картинку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UploadFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            bool? response = openFileDialog.ShowDialog();

            if (response == true)
            {
                filePath = openFileDialog.FileName;

                try
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.UriSource = new Uri(filePath);
                    image.EndInit();

                    BlogsImage.Source = image;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
