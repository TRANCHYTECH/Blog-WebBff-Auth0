using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using MongoDB.Entities;
using Tranchy.QuestionModule.Data;

namespace Tranchy.QuestionModule.Queries;

[QueryType]
public static class QuestionCategoryQueries
{
    [UsePaging]
    [UseSorting]
    [UseFiltering]
    public static IExecutable<QuestionCategory> GetQuestionCategories() => DB.Collection<QuestionCategory>().AsExecutable();

    [DataLoader]
    public static async Task<IReadOnlyDictionary<string, QuestionCategory>> GetQuestionCategoriesByKeys(IReadOnlyList<string> keys,
        CancellationToken cancellationToken)
    {
        var categories = await DB.Find<QuestionCategory>()
            .Match(c => keys.Contains(c.Key)).ExecuteAsync(cancellationToken);

        return categories.ToDictionary(c => c.Key);
    }
}