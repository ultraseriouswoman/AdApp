using AdApp.Application.DTO.Requests;
using AdApp.Domain.Entities;
using MediatR;

namespace AdApp.Application.Commands
{
    public record class UploadAdPlatformsCommand
        (UploadAdPlatformsRequest Request) : IRequest<IEnumerable<AdPlatform>>
    {
    }
}
