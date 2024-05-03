using FluentValidation;
using WebApplication1.Dtos.Stock;

namespace WebApplication1.Validations.Stocks;

public class CreateStockValidation : AbstractValidator<CreateStockRequest>
{
    public CreateStockValidation()
    {
        this.RuleFor(model => model.Symbol).NotEmpty().WithMessage("Symbol is mandatory field");
        this.RuleFor(model => model.Industry).NotEmpty().WithMessage("Industry is mandatory field");
        this.RuleFor(model => model.Purchase).NotEmpty().WithMessage("Purchase is mandatory field");
        this.RuleFor(model => model.CompanyName).NotEmpty().WithMessage("Company Name is mandatory field");
        this.RuleFor(model => model.LastDiv).NotEmpty().WithMessage("Last div is mandatory field");
        this.RuleFor(model => model.MarketCap).NotEmpty().WithMessage("Market cap is mandatory field");
    }
}