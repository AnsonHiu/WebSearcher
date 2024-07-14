using Data.Models.SearchParams;
using Google.Apis.CustomSearchAPI.v1.Data;

namespace Data.Interfaces;

public interface ISearchService
{
    Task<IEnumerable<Result>> Search(SearchParams searchParams, CancellationToken cancellationToken);
}

