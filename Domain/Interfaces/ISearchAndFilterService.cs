using Domain.Entities;

namespace Domain.Interfaces;

public interface ISearchAndFilterService
{
    Task<IEnumerable<UrlLocation>> SearchAndFilter(string keyword, string urlToMatch, CancellationToken cancellationToken);
}