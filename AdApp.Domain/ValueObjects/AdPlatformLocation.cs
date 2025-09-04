using AdApp.Domain.Exceptions;

namespace AdApp.Domain.ValueObjects
{
    public class AdPlatformLocation
    {
        public string Path { get; }

        private AdPlatformLocation(string path) => Path = path;

        public static AdPlatformLocation Create(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new DomainException("Location can't be empty");

            if (!path.StartsWith('/'))
                throw new DomainException("Location should starts on '/'");

            return new AdPlatformLocation(path.Trim());
        }

        public bool IsParentOf(AdPlatformLocation other) =>
            other.Path.StartsWith(Path + "/") || other.Path == Path;

        public bool IsChildOf(AdPlatformLocation other) =>
            Path.StartsWith(other.Path + "/") || Path == other.Path;

        public override string ToString() => Path;
    }
}
