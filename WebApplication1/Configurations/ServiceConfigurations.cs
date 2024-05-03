using FluentValidation;
using FluentValidation.AspNetCore;
using WebApplication1.Dtos.Stock;
using WebApplication1.Interface;
using WebApplication1.Repository;
using WebApplication1.Validations.Stocks;

namespace WebApplication1.Configurations;

public class ServiceConfigurations
{
    public static async Task Configure(IServiceCollection services, IConfiguration configuration)
    {
        // Basic Service Configurations
        services.AddControllers();
        
        // Register FluentValidation validators
        services.AddControllers()
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateStockValidation>());

        
        // Repository Registrations
        RegisterRepositoriesAndValidators(services);
        // For Stocks
        services.AddScoped<IStockRepository, StockRepository>();

        // For Comments
        services.AddScoped<ICommentRepository, CommentRepository>();

        
    }
    private static void RegisterRepositoriesAndValidators(IServiceCollection services)
    {
        // Add validation
        services.AddScoped<AbstractValidator<CreateStockRequest>, CreateStockValidation>();

    }
}