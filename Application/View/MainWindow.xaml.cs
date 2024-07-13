using Application.Models;
using AutoMapper;
using Domain.Queries.SearchQuery;
using Domain.Services;
using MediatR;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Application;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
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

    // TODO: Test Dto mapping
    // TODO: max count returns more than max count
    // TODO: Unit Tests
    // TODO: Check out ViewModel
    // TODO: Check out command for event handling
    // TODO: Google region set to AU
    // TODO: Consider adding cancel button (cancellationi token)
    // TODO: Loading screen
    // Register: Setup AWS Secrets Manager for sensitive config data
    private async void SearchForKeywords(object sender, RoutedEventArgs e)
    {
        ErrorMessage = string.Empty;
        try
        {
            var query = new KeywordSearchQuery(Keyword, 1);
            var searchResults = await _mediator.Send(query, CancellationToken.None);
            var filteredResults = KeywordsFilter.GetMatchedUrl(MatchingUrl, searchResults);
            var mappedFilteredResults = _mapper.Map<IEnumerable<UrlLocation>>(filteredResults);
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