using Data.Common.Extensions;
using Data.Interfaces;
using Data.Models;
using Data.Options;
using Google.Apis.CustomSearchAPI.v1;
using Google.Apis.CustomSearchAPI.v1.Data;
using Microsoft.Extensions.Options;
using System.ComponentModel;

namespace Data.Services.Google;

public class GoogleApiService(IOptions<GoogleCustomSearchApiOptions> options, IGoogleApiFactory googleApiFactory) : IGoogleApiService, IDisposable
{
    private readonly GoogleCustomSearchApiOptions _googleSearchConfig = options.Value;
    private readonly IGoogleApiFactory _googleApiFactory = googleApiFactory;
    private CustomSearchAPIService? _customSearchApiService;

    /// <summary>
    /// Configuration of google custom search API found here
    /// https://developers.google.com/custom-search/v1/reference/rest/v1/cse/list#request
    /// </summary>
    public async Task<Search> ExecuteSearchAsync(GoogleSearchRequest request, CancellationToken cancellationToken)
    {
        _customSearchApiService ??= _googleApiFactory.CreateCustomSearchAPIService();
        var listRequest = _customSearchApiService.Cse.List();
        listRequest.Cx = _googleSearchConfig.SearchEngineId;
        listRequest.Q = request.Keyword;
        listRequest.Start = request.Skip;
        listRequest.Num = request.FetchCount;

        if (Enum.TryParse(_googleSearchConfig.SearchCountry, out GlCountry searchRegion)
            && searchRegion.GetDescription() is string description
            && description is not null)
        {
            listRequest.Gl = description;
        }
        return await listRequest.ExecuteAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _customSearchApiService?.Dispose();
            _customSearchApiService = null;
        }
    }

    /// <summary>
    /// See https://developers.google.com/custom-search/docs/json_api_reference#countryCollections for a full list of values
    /// </summary>
    private enum GlCountry
    {
        [Description("au")]
        Australia
    }
}
