namespace WebApi.Models
{
    public class RequestedData<T>
    {
        public string token { get; set; }
        public T model { get; set; }
    }

}
