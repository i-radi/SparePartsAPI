using FluentValidation;

namespace SpareParts.Domain;

public class ProductDtoValidator : AbstractValidator<UpdateProductDto>
{
    public ProductDtoValidator()
    {
        RuleFor(s => s.Name).NotNull().NotEmpty().Length(1,50);
        RuleFor(s => s.Price).InclusiveBetween(1,100);
    }
}