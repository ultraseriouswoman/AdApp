using AdApp.Domain.Entities;
using AdApp.Domain.ValueObjects;

namespace AdApp.Domain.Interfaces
{
    public interface IAdPlatformRepository
    {
        Task ClearAsync();
        Task AddRangeAsync(IEnumerable<AdPlatform> platforms);
        Task<IEnumerable<AdPlatform>> FindByLocationAsync(AdPlatformLocation location);
    }
}
