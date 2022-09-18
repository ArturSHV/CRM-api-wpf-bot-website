using DesktopApplication.Models;
using DesktopApplication.View;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DesktopApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DesktopPage desktop = new DesktopPage();
        ServicesPage services = new ServicesPage();
        ProjectsPage projects = new ProjectsPage();
        BlogsPage blogs = new BlogsPage();
        ContactsPage contacts;
        Button LastBtnName = new Button();

        public MainWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Изменить стиль активной кнопки
        /// </summary>
        /// <param name="sender"></param>
        private void ChangeStyleBtn(object sender)
        {
            LastBtnName.Style = (Style)Application.Current.FindResource("BtnMainLeftMenuStyle");

            var button = sender as Button;

            button.Style = (Style)Application.Current.FindResource("BtnMainLeftMenuStyleActive"); 

            LastBtnName = button;

        }

        /// <summary>
        /// вкладка рабочий стол
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesktopBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = desktop;
            ChangeStyleBtn(sender);
            

        }


        /// <summary>
        /// вкладка услуги
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Services_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = services;
            ChangeStyleBtn(sender);
        }


        /// <summary>
        /// вкладка проекты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Projects_click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = projects;
            ChangeStyleBtn(sender);
        }


        /// <summary>
        /// вкладка блоги
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Blogs_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = blogs;
            ChangeStyleBtn(sender);
        }

        private void Contacts_Click(object sender, RoutedEventArgs e)
        {
            contacts = new ContactsPage();
            MainFrame.Content = null;
            contacts.ShowDialog();
            ChangeStyleBtn(sender);
        }
    }
}
