using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using MongoDB.Entities;
using Tranchy.QuestionModule.Data;

namespace Tranchy.QuestionModule.Queries;

[QueryType]
public static class QuestionQueries
{
    [UseSorting]
    [UseFiltering]
    public static IExecutable<Question> GetQuestions() => DB.Collection<Question>().AsExecutable();
}