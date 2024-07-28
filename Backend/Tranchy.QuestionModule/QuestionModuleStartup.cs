using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Entities;
using Tranchy.QuestionModule.Data;
using Microsoft.Extensions.Logging;
using MongoDB.Driver.Core.Configuration;

namespace Tranchy.QuestionModule;

public class QuestionModuleStartup : IModuleStartup
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var conventionPack = new ConventionPack { new IgnoreExtraElementsConvention(ignoreExtraElements: true) };

        ConventionRegistry.Register("TranchyAskDefaultConventions", conventionPack, _ => true);

        services.AddGraphQL().AddQuestionModuleTypes();
    }

    public static async Task InitDatabase(IConfiguration configuration)
    {
        MongoClientSettings conn = GetQuestionConnectionString(configuration);

        using var loggerFactory = LoggerFactory.Create(b =>
        {
            b.AddSimpleConsole();
            b.AddConfiguration(configuration.GetSection("Logging"));
            b.SetMinimumLevel(LogLevel.Debug);
        });
        conn.LoggingSettings = new LoggingSettings(loggerFactory);

        await DB.InitAsync("question", conn);
        DB.DatabaseFor<Question>("question");
        DB.DatabaseFor<QuestionCategory>("question");
        DB.DatabaseFor<QuestionPriority>("question");
    }

    public static async Task MigrateDatabase(IServiceProvider serviceProvider)
    {
        await SampleCoreData.Seed();
    }

    private static MongoClientSettings GetQuestionConnectionString(IConfiguration configuration)
    {
        return MongoClientSettings.FromConnectionString(configuration.GetConnectionString("Question"));
    }
}