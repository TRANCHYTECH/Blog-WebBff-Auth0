using Microsoft.Extensions.Hosting;

namespace Tranchy.Common
{
    public static class HostBuilderExtensions
    {
        public static async Task<int> RunWithCustomGraphQlCommandsAsync(this IHost host, string[] args)
        {
            var customArgs = args.IsGraphQlCommand() ? args.SkipWhile(a => !a.Equals("schema", StringComparison.Ordinal)).ToArray() : args;
            return await host.RunWithGraphQLCommandsAsync(customArgs);
        }

        public static bool IsGraphQlCommand(this string[] args) => args.Contains("schema");
    }
}
