using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tranchy.PaymentModule.Data;

namespace Tranchy.PaymentModule;

public class PaymentModuleStartup : IModuleStartup
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddPooledDbContextFactory<PaymentDbContext>(SetupDbContext)
            .AddScoped(provider => provider.GetRequiredService<IDbContextFactory<PaymentDbContext>>().CreateDbContext());

        services.AddGraphQL()
            .RegisterDbContext<PaymentDbContext>(DbContextKind.Pooled)
            .AddPaymentModuleTypes();
    }

    private static void SetupDbContext(IServiceProvider serviceProvider, DbContextOptionsBuilder options)
    {
        string? connectionString = serviceProvider.GetRequiredService<IConfiguration>().GetConnectionString("Payment");
        options.UseSqlServer(connectionString, sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(3);
            sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", PaymentDbContext.DbSchema);
            sqlOptions.MigrationsAssembly(typeof(PaymentDbContext).Assembly.FullName);
        });

        if (serviceProvider.GetRequiredService<IHostEnvironment>().IsDevelopment())
        {
            options.EnableSensitiveDataLogging().EnableDetailedErrors();
        }
    }

    public static async Task MigrateDatabase(IServiceProvider serviceProvider)
    {
        var factory = serviceProvider.GetRequiredService<IDbContextFactory<PaymentDbContext>>();
        await using var context = await factory.CreateDbContextAsync();
        await context.Database.MigrateAsync().ConfigureAwait(false);
    }
}
