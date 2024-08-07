using Microsoft.AspNetCore.Authentication.JwtBearer;
using StackExchange.Redis;
using Tranchy.Common;
using Tranchy.QuestionModule;
using Tranchy.UserModule;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services
    .AddSingleton(_ => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!))
    .AddHttpContextAccessor()
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services
    .AddGraphQLServer()
    .AddInMemorySubscriptions()
    .AddRedisSubscriptions(sp => sp.GetRequiredService<ConnectionMultiplexer>())
    .AddMutationConventions()
    .AddMongoDbSorting()
    .AddMongoDbFiltering()
    .AddMongoDbPagingProviders()
    .AllowIntrospection(allow: true)
    .AddAuthorization();

QuestionModuleStartup.ConfigureServices(builder.Services, configuration);
UserModuleStartup.ConfigureServices(builder.Services, configuration);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader().AllowAnyMethod().AllowCredentials()));
}

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

if (configuration.GetValue<bool>("EnableBananaCakePop"))
{
    app.MapBananaCakePop("/api/graphql/ui").AllowAnonymous();
}
app.MapGraphQLHttp("/api/graphql").RequireAuthorization();

var integrationGroupBuilder = app.MapGroup("/api/integration");
integrationGroupBuilder.MapIntegrationEndpoints<UserModuleStartup>().RequireAuthorization();

if (!args.IsGraphQlCommand())
{
    await QuestionModuleStartup.InitDatabase(configuration);
    await UserModuleStartup.InitDatabase(configuration);
}

if (app.Configuration.GetValue<bool>("ApplyMigrationsOnStartup"))
{
    await using var scope = app.Services.CreateAsyncScope();
    await QuestionModuleStartup.MigrateDatabase(scope.ServiceProvider);
    await UserModuleStartup.MigrateDatabase(scope.ServiceProvider);
}

await app.RunWithCustomGraphQlCommandsAsync(args);
