using FluentValidation;

namespace AdApp.Application.Commands.Validators
{
    public class UploadAdPlatformsCommandValidator : AbstractValidator<UploadAdPlatformsCommand>
    {
        public UploadAdPlatformsCommandValidator()
        {
            RuleFor(x => x.Request.File)
                .NotNull().WithMessage("File is required")
                .Must(file => file != null && file.Length > 0).WithMessage("File cannot be empty")
                .Must(file => file != null &&
                     (Path.GetExtension(file.FileName).ToLower() == ".txt" ||
                      file.ContentType == "text/plain"))
                .WithMessage("Only .txt files are supported");
        }
    }
}
