using DesktopApplication.Controllers;
using DesktopApplication.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DesktopApplication.View
{
    /// <summary>
    /// Логика взаимодействия для EditProjectWindow.xaml
    /// </summary>
    public partial class EditProjectWindow : Window
    {
        Controller<ProjectsModel> projectsController;

        string filePath;

        public EditProjectWindow(DataGrid dataGrid)
        {
            InitializeComponent();

            ProjectsModel selectedProject = dataGrid.SelectedItem as ProjectsModel;

            DataContext = selectedProject;
            
        }

        /// <summary>
        /// кнопка сохранения проекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveProject_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Title.Text) && 
                !string.IsNullOrEmpty(Description.Text) && 
                !string.IsNullOrEmpty(ProjectsImage.Source.ToString()))
            {
                ProjectsModel selectedProject = new ProjectsModel() { Id = Convert.ToInt32(IdProject.Text),
                                                                      Title = Title.Text, 
                                                                      Description = Description.Text, 
                                                                      Image = ProjectsImage.Source.ToString() };

                projectsController = new Controller<ProjectsModel>();

                projectsController.EditData(selectedProject);

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
