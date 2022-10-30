using System.Net;
using WebApi.Data;

namespace WebApi.Helpers
{
    public static class FileHelper
    {
        /// <summary>
        /// Скачивание файла в папку Api. Возврашает путь к файлу
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public static async Task<string> GetFile(string uploadedFile, IWebHostEnvironment _appEnvironment)
        {

            string directoryPath = Path.Combine(_appEnvironment.ContentRootPath, "wwwroot/img");

            try
            {
                string extension = Path.GetExtension(uploadedFile);

                if ((extension == ".png") || (extension == ".jpg") || (extension == ".jpeg"))

                {
                    string newNameImage = HashPasswordHelper.RandomString(30) + extension;

                    string filePath = Path.Combine(directoryPath, newNameImage);

                    WebClient webClient = new WebClient();

                    webClient.DownloadFile(uploadedFile, filePath);

                    directoryPath = NewPath(filePath);

                    return directoryPath;
                }

                else
                    return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        /// <summary>
        /// Создание пути к картинке для записи в БД
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static string NewPath(string filePath)
        {
            int index = filePath.LastIndexOf(@"\");

            string? imageFoulder = SettingsHelpers.ReturnHostString() + "img/";

            string directoryPath = imageFoulder + filePath[(index + 1)..];

            return directoryPath;
        }

    }
}
