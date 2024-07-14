using Data.Interfaces;
using Data.Models;
using Data.Models.SearchParams;
using FluentValidation;
using Google;
using Google.Apis.CustomSearchAPI.v1.Data;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Data.Services.Google;

/// <summary>
/// Query Service using Google Custom Search API
/// </summary>
public class GoogleSearchService(ILogger<GoogleSearchService> logger, IGoogleApiService googleApiService, IValidator<SearchParams> searchParamsValidator) : ISearchService
{
    private readonly ILogger<GoogleSearchService> _logger = logger;
    private readonly IGoogleApiService _googleApiService = googleApiService;
    private readonly IValidator<SearchParams> _validator = searchParamsValidator;

    // Currently max page size allowed by Google Custom Search Api is 10
    public const int DefaultPageSize = 10;

    /// <summary>
    /// Use this method to programatically query google
    /// </summary>
    public async Task<IEnumerable<Result>> Search(SearchParams searchParams, CancellationToken cancellationToken)
    {
        List<Result> searchResults = [];
        try
        {
            _validator.ValidateAndThrow(searchParams);
            var pageCount = 0;
            while (pageCount * DefaultPageSize < searchParams.ReturnCount)
            {
                var currentItemsCount = pageCount * DefaultPageSize;
                var firstItemIndex = currentItemsCount + 1;
                var fetchCount = IsRemainingItemsMoreThanDefaultPageSize(currentItemsCount, searchParams.ReturnCount, out var remainingItemCount)
                    ? DefaultPageSize
                    : remainingItemCount;

                var googleSearchRequest = new GoogleSearchRequest(searchParams.Keywords, firstItemIndex, fetchCount);

                var search = await _googleApiService.ExecuteSearchAsync(googleSearchRequest, cancellationToken);
                if (search.Items is not null)
                {
                    searchResults = [.. searchResults, .. search.Items];
                }
                pageCount++;
            }
            return searchResults;
        }
        catch (ValidationException)
        {
            throw;
        }
        catch (GoogleApiException ex)
        {
            if (ex.HttpStatusCode == HttpStatusCode.TooManyRequests)
            {
                _logger.LogError("Google search failed for hitting the api request limit.");
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
        finally
        {
            _googleApiService.Dispose();
        }
    }

    private static bool IsRemainingItemsMoreThanDefaultPageSize(int currentItemsCount, int returnCount, out int remainingItemCount)
    {
        remainingItemCount = returnCount - currentItemsCount;
        return remainingItemCount >= DefaultPageSize;
    }
}