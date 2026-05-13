using FluentValidation;
using SportShoes2026.Entities;

namespace SportShoes2026.Service.Validator
{
    public class SportShoeValidator : AbstractValidator<SportShoe>
    {
        public SportShoeValidator()
        {
            RuleFor(s => s.Model)
                .NotEmpty();

            RuleFor(s => s.Price)
                .GreaterThan(0);

            RuleFor(s => s.BrandId)
                .GreaterThan(0);

            RuleFor(s => s.GenreId)
                .GreaterThan(0);

            RuleFor(s => s.SportId)
                .GreaterThan(0);
        }
    }
}
