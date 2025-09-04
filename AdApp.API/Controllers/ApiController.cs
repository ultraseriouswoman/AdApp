using AdApp.API.Filters.ExceptionFilters;
using Microsoft.AspNetCore.Mvc;

namespace AdApp.API.Controllers
{
    [ApiController]
    [TypeFilter(typeof(ApiExceptionFilter))]
    public class ApiController : ControllerBase
    {
    }
}
