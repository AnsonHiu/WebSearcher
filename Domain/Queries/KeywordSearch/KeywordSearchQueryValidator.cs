using FluentValidation;

namespace Domain.Queries.SearchQuery;

public class KeywordSearchQueryValidator : AbstractValidator<KeywordSearchQuery>
{
    public KeywordSearchQueryValidator()
    {
        RuleFor(q => q.Keywords).NotEmpty();
    }
}
