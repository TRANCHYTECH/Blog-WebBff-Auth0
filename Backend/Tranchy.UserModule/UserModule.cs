using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Tranchy.UserModule.Data;

namespace Tranchy.UserModule;

public class UserModuleStartup : IModuleStartup
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    public static Task MigrateDatabase(IServiceProvider serviceProvider)
    {
        return Task.CompletedTask;
    }

    public static async Task InitDatabase(IConfiguration configuration)
    {
        var conn = MongoClientSettings.FromConnectionString(configuration.GetConnectionString("Question"));
        var conventionPack = new ConventionPack { new IgnoreExtraElementsConvention(true) };
        ConventionRegistry.Register("TranchyAskDefaultConventions", conventionPack, _ => true);
        await DB.InitAsync("question", conn);
        DB.DatabaseFor<User>("question");
    }
}
