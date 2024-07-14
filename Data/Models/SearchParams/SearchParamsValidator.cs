using FluentValidation;

namespace Data.Models.SearchParams;

public class SearchParamsValidator : AbstractValidator<SearchParams>
{
    public SearchParamsValidator()
    {
        RuleFor(s => s.ReturnCount).InclusiveBetween(1, 100);
        RuleFor(s => s.Keywords).NotEmpty();
    }
}