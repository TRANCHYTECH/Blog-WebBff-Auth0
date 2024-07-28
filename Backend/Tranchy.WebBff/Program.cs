using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Tranchy.Common;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.AddAuthorization();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
    options.ForwardedHeaders = ForwardedHeaders.XForwardedHost | ForwardedHeaders.XForwardedProto);
builder.Services.AddHttpForwarder();

builder.Services
    .AddHttpClient("Fusion")
    .AddHeaderPropagation();

builder.Services
    .AddFusionGatewayServer()
    .ConfigureFromFile("./gateway.fgp");

builder.Services
    .AddHeaderPropagation(c =>
    {
        c.Headers.Add("GraphQL-Preflight");
        c.Headers.Add("Traceparent");
        c.Headers.Add("Authorization");
    });

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader().AllowAnyMethod().AllowCredentials()));
}

var app = builder.Build();

app.UseHeaderPropagation();
app.UseForwardedHeaders();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

if (configuration.GetValue<bool>("EnableBananaCakePop"))
{
    app.MapBananaCakePop("/api/graphql/ui").AllowAnonymous();
}
app.MapGraphQLHttp("/api/graphql").RequireAuthorization();

await app.RunWithCustomGraphQlCommandsAsync(args);