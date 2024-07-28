using Microsoft.EntityFrameworkCore;

namespace Tranchy.PaymentModule.Data;

public class PaymentDbContext(DbContextOptions options) : DbContext(options)
{
    public const string DbSchema = "payment";

    public DbSet<Deposit> Deposits => Set<Deposit>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DbSchema);
        modelBuilder.ApplyConfiguration(new DepositEntityTypeConfiguration());
    }
}
