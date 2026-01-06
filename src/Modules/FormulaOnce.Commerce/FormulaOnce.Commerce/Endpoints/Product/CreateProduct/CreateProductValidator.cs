using FastEndpoints;
using FluentValidation;

namespace FormulaOnce.Commerce.Endpoints.Product.CreateProduct;

public class CreateProductValidator : Validator<CreateProductRequest>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(200);
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be a positive value.");
        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).WithMessage("Initial stock cannot be negative.");
    }
}