using FluentValidation;

namespace Domain.Queries.SearchQuery;

public class KeywordSearchQueryValidator : AbstractValidator<KeywordSearchQuery>
{
    public KeywordSearchQueryValidator()
    {
        RuleFor(q => q.MaxCount).InclusiveBetween(1, 100);
        RuleFor(q => q.Keyword).NotEmpty();
    }
}
