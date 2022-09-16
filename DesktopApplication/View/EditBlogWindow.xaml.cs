using DesktopApplication.Controllers;
using DesktopApplication.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DesktopApplication.View
{
    /// <summary>
    /// Логика взаимодействия для EditBlogWindow.xaml
    /// </summary>
    public partial class EditBlogWindow : Window
    {
        Controller<BlogsModel> blogsController;

        string filePath;

        public EditBlogWindow(DataGrid dataGrid)
        {
            InitializeComponent();

            BlogsModel selectedBlog = dataGrid.SelectedItem as BlogsModel;

            DataContext = selectedBlog;
            
        }

        /// <summary>
        /// кнопка сохранения блога
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveBlog_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Title.Text) && 
                !string.IsNullOrEmpty(Description.Text) && 
                !string.IsNullOrEmpty(BlogsImage.Source.ToString()) &&
                !string.IsNullOrEmpty(datePicker1.SelectedDate.ToString()))
            {
                BlogsModel selectedBlog = new BlogsModel() { Id = Convert.ToInt32(IdBlog.Text),
                                                             Title = Title.Text, 
                                                             Description = Description.Text, 
                                                             Image = BlogsImage.Source.ToString(),
                                                             CreateDate = (DateTime)datePicker1.SelectedDate};

                blogsController = new Controller<BlogsModel>();

                blogsController.EditData(selectedBlog);

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
