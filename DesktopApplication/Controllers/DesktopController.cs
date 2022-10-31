using DesktopApplication.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static DesktopApplication.Helpers.GetApiData;
using PropertyChanged;
using System.Collections;
using System.Threading.Tasks;
using DesktopApplication.Api;
using DesktopApplication.Helpers;

namespace DesktopApplication.Controllers
{

    [AddINotifyPropertyChangedInterface]
    public class DesktopController
    {
        StatusesModel statusesModel = new StatusesModel();
        MessagesModel messagesModel = new MessagesModel();
        DataApi dataApi = new DataApi();
        MessageCreate messageCreate = new MessageCreate();


        /// <summary>
        /// Заявки
        /// </summary>
        public IEnumerable<MessagesModel> messages { get; set; } 


        public ObservableCollection<MessagesModel> forMessages { get; set; }

        /// <summary>
        /// Статусы
        /// </summary>
        public ObservableCollection<StatusesModel> statuses { get; set; }


        /// <summary>
        /// Заявок за период
        /// </summary>
        public int TextCountMessagesPeriod { get; set; }

        /// <summary>
        /// Всего заявок
        /// </summary>
        public int AllMessagesTextBlock {
            get{ 
                if (messages != null) 
                    return forMessages.Count; 
                else 
                    return 0; 
             }}


        /// <summary>
        /// Начальные данные
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MessagesModel> FirstData()
        {
            GetStatuses();

            GetMessages();

            TextCountMessagesPeriod = (messages != null ? messages.Count() : 0);

            return messages;
        }

        /// <summary>
        /// Заявки сегодня
        /// </summary>
        /// <returns></returns>
        public async void TodayData()
        {
            await Task.Run(() =>
            {
                GetStatuses();

                GetMessages();

                var date = DateTime.Today;

                messages = forMessages.Where(x => x.date.ToShortDateString() == date.ToShortDateString());

                TextCountMessagesPeriod = messages.Count();
            });
            
        }

        /// <summary>
        /// Заявки вчера
        /// </summary>
        /// <returns></returns>
        public async void YesterdayData()
        {
            await Task.Run(() =>
            {
                GetStatuses();

                GetMessages();

                var date = DateTime.Today.AddDays(-1);

                messages = forMessages.Where(x => x.date.ToShortDateString() == date.ToShortDateString());

                TextCountMessagesPeriod = messages.Count();
            });
            
        }

        /// <summary>
        /// Заявки неделя
        /// </summary>
        /// <returns></returns>
        public async void WeekData()
        {
            await Task.Run(() =>
            {
                GetStatuses();

                GetMessages();

                var date = DateTime.Today.AddDays(-7);

                messages = forMessages.Where(x => x.date >= date);

                TextCountMessagesPeriod = messages.Count();
            });
            
        }

        /// <summary>
        /// Заявки месяц
        /// </summary>
        /// <returns></returns>
        public async void MonthData()
        {
            await Task.Run(() =>
            {
                GetStatuses();

                GetMessages();

                var date = DateTime.Today.AddMonths(-1);

                messages = forMessages.Where(x => x.date >= date);

                TextCountMessagesPeriod = messages.Count();
            });
            
        }

        /// <summary>
        /// Заявки период
        /// </summary>
        /// <returns></returns>
        public async void PeriodData(DateTime date1, DateTime date2)
        {
            await Task.Run(() =>
            {
                GetStatuses();

                GetMessages();

                var date = DateTime.Today.AddMonths(-1);

                messages = forMessages.Where(x => (x.date >= date1) && (x.date <= date2));

                TextCountMessagesPeriod = messages.Count();
            });
            
        }


        /// <summary>
        /// Заявки отсортированные по статусу
        /// </summary>
        /// <returns></returns>
        public async void SortedDataByStatus(StatusesModel status)
        {
            await Task.Run(() =>
            {
                GetMessages();

                messages = forMessages.Where(x => x.status == status.Name);

                TextCountMessagesPeriod = messages.Count();
            });
            
        }


        /// <summary>
        /// Все статусы
        /// </summary>
        private void GetStatuses()
        {
            var status = dataApi.GetModel(new { token = Token.token }, statusesModel.urlGetStatuses);

            statuses = messageCreate.Response(status, messageCreate.ReturnListModelInView<StatusesModel>(status));

        }

        /// <summary>
        /// Все заявки
        /// </summary>
        public void GetMessages()
        {
            var a = dataApi.GetModel(new { token = Token.token }, messagesModel.urlGetMessages);

            forMessages = messageCreate.Response(a, messageCreate.ReturnListModelInView<MessagesModel>(a));

            messages = forMessages;

        }


        /// <summary>
        /// Изменить статус
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public void EditStatus(MessagesModel messagesModel)
        {
            string request = dataApi.EditModel(new {token = Token.token, model = messagesModel}, messagesModel.urlEditMessage);
            
            GetMessages();
            
        }
    }
}
