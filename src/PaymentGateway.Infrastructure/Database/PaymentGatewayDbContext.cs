using Microsoft.EntityFrameworkCore;
using PaymentGateway.Infrastructure.Database.Entities;

namespace PaymentGateway.Infrastructure.Database;

/// <summary>
/// Represents the database context for the Payments.
/// </summary>
public class PaymentGatewayDbContext : DbContext
{
    /// <summary>
    /// Configures the database options.
    /// </summary>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseInMemoryDatabase(databaseName: "Payments");

    /// <summary>
    /// Payment entities in the database.
    /// </summary>
    public DbSet<Payment> Payments { get; set; }
}