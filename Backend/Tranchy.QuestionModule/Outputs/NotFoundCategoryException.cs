namespace Tranchy.QuestionModule.Outputs;

public class NotFoundCategoryException(string[] categoryKeys) : Exception
{
    public string[] CategoryKeys { get; } = categoryKeys;
}