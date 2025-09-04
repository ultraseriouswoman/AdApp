using AdApp.Domain.Entities;
using AdApp.Domain.Interfaces;
using AdApp.Domain.ValueObjects;
using System.Collections.Concurrent;

namespace AdApp.Infrastructure.Repositories
{
    public class AdPlatformRepository : IAdPlatformRepository
    {
        private readonly ConcurrentDictionary<Guid, AdPlatform> _platforms = new();
        private readonly ReaderWriterLockSlim _lock = new();

        public async Task ClearAsync()
        {
            await Task.Run(() =>
            {
                _lock.EnterWriteLock();
                try
                {
                    _platforms.Clear();
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            });
        }

        public async Task AddRangeAsync(IEnumerable<AdPlatform> platforms)
        {
            await Task.Run(() =>
            {
                _lock.EnterWriteLock();
                try
                {
                    foreach (var platform in platforms)
                    {
                        _platforms[platform.Id] = platform;
                    }
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            });
        }

        public async Task<IEnumerable<AdPlatform>> FindByLocationAsync(AdPlatformLocation location)
        {
            return await Task.Run(() =>
            {
                _lock.EnterReadLock();
                try
                {
                    return _platforms.Values
                        .Where(platform => platform.ServesLocation(location))
                        .OrderBy(platform => GetMinLocationDepth(platform, location))
                        .ThenBy(platform => platform.Title)
                        .ToList();
                }
                finally
                {
                    _lock.ExitReadLock();
                }
            });
        }

        private int GetMinLocationDepth(AdPlatform platform, AdPlatformLocation targetLocation)
        {
            return platform.Locations
                .Where(loc => loc.IsParentOf(targetLocation))
                .Min(loc => loc.Path.Split('/').Length - 1);
        }
    }
}
