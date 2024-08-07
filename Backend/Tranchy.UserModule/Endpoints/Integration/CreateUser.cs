using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Tranchy.Common.Constants;
using Tranchy.UserModule.Requests;

namespace Tranchy.UserModule.Endpoints.Integration;

public class CreateUser : IEndpoint
{
    public static void Register(RouteGroupBuilder routeGroupBuilder) => routeGroupBuilder
        .MapPost("oauth0/users", CreateUserAuth0Action)
        .RequireAuthorization(AuthPolicyNames.CreateUserPolicy)
        .WithName("CreateUserAction")
        .WithSummary("Create user action")
        .WithTags(Tags.Integration)
        .WithOpenApi();

    private static async Task<Results<Accepted, BadRequest<string>>> CreateUserAuth0Action(
        [FromBody] CreateUserActionRequest request,
        [FromServices] ILogger<CreateUser> logger,
        CancellationToken cancellationToken)
    {
        logger.ReceivedCreatingUserAction(request.UserId, request.Email);

        // Could reuse object id from original source as id of entity User
        (string providerId, string userId) = request.ParseUserId();
        try
        {
            var userEntity = new Data.User
            {
                ID = userId,
                Email = request.Email,
                UserName = request.Email,
                ProviderId = providerId
            };

            await DB.InsertAsync(userEntity, cancellation: cancellationToken);

            logger.CreatedUser(userEntity.ID, userEntity.UserName);
        }
        catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
        {
            logger.DuplicatedUser(ex);

            return TypedResults.BadRequest("DuplicatedUserIdOrEmail");
        }

        return TypedResults.Accepted(request.UserId);
    }
}