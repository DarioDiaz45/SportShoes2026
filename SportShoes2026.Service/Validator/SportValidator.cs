using FluentValidation;
using SportShoes2026.Entities;

namespace SportShoes2026.Service.Validator
{
    public class SportValidator : AbstractValidator<Sport>
    {
        public SportValidator()
        {
            RuleFor(s => s.SportName)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
