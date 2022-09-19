using DesktopApplication.Controllers;
using DesktopApplication.Models;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DesktopApplication.View
{
    /// <summary>
    /// Логика взаимодействия для Desktop.xaml
    /// </summary>
    public partial class ServicesPage : Page
    {
        Controller<ServicesModel> controller = new Controller<ServicesModel>();
        AddNewServiceWindow serviceWindow;

        public ServicesPage()
        {
            InitializeComponent();
            DataContext = controller;
        }


        /// <summary>
        /// Изменение данных услуг
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridServices_CurrentCellChanged(object sender, EventArgs e)
        {
            ServicesModel selectedService = dataGridServices.SelectedItem as ServicesModel;

            if (selectedService != null)
            {
                if ((selectedService.Title.Length > 0) && (selectedService.Description.Length > 0))

                    controller.EditData(selectedService);
            }
            
        }


        /// <summary>
        /// Кнопка добавить новую услугу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddService_Click(object sender, RoutedEventArgs e)
        {
            serviceWindow = new AddNewServiceWindow();

            serviceWindow.ShowDialog();

            controller.GetModel();
        }


        /// <summary>
        /// Кнопка обновить данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnRefreshService_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                var a = controller.GetModel();
                if (a == null)
                    MessageBox.Show("Нет соединения с сервером");
            });
            
        }

        /// <summary>
        /// кнопка удаления услуги
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnDeleteService_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridServices.SelectedIndex >= 0)
            {
                ServicesModel selectedService = dataGridServices.SelectedItem as ServicesModel;

                await Task.Run(() =>
                {
                    var result = controller.DeleteData(selectedService);

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
                MessageBox.Show("Выберите услугу");

        }
    }
}
