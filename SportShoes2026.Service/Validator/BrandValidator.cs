using FluentValidation;
using SportShoes2026.Entities;

namespace SportShoes2026.Service.Validator
{
    public class BrandValidator:AbstractValidator<Brand>
    {
        public BrandValidator()
        {
            RuleFor(b => b.BrandName)
                .NotEmpty()
                .MaximumLength(50);

           
        }
    }
}
