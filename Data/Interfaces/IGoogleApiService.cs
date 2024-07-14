using Data.Models;
using Google.Apis.CustomSearchAPI.v1.Data;

namespace Data.Interfaces;
public interface IGoogleApiService
{
    void Dispose();
    Task<Search> ExecuteSearchAsync(GoogleSearchRequest request, CancellationToken cancellationToken);
}
