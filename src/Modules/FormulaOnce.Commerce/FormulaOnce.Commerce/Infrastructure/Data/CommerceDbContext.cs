using System.Reflection;
using FormulaOnce.Commerce.Domain;
using FormulaOnce.Commerce.Domain.Cart;
using FormulaOnce.Commerce.Domain.Customer;
using FormulaOnce.Commerce.Domain.Order;
using FormulaOnce.Commerce.Domain.Product;
using Microsoft.EntityFrameworkCore;

namespace FormulaOnce.Commerce.Infrastructure.Data;

public class CommerceDbContext(DbContextOptions<CommerceDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Commerce");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}