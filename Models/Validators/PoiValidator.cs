using FluentValidation;

namespace FirstCoreApi.Models.Validators
{
    public class PoiValidator : AbstractValidator<PointOfInterestUpdateDto>
    {
        public PoiValidator()
        {
            RuleFor(x => x.Name).Must(n => n != "Forbidden").WithMessage("This is clearly forbidden for me.");
        }
    }
}
