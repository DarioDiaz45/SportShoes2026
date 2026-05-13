using FluentValidation;
using SportShoes2026.Entities;

namespace SportShoes2026.Service.Validator
{
    public class SizeValidator:AbstractValidator<SiZe>
    {
        public SizeValidator()
        {
            RuleFor(s => s.SizeNumber)
                .GreaterThan(0);
        }
    }
}
