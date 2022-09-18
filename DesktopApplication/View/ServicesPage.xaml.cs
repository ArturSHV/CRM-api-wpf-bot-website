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
        ServicesPageModel servicesPageModel = new ServicesPageModel();

        public ServicesPage()
        {
            InitializeComponent();
            DataContext = servicesPageModel;
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
        private void BtnAddService_Click(object sender, RoutedEventArgs e)
        {
            serviceWindow = new AddNewServiceWindow();

            serviceWindow.ShowDialog();

            servicesPageModel.PageModelCreator();
        }


        /// <summary>
        /// Кнопка обновить данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefreshService_Click(object sender, RoutedEventArgs e)
        {
            servicesPageModel.PageModelCreator();
        }

        /// <summary>
        /// кнопка удаления услуги
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteService_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridServices.SelectedIndex >= 0)
            {
                ServicesModel selectedService = dataGridServices.SelectedItem as ServicesModel;

                var result = servicesController.DeleteData(selectedService);

                servicesPageModel.PageModelCreator();

                MessageBox.Show(result);
            }
            else
                MessageBox.Show("Выберите услугу");

        }
    }
}
