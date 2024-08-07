using Microsoft.AspNetCore.Routing;

namespace Tranchy.Common;

public static class RouteGroupBuilderExtensions
{
    public static RouteGroupBuilder MapMobileEndpoints<TModule>(this RouteGroupBuilder group)
        where TModule : class, IModuleStartup =>
        group.MapEndpointsInternal<TModule>("Mobile");

    public static RouteGroupBuilder MapBackOfficeEndpoints<TModule>(this RouteGroupBuilder group)
        where TModule : class, IModuleStartup =>
        group.MapEndpointsInternal<TModule>("BackOffice");

    public static RouteGroupBuilder MapIntegrationEndpoints<TModule>(this RouteGroupBuilder group)
        where TModule : class, IModuleStartup =>
        group.MapEndpointsInternal<TModule>("Integration");

    private static RouteGroupBuilder MapEndpointsInternal<TModule>(this RouteGroupBuilder group, string target)
        where TModule : class, IModuleStartup
    {
        var endpoints = typeof(TModule).Assembly.GetTypes()
            .Where(t =>
                t.IsClass &&
                t.IsAssignableTo(typeof(IEndpoint)) &&
                t.Namespace?.EndsWith(target, StringComparison.Ordinal) == true);

        foreach (var endpoint in endpoints)
        {
            var registerRoutesMethod = endpoint.GetMethod(nameof(IEndpoint.Register));
            registerRoutesMethod?.Invoke(null, [group]);
        }

        return group;
    }
}
