using AdApp.Application.Commands;
using AdApp.Application.DTO;
using AdApp.Application.DTO.Requests;
using AdApp.Application.DTO.Responses;
using AdApp.Application.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdApp.API.Controllers
{
    [ApiController]
    [Route("api/platforms")]
    public class AdPlatformsController(IMediator mediator, IMapper mapper) : ApiController
    {
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ApiResponse<IEnumerable<AdPlatformResponse>>>> UploadAdPlatforms(
            [FromForm] UploadAdPlatformsRequest request)
        {
            try
            {
                var command = new UploadAdPlatformsCommand(request);
                var result = mapper.Map<IEnumerable<AdPlatformResponse>>(await mediator.Send(command));

                return new ApiResponse<IEnumerable<AdPlatformResponse>>
                {
                    Result = result,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
            
        }

        [HttpGet("search")]
        public async Task<ActionResult<ApiResponse<IEnumerable<AdPlatformResponse>>>> GetAdPlatformsByLocation(
            [FromQuery] string location)
        {
            try
            {
                var query = new GetAdPlatformsByLocationQuery(location);
                var result = mapper.Map<IEnumerable<AdPlatformResponse>>(await mediator.Send(query));

                return new ApiResponse<IEnumerable<AdPlatformResponse>>
                {
                    Result = result,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
