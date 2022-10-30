namespace WebApi.Models
{
    public static class ErrorMessages
    {
        public static Dictionary<int,string> errorMessage = new Dictionary<int, string>()
            {
                {400, "Bad Request" },
                {401, "Для получения запрашиваемого ответа нужна аутентификация" },
                {403, "Нет прав доступа к содержимому" },
                {404, "Запрашиваемый ресурс не найден" },
                {500, "Внутренняя ошибка сервера" },
                {503, "Сервис недоступен" }
            };

    }
}
