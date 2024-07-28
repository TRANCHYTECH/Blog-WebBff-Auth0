using HotChocolate;
using MongoDB.Entities;
using Tranchy.Common;
using Tranchy.Common.Data;
using Tranchy.Common.HotChocolate;

namespace Tranchy.QuestionModule.Data;

[Collection("Questions")]
public class Question : EntityBase, IOwnEntity, IQueryIndex
{
    public required string Title { get; set; }

    public required SupportLevel SupportLevel { get; set; }

    public QuestionStatus Status { get; private set; } = QuestionStatus.New;

    public string? PriorityKey { get; set; }

    public string[] CategoryKeys { get; set; } = [];

    public bool? CommunityShareAgreement { get; set; }

    [Web]
    public QuestionConsultant? Consultant { get; private set; }

    [Ignore]
    public QuestionPermissions? Permissions { get; private set; }

    public string Comment { get; set; } = string.Empty;

    public required string CreatedBy { get; init; }

    [GraphQLIgnore]
    public long QueryIndex { get; init; }

    public void Approve(string? comment)
    {
        Status = QuestionStatus.Accepted;
        if (!string.IsNullOrEmpty(comment))
        {
            Comment = comment;
        }
    }

    public void Reject(string comment)
    {
        Status = QuestionStatus.Rejected;
        if (!string.IsNullOrEmpty(comment))
        {
            Comment = comment;
        }
    }

    public void TakeConsultation(string userId)
    {
        if (string.Equals(CreatedBy, userId, StringComparison.Ordinal))
        {
            throw new BusinessLogicException("Could not pick yourself");
        }

        if (Status != QuestionStatus.Accepted)
        {
            throw new BusinessLogicException("Invalid status");
        }

        Status = QuestionStatus.InProgress;
        Consultant = new QuestionConsultant { UserId = userId, CreatedAt = DateTime.UtcNow };
    }

    public void FinishConsultation(string userId, string conclusion)
    {
        if (Status != QuestionStatus.InProgress)
        {
            throw new BusinessLogicException("Invalid status");
        }

        if (Consultant is null || !string.Equals(Consultant.UserId, userId, StringComparison.Ordinal))
        {
            throw new BusinessLogicException("Invalid consultant");
        }

        Consultant.Conclusion = conclusion;
        Status = QuestionStatus.Resolved;
    }

    public void RefinePermissions(string userId)
    {
        Permissions = new QuestionPermissions();

        // Actions.
        if (Status == QuestionStatus.Accepted)
        {
            if (!IsRequester(userId))
            {
                Permissions.Actions.Add(QuestionAction.TakeConsultation);
            }
        }
        else if (Status == QuestionStatus.InProgress)
        {
            if (IsConsultant(userId))
            {
                Permissions.DirectChatTargetUserId = CreatedBy;
                Permissions.Actions.Add(QuestionAction.GoToConversation);
            }
            else if (IsRequester(userId))
            {
                Permissions.DirectChatTargetUserId = Consultant?.UserId;
                Permissions.Actions.Add(QuestionAction.GoToConversation);
            }
        }
    }

    private bool IsRequester(string userId) => string.Equals(CreatedBy, userId, StringComparison.Ordinal);

    private bool IsConsultant(string userId) => string.Equals(Consultant?.UserId, userId, StringComparison.Ordinal);
}
