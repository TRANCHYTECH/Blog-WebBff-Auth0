using HotChocolate;
using HotChocolate.Types;
using Tranchy.Common.HotChocolate;
using Tranchy.QuestionModule.Data;

namespace Tranchy.QuestionModule.Subscriptions;

[SubscriptionType]
[Web]
public static class QuestionSubscriptions
{
    [Subscribe]
    public static Question QuestionCreated([EventMessage] Question question) => question;
}