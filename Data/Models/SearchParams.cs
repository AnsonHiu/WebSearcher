using FluentValidation;

namespace Data.Models;

/// <param name="Keywords">Query used for Googling</param>
/// <param name="ReturnCount">Number of results wanted</param>
public record SearchParams
(
    string Keywords,
    int ReturnCount
);

public class SearchParamsValidator: AbstractValidator<SearchParams>
{
    public SearchParamsValidator()
    {
        RuleFor(s => s.ReturnCount).InclusiveBetween(1, 100);
        RuleFor(s => s.Keywords).NotEmpty();
    }
}