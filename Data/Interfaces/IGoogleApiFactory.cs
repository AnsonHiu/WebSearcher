using Google.Apis.CustomSearchAPI.v1;

namespace Data.Interfaces;

/// <summary>
/// Returns instances of google api services
/// </summary>
public interface IGoogleApiFactory
{
    CustomSearchAPIService CreateCustomSearchAPIService();
}