using FormulaOnce.Commerce.Endpoints.Customer.UpdateCustomerProfile;
using FormulaOnce.Commerce.Infrastructure.Data;
using FormulaOnce.Commerce.Infrastructure.Repositories;
using FormulaOnce.Commerce.Infrastructure.Repositories.Cart;
using FormulaOnce.Commerce.Infrastructure.Repositories.Customer;
using FormulaOnce.Commerce.Infrastructure.Repositories.Order;
using FormulaOnce.Commerce.Infrastructure.Repositories.Product;
using FormulaOnce.Commerce.Services;
using FormulaOnce.Commerce.Services.Cart;
using FormulaOnce.Commerce.Services.Order;
using FormulaOnce.Commerce.Services.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace FormulaOnce.Commerce;

public static class FormulaOnceCommerceExtensions
{
    public static IServiceCollection AddFormulaOnceCommerce(this IServiceCollection services,
        ConfigurationManager configuration, ILogger logger)
    {
        services.AddDbContext<CommerceDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("CommerceDatabase") ??
                                   throw new InvalidOperationException(
                                       "Connection string 'CommerceDatabase' not found.");

            options.UseSqlServer(connectionString);
        });

        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ICustomerService, CustomerService>();

        logger.Information("::{module} module services registered", "Commerce");

        return services;
    }
}