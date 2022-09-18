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
    public partial class DesktopPage : Page
    {
        DesktopController desktopController = new DesktopController();
        bool rightMenuIsOpen = false;
        Button LastBtnName = new Button();


        public DesktopPage()
        {
            InitializeComponent();
            DataContext = desktopController;
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
        /// Показать данные за сегодня
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTodayClick(object sender, RoutedEventArgs e)
        {
            ChangeStyleBtn(sender);
            desktopController.TodayData();
        }

        /// <summary>
        /// Показать данные за вчера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnYesterdayClick(object sender, RoutedEventArgs e)
        {
            ChangeStyleBtn(sender);
            desktopController.YesterdayData();

        }

        /// <summary>
        /// Показать данные за неделю
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnWeekClick(object sender, RoutedEventArgs e)
        {
            ChangeStyleBtn(sender);
            desktopController.WeekData();

        }


        /// <summary>
        /// Показать данные за месяц
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMonthClick(object sender, RoutedEventArgs e)
        {
            ChangeStyleBtn(sender);
            desktopController.MonthData();

        }

        /// <summary>
        /// Обновить данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {

            desktopController.FirstData();

            BtnEditStatus.IsEnabled = true;

            comboAllStatuses.IsEnabled = true;

            BtnFilter.IsEnabled = true;

        }




        /// <summary>
        /// Комбобокс сортировка по статусам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboAllStatuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var status = comboAllStatuses.SelectedItem as StatusesModel;

            if (status!=null)
            {
                desktopController.SortedDataByStatus(status);

            }
        }


        /// <summary>
        /// Изменить статус кнопка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditStatus_Click(object sender, RoutedEventArgs e)
        {
            StackPanelChangeStatus.Visibility = Visibility.Visible;

            ComboBoxChangeStatus.IsDropDownOpen = true;

        }


        /// <summary>
        /// Открыть правое меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFilter_Click(object sender, RoutedEventArgs e)
        {
            if (rightMenuIsOpen == false)
            {
                rightMenuIsOpen = true;

                WidthRightMenu.Width = new GridLength(195);
            }

            else
            {
                rightMenuIsOpen = false;

                WidthRightMenu.Width = new GridLength(0);
            }

        }


        /// <summary>
        /// Изменить статус Комбобокс
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxChangeStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(HiddenSelectedMessage.Text))
            {
                var id = Convert.ToInt32(HiddenSelectedMessage.Text);

                var status = ComboBoxChangeStatus.SelectedItem as StatusesModel;

                MessagesModel messagesModel = new MessagesModel() { id = id, status = status.Name };

                desktopController.EditStatus(messagesModel);
            }

        }

        /// <summary>
        /// Показать данные за период
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPeriodClick(object sender, RoutedEventArgs e)
        {
            DateTime date1;

            DateTime date2;

            if (datePicker1.Text != "")
                date1 = Convert.ToDateTime(datePicker1.Text);
            else
                date1 = DateTime.MinValue;

            if (datePicker2.Text != "")
                date2 = Convert.ToDateTime(datePicker2.Text);
            else
                date2 = DateTime.Now;

            desktopController.PeriodData(date1, date2);
        }
    }
}
