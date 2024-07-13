using Google.Apis.CustomSearchAPI.v1.Data;

namespace Data.Interfaces;
public interface ISearchService
{
    public Task<IEnumerable<Result>> Search(string searchTerms, int maxReturnResultsCount, CancellationToken cancellationToken);
}

