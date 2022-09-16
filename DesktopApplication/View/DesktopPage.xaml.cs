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
        private async void BtnTodayClick(object sender, RoutedEventArgs e)
        {
            ChangeStyleBtn(sender);
            await Task.Run(() =>
            {
                App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    DataContext = desktopController.TodayData();
                });

            });

        }

        /// <summary>
        /// Показать данные за вчера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnYesterdayClick(object sender, RoutedEventArgs e)
        {
            ChangeStyleBtn(sender);
            await Task.Run(() =>
            {
                App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    DataContext = desktopController.YesterdayData();
                });

            });

        }

        /// <summary>
        /// Показать данные за неделю
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnWeekClick(object sender, RoutedEventArgs e)
        {
            ChangeStyleBtn(sender);
            await Task.Run(() =>
            {
                App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    DataContext = desktopController.WeekData();
                });

            });

        }


        /// <summary>
        /// Показать данные за месяц
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnMonthClick(object sender, RoutedEventArgs e)
        {
            ChangeStyleBtn(sender);
            await Task.Run(() =>
            {
                App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    DataContext = desktopController.MonthData();
                });

            });

        }

        /// <summary>
        /// Обновить данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            
            await Task.Run(() =>
            {
                App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    DataContext = desktopController.FirstData();

                    BtnEditStatus.IsEnabled = true;

                    comboAllStatuses.IsEnabled = true;

                    BtnFilter.IsEnabled = true;

                });

            });

           
        }


        /// <summary>
        /// Комбобокс сортировка по статусам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void comboAllStatuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Task.Run(() =>
            {
                App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    var status = comboAllStatuses.Text;

                    if (!string.IsNullOrEmpty(status))
                    {
                        if (status != "Все")
                        {
                            DataContext = desktopController.SortedDataByStatus(status);
                        }
                        else
                        {
                            DataContext = desktopController.FirstData();
                        }

                    }
                    
                });

            });
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
        private async void ComboBoxChangeStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Task.Run(() =>
            {
                App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    if (!string.IsNullOrEmpty(HiddenSelectedMessage.Text))
                    { 
                        var id = Convert.ToInt32(HiddenSelectedMessage.Text);

                        string status = ComboBoxChangeStatus.SelectedItem as string;

                        MessagesModel messagesModel = new MessagesModel() { id = id, status = status };

                        desktopController.EditStatus(messagesModel);

                        DataContext = desktopController.FirstData();

                    }

                });

            });
            
        }

        /// <summary>
        /// Показать данные за период
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnPeriodClick(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                DateTime date1;

                DateTime date2;

                App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    if (datePicker1.Text != "")
                        date1 = Convert.ToDateTime(datePicker1.Text);
                    else
                        date1 = DateTime.MinValue;

                    if (datePicker2.Text != "")
                        date2 = Convert.ToDateTime(datePicker2.Text);
                    else
                        date2 = DateTime.Now;

                    DataContext = desktopController.PeriodData(date1, date2);
                });

            });
        }
    }
}
