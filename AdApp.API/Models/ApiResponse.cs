using System.Net;

namespace AdApp.Application.DTO
{
    public class ApiResponse : ApiResponse<object> // Объект по умолчанию
    {

    }
    public class ApiResponse<T> // Используется для документации в Swagger
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Success
        {
            get
            {
                return (int)StatusCode / 100 == 2;
            }
        }
        public List<string> ErrorMessages { get; set; } = new();
        public T? Result { get; set; }
    }
}
