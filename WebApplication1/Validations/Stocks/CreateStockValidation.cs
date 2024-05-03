using FluentValidation;
using WebApplication1.Dtos.Stock;

namespace WebApplication1.Validations.Stocks;

public class CreateStockValidation : AbstractValidator<CreateStockRequest>
{
    public CreateStockValidation()
    {
        this.RuleFor(model => model.Symbol).NotEmpty().WithMessage("Symbol is mandatory field");
    }
}