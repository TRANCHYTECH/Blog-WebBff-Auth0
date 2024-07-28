using HotChocolate;
using HotChocolate.Types;
using MongoDB.Entities;
using Tranchy.Common.Data;

namespace Tranchy.QuestionModule.Data;

[Collection("QuestionCategories")]
public class QuestionCategory : EntityBase
{
    public required string Key { get; init; }

    public required LocalizedString Title { get; init; }

    [GraphQLIgnore]
    public required LocalizedString Description { get; init; }
}

[ObjectType]
public class QuestionCategoryType : ObjectType<QuestionCategory>
{
    protected override void Configure(IObjectTypeDescriptor<QuestionCategory> descriptor)
    {
        descriptor.Ignore(f => f.HasDefaultID());
    }
}

[ExtendObjectType<QuestionCategory>(IgnoreProperties = [nameof(QuestionCategory.HasDefaultID)])]
public class QuestionCategoryTypeExtensions;