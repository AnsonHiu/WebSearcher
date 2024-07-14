using AutoMapper;
using Data;
using Data.Interfaces;
using FluentValidation;
using MediatR;

namespace Domain.Queries.SearchQuery;

public class KeywordSearchQueryHandler(
    ISearchService searchService,
    IMapper mapper,
    IValidator<KeywordSearchQuery> validator) : IRequestHandler<KeywordSearchQuery, IEnumerable<KeywordSearchQueryResult>>
{
    private readonly ISearchService _searchService = searchService;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<KeywordSearchQuery> _validator = validator;

    public async Task<IEnumerable<KeywordSearchQueryResult>> Handle(KeywordSearchQuery query, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(query);
        var results = await _searchService.Search(_mapper.Map<SearchParams>(query), cancellationToken);
        return _mapper.Map<IEnumerable<KeywordSearchQueryResult>>(results);
    }
}
