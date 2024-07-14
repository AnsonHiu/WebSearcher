using Data.Interfaces;
using Data.Options;
using Google.Apis.CustomSearchAPI.v1;
using Google.Apis.Services;
using Microsoft.Extensions.Options;

namespace Data.Services.Google;

public class GoogleApiFactory(IOptions<GoogleCustomSearchApiOptions> options) : IGoogleApiFactory
{
    private readonly GoogleCustomSearchApiOptions _googleSearchConfig = options.Value;

    public CustomSearchAPIService CreateCustomSearchAPIService()
    {
        return new(new BaseClientService.Initializer
        {
            ApplicationName = _googleSearchConfig.ApplicationName,
            ApiKey = _googleSearchConfig.ApiKey
        });
    }
}