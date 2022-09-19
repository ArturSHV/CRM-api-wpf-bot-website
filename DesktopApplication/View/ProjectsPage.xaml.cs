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
        Controller<ProjectsModel> сontroller = new Controller<ProjectsModel>();
        AddNewProjectWindow projectWindow;
        EditProjectWindow editProjectWindow;

        public ProjectsPage()
        {
            InitializeComponent();
            DataContext = сontroller;
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
            сontroller.GetModel();
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
                var a = сontroller.GetModel();

                if (a == null)
                    MessageBox.Show("Нет соединения с сервером");
            });
        }


        /// <summary>
        /// кнопка удаления проекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnDeleteProject_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridProjects.SelectedIndex >= 0)
            {
                ProjectsModel projectsModel = dataGridProjects.SelectedItem as ProjectsModel;

                await Task.Run(() =>
                {
                    var result = сontroller.DeleteData(projectsModel);

                    if (result != null)
                    {
                        сontroller.GetModel();

                        MessageBox.Show(result);
                    }
                    else
                        MessageBox.Show("Нет соединения с сервером");
                });
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
            }
            else
                MessageBox.Show("Выберите проект");
        }
    }
}
