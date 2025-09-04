using AdApp.Domain.ValueObjects;

namespace AdApp.Domain.Entities
{
    public class AdPlatform
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public List<AdPlatformLocation> Locations { get; private set; }

        private AdPlatform() { } //For DB

        public AdPlatform(string title, IEnumerable<AdPlatformLocation> locations)
        {
            Id = Guid.NewGuid();
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Locations = locations?.ToList() ?? throw new ArgumentNullException(nameof(locations));
        }

        public bool ServesLocation(AdPlatformLocation targetLocation) =>
            Locations.Any(location => location.IsParentOf(targetLocation));
    }
}
