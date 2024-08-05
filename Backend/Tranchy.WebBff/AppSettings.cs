namespace Tranchy.WebBff
{
    public class OpenIdConnectSettings
    {
        public string Authority { get; set; } = default!;

        public string ClientId { get; set; } = default!;

        public string ClientSecret { get; set; } = default!;

        public string[] ValidAudiences { get; set; } = default!;

        public string[] Scopes { get; set; } = default!;

        public string? AuthorizationUrl { get; set; }

        public string? TokenUrl { get; set; }
    }
}
