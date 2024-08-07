using MongoDB.Bson.Serialization.Attributes;

namespace Tranchy.UserModule.Data;

[Collection("UserAction")]
public abstract class UserActionBase : IEntity
{
    [BsonId] public string ID { get; protected init; } = default!;

    public object GenerateNewID() => 1;

    public bool HasDefaultID() => false;
}
