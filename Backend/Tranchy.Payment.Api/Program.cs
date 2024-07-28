using Microsoft.AspNetCore.Authentication.JwtBearer;
using Tranchy.Common;
using Tranchy.PaymentModule;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
services.AddGraphQLServer()
    .AllowIntrospection(allow: true)
    .AddMutationConventions()
    .AddSorting()
    .AddFiltering()
    .AddAuthorization();

PaymentModuleStartup.ConfigureServices(services, configuration);

var app = builder.Build();

if (configuration.GetValue<bool>("ApplyMigrationsOnStartup"))
{
    await using var scope = app.Services.CreateAsyncScope();
    await PaymentModuleStartup.MigrateDatabase(scope.ServiceProvider);
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

if (configuration.GetValue<bool>("EnableBananaCakePop"))
{
    app.MapBananaCakePop("/api/graphql/ui").AllowAnonymous();
}
app.MapGraphQLHttp("/api/graphql").RequireAuthorization();

await app.RunWithCustomGraphQlCommandsAsync(args);
