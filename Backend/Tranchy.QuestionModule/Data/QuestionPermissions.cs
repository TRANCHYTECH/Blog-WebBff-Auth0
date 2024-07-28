using System.Collections.ObjectModel;

namespace Tranchy.QuestionModule.Data;

public class QuestionPermissions
{
    public ICollection<QuestionAction> Actions { get; init; } = new Collection<QuestionAction>();
    public string? DirectChatTargetUserId { get; set; }
}

public enum QuestionAction
{
    TakeConsultation,
    GoToConversation,
}
