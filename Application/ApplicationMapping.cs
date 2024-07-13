using Application.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

/// <summary>
/// Maps between Domain Models and Application Models
/// </summary>
class ApplicationMapping : Profile
{
    public ApplicationMapping()
    {
        CreateMap<MatchedUrlLocation, UrlLocation>()
            .ForCtorParam(nameof(UrlLocation.Location), opt => opt.MapFrom(src => src.Location.ToString()));
    }
}
