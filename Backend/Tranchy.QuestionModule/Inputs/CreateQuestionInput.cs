using Tranchy.QuestionModule.Data;

namespace Tranchy.QuestionModule.Inputs;

public record CreateQuestionInput(
    string Title,
    SupportLevel SupportLevel,
    string? PriorityKey,
    string[] CategoryKeys,
    bool? CommunityShareAgreement
);
