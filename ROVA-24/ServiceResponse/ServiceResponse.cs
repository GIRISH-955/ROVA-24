using System.Net;

namespace ROVA_24.ServiceResponse
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}
