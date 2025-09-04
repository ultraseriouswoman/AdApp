using AdApp.Application.DTO.Responses;
using AdApp.Domain.Entities;
using AdApp.Domain.Interfaces;
using AdApp.Domain.ValueObjects;
using AutoMapper;
using MediatR;

namespace AdApp.Application.Queries
{
    public class GetAdPlatformsByLocationQueryHandler : IRequestHandler<GetAdPlatformsByLocationQuery, IEnumerable<AdPlatform>>
    {
        private readonly IAdPlatformRepository _repository;

        public GetAdPlatformsByLocationQueryHandler(IAdPlatformRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AdPlatform>> Handle(GetAdPlatformsByLocationQuery request, CancellationToken cancellationToken)
        {
            var location = AdPlatformLocation.Create(request.SearchingPath);
            return await _repository.FindByLocationAsync(location);
        }
    }
}
