using AdApp.Domain.Entities;
using AdApp.Domain.Exceptions;
using AdApp.Domain.Interfaces;
using AdApp.Domain.ValueObjects;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AdApp.Application.Commands
{
    public class UploadAdPlatformsCommandHandler
        : IRequestHandler<UploadAdPlatformsCommand, IEnumerable<AdPlatform>>
    {
        private readonly IAdPlatformRepository _repository;
        private readonly IValidator<UploadAdPlatformsCommand> _validator;

        public UploadAdPlatformsCommandHandler(IAdPlatformRepository repository,
            IValidator<UploadAdPlatformsCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<IEnumerable<AdPlatform>> Handle
            (UploadAdPlatformsCommand request, CancellationToken cancellationToken)
        {

            // Валидация файла
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new DomainException($"{string.Join(',', errors)}");
            }

            try
            {
                // Чтение файла
                string fileContent;
                using (var streamReader = new StreamReader(request.Request.File.OpenReadStream()))
                {
                    fileContent = await streamReader.ReadToEndAsync();
                }

                // Парсинг и обработка
                var parseResult = await ParseAndLoadFileContent(fileContent, cancellationToken);

                return parseResult;
            }
            catch (Exception ex)
            {
                throw new DomainException(ex.Message);
            }
        }

        private async Task<IEnumerable<AdPlatform>> ParseAndLoadFileContent(string content, CancellationToken cancellationToken)
        {
            var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var platforms = new List<AdPlatform>();

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Trim();

                // Пропускаем пустые строки и комментарии
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                {
                    continue;
                }

                try
                {
                    var platform = ParseLine(line);
                    if (platform != null)
                    {
                        platforms.Add(platform);
                    }
                }
                catch (DomainException ex)
                {
                    throw new DomainException(ex.Message);
                }
            }

            // Сохранение в репозиторий
            if (platforms.Count > 0)
            {
                await _repository.ClearAsync();
                await _repository.AddRangeAsync(platforms);
            }
            else
            {
                throw new DomainException("No valid platforms found in file");
            }

            return platforms;
        }

        private static AdPlatform ParseLine(string line)
        {
            var colonIndex = line.IndexOf(':');
            if (colonIndex == -1)
                throw new DomainException("Missing colon separator");

            var name = line[..colonIndex].Trim();
            var locationsPart = line[(colonIndex + 1)..].Trim();

            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Platform name cannot be empty");

            if (string.IsNullOrWhiteSpace(locationsPart))
                throw new DomainException("Locations cannot be empty");

            var locationPaths = locationsPart.Split(',', StringSplitOptions.RemoveEmptyEntries);
            var locations = new List<AdPlatformLocation>();

            foreach (var path in locationPaths)
            {
                var trimmedPath = path.Trim();
                if (!string.IsNullOrWhiteSpace(trimmedPath))
                {
                    locations.Add(AdPlatformLocation.Create(trimmedPath));
                }
            }

            if (locations.Count == 0)
                throw new DomainException("No valid locations found");

            return new AdPlatform(name, locations);
        }
    }
}
