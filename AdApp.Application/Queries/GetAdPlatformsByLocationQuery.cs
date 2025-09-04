using AdApp.Domain.Entities;
using MediatR;

namespace AdApp.Application.Queries
{
    public record class GetAdPlatformsByLocationQuery
        (string SearchingPath) : IRequest<IEnumerable<AdPlatform>>
    {
    }
}
