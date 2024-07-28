namespace Tranchy.Common;

public interface ITenant
{
    string UserId { get; }

    string Email { get; }
}
