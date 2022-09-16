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
    public partial class AddNewProjectWindow : Window
    {
        Controller<ProjectsModel> projectsController;

        string filePath = "";

        public AddNewProjectWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// сохранить новый проект
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveNewProject_Click(object sender, RoutedEventArgs e)
        {
            var a = filePath;

            if (!string.IsNullOrEmpty(Title.Text) &&
                !string.IsNullOrEmpty(Description.Text) &&
                !string.IsNullOrEmpty(filePath))
            {

                ProjectsModel selectedProject = new ProjectsModel()
                {
                    Title = Title.Text,
                    Description = Description.Text,
                    Image = filePath
                };

                projectsController = new Controller<ProjectsModel>();

                projectsController.AddData(selectedProject);

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

                    ProjectsImage.Source = image;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
