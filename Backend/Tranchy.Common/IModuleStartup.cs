using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tranchy.Common;

public interface IModuleStartup
{
    static abstract void ConfigureServices(IServiceCollection services, IConfiguration configuration);
    static abstract Task MigrateDatabase(IServiceProvider serviceProvider);
}
