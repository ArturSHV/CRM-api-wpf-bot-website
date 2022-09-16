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
        Controller<ServicesModel> servicesController = new Controller<ServicesModel>();
        AddNewServiceWindow serviceWindow;
        ServicesPageModel servicesPageModel;

        public ServicesPage()
        {
            InitializeComponent();
            servicesPageModel = new ServicesPageModel() { model = servicesController.GetModel() };
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

                    servicesController.EditData(selectedService);
            }
            
        }


        /// <summary>
        /// Кнопка добавить новую услугу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnAddService_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    serviceWindow = new AddNewServiceWindow();

                    serviceWindow.ShowDialog();
                });
            });
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
                App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    DataContext = servicesPageModel;
                });
            });
        }

        /// <summary>
        /// кнопка удаления услуги
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnDeleteService_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    if (dataGridServices.SelectedIndex >= 0)
                    {
                        ServicesModel selectedService = dataGridServices.SelectedItem as ServicesModel;

                        var result = servicesController.DeleteData(selectedService);

                        MessageBox.Show(result);

                        DataContext = servicesPageModel;
                    }
                    else
                        MessageBox.Show("Выберите услугу");
                });
            });

        }
    }
}
