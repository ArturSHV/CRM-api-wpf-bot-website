using DesktopApplication.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static ClassLibrary.GetApiData;


namespace DesktopApplication.Controllers
{
    public class DesktopController
    {
        StatusesModel statusesModel = new StatusesModel();
        MessagesModel messagesModel = new MessagesModel();
        


        /// <summary>
        /// Модель для рабочего стола
        /// </summary>
        public DesktopPageModel desktopPageModel { get; set; }

        /// <summary>
        /// Заявки
        /// </summary>
        private ObservableCollection<MessagesModel> messages = new ObservableCollection<MessagesModel>();

        /// <summary>
        /// Статусы
        /// </summary>
        private ObservableCollection<string> statuses = new ObservableCollection<string>();

        /// <summary>
        /// Статусы для редактирования. Без "Все"
        /// </summary>
        private ObservableCollection<string> statusesForEdit = new ObservableCollection<string>();

        /// <summary>
        /// Заявок за период
        /// </summary>
        private int TextCountMessagesPeriod { get; set; }

        /// <summary>
        /// Всего заявок
        /// </summary>
        private int AllMessagesTextBlock {
            get{ 
                if (messages != null) 
                    return messages.Count; 
                else 
                    return 0; 
             }}


        /// <summary>
        /// Начальные данные
        /// </summary>
        /// <returns></returns>
        public DesktopPageModel FirstData()
        {
            GetStatuses();

            GetMessages();

            TextCountMessagesPeriod = messages.Count();

            return DesktopPageModelCreate(messages);
        }

        /// <summary>
        /// Заявки сегодня
        /// </summary>
        /// <returns></returns>
        public DesktopPageModel TodayData()
        {
            GetStatuses();

            GetMessages();

            var date = DateTime.Today;

            var a = messages.Where(x => x.date.ToShortDateString() == date.ToShortDateString());

            TextCountMessagesPeriod = a.Count();

            return DesktopPageModelCreate(a);
        }

        /// <summary>
        /// Заявки вчера
        /// </summary>
        /// <returns></returns>
        public DesktopPageModel YesterdayData()
        {
            GetStatuses();

            GetMessages();

            var date = DateTime.Today.AddDays(-1);

            var a = messages.Where(x => x.date.ToShortDateString() == date.ToShortDateString());

            TextCountMessagesPeriod = a.Count();

            return DesktopPageModelCreate(a);
        }

        /// <summary>
        /// Заявки неделя
        /// </summary>
        /// <returns></returns>
        public DesktopPageModel WeekData()
        {
            GetStatuses();

            GetMessages();

            var date = DateTime.Today.AddDays(-7);

            var a = messages.Where(x => x.date >= date);

            TextCountMessagesPeriod = a.Count();

            return DesktopPageModelCreate(a);
        }

        /// <summary>
        /// Заявки месяц
        /// </summary>
        /// <returns></returns>
        public DesktopPageModel MonthData()
        {
            GetStatuses();

            GetMessages();

            var date = DateTime.Today.AddMonths(-1);

            var a = messages.Where(x => x.date >= date);

            TextCountMessagesPeriod = a.Count();

            return DesktopPageModelCreate(a);
        }

        /// <summary>
        /// Заявки период
        /// </summary>
        /// <returns></returns>
        public DesktopPageModel PeriodData(DateTime date1, DateTime date2)
        {
            GetStatuses();

            GetMessages();

            var date = DateTime.Today.AddMonths(-1);

            var a = messages.Where(x => (x.date >= date1) && (x.date <= date2));

            TextCountMessagesPeriod = a.Count();
            
            return DesktopPageModelCreate(a);
        }


        /// <summary>
        /// Заявки отсортированные по статусу
        /// </summary>
        /// <returns></returns>
        public DesktopPageModel SortedDataByStatus(string status)
        {
            GetStatuses();

            GetMessages();

            var a = messages.Where(x => x.status == status);

            TextCountMessagesPeriod = a.Count();

            return DesktopPageModelCreate(a);
        }


        /// <summary>
        /// Создание модели рабочего стола
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private DesktopPageModel DesktopPageModelCreate(IEnumerable<MessagesModel> a)
        {
            desktopPageModel = new DesktopPageModel()
            {
                messages = a,
                statuses = statuses,
                statusesForEdit = statusesForEdit,
                AllMessagesTextBlock = AllMessagesTextBlock,
                TextCountMessagesPeriod = TextCountMessagesPeriod
            };
            return desktopPageModel;
        }


        /// <summary>
        /// Все статусы
        /// </summary>
        private void GetStatuses()
        {
            statuses.Clear();

            statusesForEdit.Clear();

            object status = PostData(statusesModel.urlGetStatuses);

            if (status != null)
            {
                string s = JArray.FromObject(status).ToString();

                var statusesCollection = JsonConvert.DeserializeObject<ObservableCollection<StatusesModel>>(s);

                statuses.Add("Все");
                foreach (var item in statusesCollection)
                {
                    statuses.Add(item.Name);
                    statusesForEdit.Add(item.Name);
                }
            }
        }

        /// <summary>
        /// Все заявки
        /// </summary>
        private void GetMessages()
        {
            messages.Clear();

            object a = PostData(messagesModel.urlGetMessages);
            if (a != null)
            {
                string c = JArray.FromObject(a).ToString();
                
                messages = JsonConvert.DeserializeObject<ObservableCollection<MessagesModel>>(c);
            }

            
        }


        /// <summary>
        /// Изменить статус
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public string EditStatus(MessagesModel messagesModel)
        {
            string request = SendPostData(messagesModel.urlEditMessage, messagesModel);

            return request;
        }


    }
}
