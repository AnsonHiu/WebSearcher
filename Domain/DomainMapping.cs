using AutoMapper;
using Domain.Queries.SearchQuery;
using Google.Apis.CustomSearchAPI.v1.Data;

namespace Domain;
public class DomainMapping : Profile
{
    /// <summary>
    /// Maps between Domain Models and Data Models
    /// </summary>
    public DomainMapping()
    {
        CreateMap<Result, KeywordSearchQueryResult>()
            .ForCtorParam(nameof(KeywordSearchQueryResult.BaseUrl), options => options.MapFrom(source => source.DisplayLink))
            .ForCtorParam(nameof(KeywordSearchQueryResult.FullUrl), options => options.MapFrom(source => source.Link));
    }
}