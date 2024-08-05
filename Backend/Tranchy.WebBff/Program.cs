using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.HttpOverrides;
using Tranchy.Common;
using Tranchy.WebBff;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var openIdConnectSettings = new OpenIdConnectSettings();
builder.Configuration.GetRequiredSection("Authentication:Schemes:Auth0").Bind(openIdConnectSettings);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    options.DefaultSignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.Cookie.Name = "__Host.Web.Ask";
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
}).AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    options.Authority = openIdConnectSettings.Authority;
    // confidential client using code flow + PKCE
    options.ClientId = openIdConnectSettings.ClientId;
    options.ClientSecret = openIdConnectSettings.ClientSecret;
    options.ResponseType = "code";
    options.ResponseMode = "query";
    options.MapInboundClaims = false;
    options.GetClaimsFromUserInfoEndpoint = true;
    options.SaveTokens = true;
    // request scopes + refresh tokens
    options.Scope.Clear();
    foreach (string scope in openIdConnectSettings.Scopes)
    {
        options.Scope.Add(scope);
    }

    options.Events = new OpenIdConnectEvents
    {
        OnRedirectToIdentityProvider = context =>
        {
            context.ProtocolMessage.SetParameter("audience", openIdConnectSettings.ValidAudiences[0]);
            return Task.FromResult(0);
        },
        OnRedirectToIdentityProviderForSignOut = context =>
        {
            string logoutUri = $"{openIdConnectSettings.Authority}/v2/logout?client_id={openIdConnectSettings.ClientId}";

            string? postLogoutUri = context.Properties.RedirectUri;
            if (!string.IsNullOrEmpty(postLogoutUri))
            {
                if (string.Equals(postLogoutUri, "/agency-portal", StringComparison.Ordinal))
                {
                    postLogoutUri = configuration.GetValue<string>("AgencyPortalSpaUrl")!;
                }

                logoutUri += $"&returnTo={Uri.EscapeDataString(postLogoutUri)}";
            }

            context.Response.Redirect(logoutUri);
            context.HandleResponse();

            return Task.CompletedTask;
        },
    };
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Auth0Policy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "write:users");
    });

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

builder.Services.AddBff(options =>
{
    options.BackchannelLogoutAllUserSessions = true;
    options.EnableSessionCleanup = true;
});

var app = builder.Build();

app.UseHeaderPropagation();
app.UseForwardedHeaders();

app.UseCors();

app.UseAuthentication();
app.UseBff();
app.UseAuthorization();
app.MapBffManagementEndpoints();

if (configuration.GetValue<bool>("EnableBananaCakePop"))
{
    app.MapBananaCakePop("/api/graphql/ui").AllowAnonymous();
}
app.MapGraphQLHttp("/api/graphql").RequireAuthorization();

await app.RunWithCustomGraphQlCommandsAsync(args);