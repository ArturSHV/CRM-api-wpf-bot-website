using DesktopApplication.Controllers;
using DesktopApplication.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DesktopApplication.View
{
    /// <summary>
    /// Логика взаимодействия для EditBlogWindow.xaml
    /// </summary>
    public partial class ContactsPage : Window
    {
        Controller<ContactsModel> contactsController = new Controller<ContactsModel>();


        public ContactsPage()
        {
            InitializeComponent();

            DataContext = contactsController.GetModel();
        }

        /// <summary>
        /// кнопка сохранения контактов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Title.Text) && 
                !string.IsNullOrEmpty(Description.Text) && 
                !string.IsNullOrEmpty(MapScript.Text))
            {
                ContactsModel contacts = new ContactsModel() { Title = Title.Text, 
                                                             Description = Description.Text, 
                                                             MapScript = MapScript.Text};

                contactsController.EditData(contacts);

                Close();
            }
            else
                MessageBox.Show("Заполните все данные");
            
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
