using DesktopApplication.Api;
using DesktopApplication.Models;
using DesktopApplication.View;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DesktopApplication.Helpers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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
        ContactsPage contacts = new ContactsPage();
        Button LastBtnName = new Button();
        MessageCreate messageCreate = new MessageCreate();
        DataApi dataApi = new DataApi();
        Account account;

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
            MainFrame.Content = contacts;
            ChangeStyleBtn(sender);

        }

        /// <summary>
        /// Авторизация в системе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Login.Text) && !string.IsNullOrEmpty(Password.Password))
            {
                account = new Account() { Login = Login.Text, Password = Password.Password };

                var a = dataApi.GetToken(account);

                if (a == null)
                {
                    MessageBox.Show("Отсутсвует соединение с сервером");
                    Application.Current.Shutdown();
                }
                else
                {
                    try
                    {
                        Token.token = JObject.Parse(a)["result"]["token"].Value<string>();
                        BackgroundPanelLogin.Visibility = Visibility.Collapsed;
                        PanelLogin.Visibility = Visibility.Collapsed;
                    }
                    catch
                    {
                        MessageBox.Show(messageCreate.ReturnMessage(a));
                    }
                }
            }
            else
                MessageBox.Show("Введите логин и пароль");

        }
    }
}
