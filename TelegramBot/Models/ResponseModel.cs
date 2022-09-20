using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Models
{
    public class ResponseModel
    {
        public ReplyKeyboardMarkup replyKeyboardMarkup { get; set; }
        public string textResponse { get; set; }
    }
}
