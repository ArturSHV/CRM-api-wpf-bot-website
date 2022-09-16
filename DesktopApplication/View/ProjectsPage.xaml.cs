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
    public partial class ProjectsPage : Page
    {
        Controller<ProjectsModel> projectsController = new Controller<ProjectsModel>();
        AddNewProjectWindow projectWindow;
        EditProjectWindow editProjectWindow;

        ProjectsPageModel projectsPageModel;

        public ProjectsPage()
        {
            InitializeComponent();
            projectsPageModel = new ProjectsPageModel() { model = projectsController.GetModel() };
        }


        /// <summary>
        /// кнопка добавления проекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnAddProject_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    projectWindow = new AddNewProjectWindow();
                    projectWindow.ShowDialog();
                });
            });
        }


        /// <summary>
        /// кнопка обновления проектов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnRefreshProject_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    DataContext = projectsPageModel;
                });
            });
        }


        /// <summary>
        /// кнопка удаления проекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnDeleteProject_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    if (dataGridProjects.SelectedIndex >= 0)
                    {
                        ProjectsModel projectsModel = dataGridProjects.SelectedItem as ProjectsModel;

                        var result = projectsController.DeleteData(projectsModel);

                        MessageBox.Show(result);

                        DataContext = projectsPageModel;
                    }
                    else
                        MessageBox.Show("Выберите проект");
                });
            });

        }

        /// <summary>
        /// кнопка редактирования проекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnEditProject_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    if (dataGridProjects.SelectedIndex >= 0)
                    {
                        editProjectWindow = new EditProjectWindow(dataGridProjects);

                        editProjectWindow.ShowDialog();
                    }
                    else
                        MessageBox.Show("Выберите проект");
                    
                });
            });
            
        }
    }
}
