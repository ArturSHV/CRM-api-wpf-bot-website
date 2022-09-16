using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DesktopApplication.Models;
using DesktopApplication.Controllers;

namespace DesktopApplication.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewServiceWindow.xaml
    /// </summary>
    public partial class AddNewServiceWindow : Window
    {
        Controller<ServicesModel> servicesController;
        ServicesModel servicesModel { get; set; }
        public AddNewServiceWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// кнопка сохранить новую услугу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveNewService_Click(object sender, RoutedEventArgs e)
        {
            string title = Title.Text;

            string descripton = Description.Text;

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(descripton))
                MessageBox.Show("Заполните все данные");
            else
            {
                servicesController = new Controller<ServicesModel>();

                servicesModel = new ServicesModel() { Title = title, Description = descripton };

                var result = servicesController.AddData(servicesModel);

                MessageBox.Show(result);

                Close();
            }
        }

    }
}
