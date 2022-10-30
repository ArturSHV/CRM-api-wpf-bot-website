using System.Net;

namespace WebSite.Helpers
{
    public static class FileHelper
    {
        /// <summary>
        /// Скачивание файла в папку. Возврашает путь к файлу
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public static string GetFile(IFormFile formFile)
        {
            try
            {
                var filePath = Path.GetTempFileName() + formFile.FileName;

                string extension = Path.GetExtension(formFile.FileName);

                if ((extension == ".png") || (extension == ".jpg") || (extension == ".jpeg"))

                {
                    using (var stream = File.Create(filePath))
                    {
                        formFile.CopyToAsync(stream);
                    }

                    return filePath;
                }

            }
            catch { }

            return "";
        }

    }
}
