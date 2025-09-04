using AdApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AdApp.Application.DTO.Requests
{
    public record class UploadAdPlatformsRequest
        (IFormFile File)
    {
    }
}
