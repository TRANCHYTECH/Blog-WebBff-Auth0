using System.Security.Claims;
using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using Microsoft.Extensions.Logging;
using MongoDB.Entities;
using Tranchy.Common;
using Tranchy.Common.HotChocolate;
using Tranchy.QuestionModule.Data;
using Tranchy.QuestionModule.Inputs;
using Tranchy.QuestionModule.Outputs;
using Tranchy.QuestionModule.Subscriptions;

namespace Tranchy.QuestionModule.Mutations;

[MutationType]
public class CreateQuestionMutation
{
    // [Mobile]
    [Tag("mobile")]
    [Error<NotFoundCategoryException>]
    public async Task<Question> CreateQuestion(CreateQuestionInput input,
        ClaimsPrincipal principal,
        [Service] ITopicEventSender sender,
        [Service] ILogger<CreateQuestionMutation> logger)
    {
        var foundCategories = await DB.Find<QuestionCategory, string>()
            .Match(c => input.CategoryKeys.Contains(c.Key))
            .Project(c => c.Key)
            .ExecuteAsync();
        var notFoundCategories = input.CategoryKeys.Except(foundCategories).ToArray();
        if (notFoundCategories.Length != 0)
        {
            throw new NotFoundCategoryException(notFoundCategories);
        }

        var newQuestion = new Question
        {
            Title = input.Title,
            CreatedBy = principal.UserName(),
            SupportLevel = input.SupportLevel,
            PriorityKey = input.PriorityKey,
            CategoryKeys = input.CategoryKeys,
            CommunityShareAgreement = input.CommunityShareAgreement,
        };
        await DB.InsertAsync(newQuestion);

        logger.LogInformation("new question created");

        await sender.SendAsync(nameof(QuestionSubscriptions.QuestionCreated), newQuestion);

        return newQuestion;
    }
}