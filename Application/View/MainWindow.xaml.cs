using Application.Models;
using AutoMapper;
using Domain.Queries.SearchQuery;
using Domain.Services;
using MediatR;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Application;

public partial class MainWindow: Window, INotifyPropertyChanged
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private string _errorMessage = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<UrlLocation> SearchResults { get; set; } = [];
    public string Keyword { get; set; } = string.Empty;
    public string MatchingUrl { get; set; } = string.Empty;
    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            if(PropertyChanged is not null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ErrorMessage)));
            }
        }
    }

    //public ICommand SimpleCommand = 

    public MainWindow(IMediator mediator, IMapper mapper)
    {
        InitializeComponent();
        _mediator = mediator;
        _mapper = mapper;
        DataContext = this;
    }

    // TODO: Unit Tests
    // TODO: Google region set to AU
    // TODO: Check out ViewModel
    // TODO: Check out command for event handling
    // TODO: Loading screen
    // TODO: Consider adding cancel button (cancellation token)
    // Register: Setup AWS Secrets Manager for sensitive config data
    private async void SearchForKeywords(object sender, RoutedEventArgs e)
    {
        ErrorMessage = string.Empty;
        try
        {
            var query = new KeywordSearchQuery(Keyword, 25);
            var searchResults = await _mediator.Send(query, CancellationToken.None);
            var filteredResults = KeywordsFilter.GetMatchedUrl(MatchingUrl, searchResults);
            var mappedFilteredResults = _mapper.Map<IEnumerable<UrlLocation>>(filteredResults.ToList());
            if(SearchResults.Count > 0)
            {
                SearchResults.Clear();
            }
            foreach(var result in mappedFilteredResults)
            {
                SearchResults.Add(result);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }
}