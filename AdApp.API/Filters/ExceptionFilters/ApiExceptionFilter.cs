using AdApp.Application.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace AdApp.API.Filters.ExceptionFilters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _hostEnvironment;
        protected ApiResponse response;

        public ApiExceptionFilter(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            response = new ApiResponse();
        }

        public void OnException(ExceptionContext context)
        {
            // Не отображает исключения, пока приложение запущено в Debug
            if (_hostEnvironment.IsDevelopment())
            {
                response.ErrorMessages.Add(context.Exception.ToString());
            }
            else
            {
                response.ErrorMessages.Add("An Internal Error occurred");
            }
            response.StatusCode = HttpStatusCode.InternalServerError;
            context.Result = new OkObjectResult(response);
        }
    }
}
