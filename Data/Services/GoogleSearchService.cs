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
public class GoogleSearchService(ILogger<GoogleSearchService> logger, IOptions<GoogleCustomSearchApiOptions> options) : ISearchService
{
    private readonly ILogger<GoogleSearchService> _logger = logger;
    private readonly GoogleCustomSearchApiOptions _googleSearchConfig = options.Value;

    public async Task<IEnumerable<Result>> Search(string searchTerms, int maxPagesQueried, CancellationToken cancellationToken)
    {
        IList<Result> results = [];
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

            IList<Result> paging = [];
            var count = 0;
            while (paging != null && count * 10 < maxPagesQueried)
            {
                request.Start = count * 10 + 1;
                var search = await request.ExecuteAsync(cancellationToken);
                paging = search.Items;
                if (paging != null)
                {
                    results = [.. results, .. paging];
                }
                count++;
            }
            return results;
        }
        catch (GoogleApiException ex)
        {
            if(ex.HttpStatusCode == HttpStatusCode.TooManyRequests)
            {
                _logger.LogError("Google search failed for hitting the api limit.");
            }
            else
            {
                _logger.LogError(ex, "Google search failed after retrieving [{resultsCount}] sites", results.Count);
            }
            throw;
        }
    }
}