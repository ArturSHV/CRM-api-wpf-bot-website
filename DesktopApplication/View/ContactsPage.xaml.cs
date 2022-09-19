using DesktopApplication.Controllers;
using DesktopApplication.Models;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DesktopApplication.View
{
    /// <summary>
    /// Логика взаимодействия для EditBlogWindow.xaml
    /// </summary>
    public partial class ContactsPage : Page
    {
        Controller<ContactsModel> controller = new Controller<ContactsModel>();


        public ContactsPage()
        {
            InitializeComponent();

            DataContext = controller;
        }

        private async void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Preloader.Visibility = Visibility.Visible;

            await Task.Run(() =>
            {
                var a = controller.GetModel();
                if (a == null)
                    MessageBox.Show("Нет соединения с сервером");

                App.Current.Dispatcher.Invoke(() =>
                {
                    Preloader.Visibility = Visibility.Collapsed;
                });
            });
        }

        /// <summary>
        /// Изменение данных услуг
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            ContactsModel contact = dataGrid.SelectedItem as ContactsModel;

            if (contact != null)
            {
                if ((contact.Title.Length > 0) && (contact.Description.Length > 0) && (contact.MapScript.Length > 0))

                    controller.EditData(contact);
            }

        }

        //private void BtnCloseWindow_Click(object sender, RoutedEventArgs e)
        //{
        //    Close();
        //}

        //private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    DragMove();
        //}
    }
}
