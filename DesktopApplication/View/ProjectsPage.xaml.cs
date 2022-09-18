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

        ProjectsPageModel projectsPageModel = new ProjectsPageModel();

        public ProjectsPage()
        {
            InitializeComponent();
            DataContext = projectsPageModel;
        }


        /// <summary>
        /// кнопка добавления проекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddProject_Click(object sender, RoutedEventArgs e)
        {
            projectWindow = new AddNewProjectWindow();
            projectWindow.ShowDialog();
            projectsPageModel.PageModelCreator();
        }


        /// <summary>
        /// кнопка обновления проектов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefreshProject_Click(object sender, RoutedEventArgs e)
        {
            projectsPageModel.PageModelCreator();
        }


        /// <summary>
        /// кнопка удаления проекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteProject_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridProjects.SelectedIndex >= 0)
            {
                ProjectsModel projectsModel = dataGridProjects.SelectedItem as ProjectsModel;

                var result = projectsController.DeleteData(projectsModel);

                projectsPageModel.PageModelCreator();

                MessageBox.Show(result);
            }
            else
                MessageBox.Show("Выберите проект");
        }

        /// <summary>
        /// кнопка редактирования проекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditProject_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridProjects.SelectedIndex >= 0)
            {
                editProjectWindow = new EditProjectWindow(dataGridProjects);

                editProjectWindow.ShowDialog();

                projectsPageModel.PageModelCreator();
            }
            else
                MessageBox.Show("Выберите проект");

        }
    }
}
