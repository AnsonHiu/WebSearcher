using Data.Interfaces;
using Data.Options;
using Google;
using Google.Apis.CustomSearchAPI.v1;
using Google.Apis.CustomSearchAPI.v1.Data;
using Google.Apis.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;

namespace Data.Services;

/// <summary>
/// Query Service using Google Custom Search API
/// </summary>
public class GoogleSearchService(ILogger<GoogleSearchService> logger, IOptions<GoogleCustomSearchApiOptions> options) : ISearchService
{
    private readonly ILogger<GoogleSearchService> _logger = logger;
    private readonly GoogleCustomSearchApiOptions _googleSearchConfig = options.Value;
    // Currently max page size allowed by Google Custom Search Api is 10
    private const int DefaultPageSize = 10;

    public async Task<IEnumerable<Result>> Search(string searchTerms, int returnCount, CancellationToken cancellationToken)
    {
        IList<Result> searchResults = [];
        try
        {
            var service = new CustomSearchAPIService(new BaseClientService.Initializer
            {
                ApplicationName = _googleSearchConfig.ApplicationName,
                ApiKey = _googleSearchConfig.ApiKey
            });
            var request = service.Cse.List();
            request.Cx = _googleSearchConfig.SearchEngineId;
            request.Q = searchTerms;

            var pageCount = 0;
            while (pageCount * DefaultPageSize < returnCount)
            {
                var currentItemsCount = pageCount * DefaultPageSize;
                request.Start = currentItemsCount + 1;
                request.Num = IsRemainingItemsMoreThanDefaultPageSize(currentItemsCount, returnCount, out var remainingItemCount)
                    ? DefaultPageSize
                    : remainingItemCount;
                var search = await request.ExecuteAsync(cancellationToken);
                if (search.Items != null)
                {
                    searchResults = [.. searchResults, .. search.Items];
                }
                pageCount++;
            }
            return searchResults;
        }
        catch (GoogleApiException ex)
        {
            if(ex.HttpStatusCode == HttpStatusCode.TooManyRequests)
            {
                _logger.LogError("Google search failed for hitting the api limit.");
            }
            else
            {
                _logger.LogError(ex, "Google search failed after retrieving [{resultsCount}] sites.", searchResults.Count);
            }
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error has occurred while querying with Google.");
            throw;
        }
    }

    private static bool IsRemainingItemsMoreThanDefaultPageSize(int currentItemsCount, int returnCount, out int remainingItemCount)
    {
        remainingItemCount = returnCount - currentItemsCount;
        return remainingItemCount >= DefaultPageSize;
    }
}