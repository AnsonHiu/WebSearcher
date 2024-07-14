using Application.Models;
using AutoMapper;
using Domain.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Application;

public partial class MainWindow: Window, INotifyPropertyChanged
{
    private readonly ISearchAndFilterService _searchAndFilterService;
    private readonly IMapper _mapper;
    private string _errorMessage = string.Empty;
    private string _searchButtonText = "Search";

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
            NotifyPropertyChanged(nameof(ErrorMessage));
        }
    }
    public string SearchButtonText
    {
        get => _searchButtonText;
        set
        {
            _searchButtonText = value;
            NotifyPropertyChanged(nameof(SearchButtonText));
        }
    }

    //public ICommand SimpleCommand = 

    public MainWindow(ISearchAndFilterService searchAndFilterService, IMapper mapper)
    {
        InitializeComponent();
        _searchAndFilterService = searchAndFilterService;
        _mapper = mapper;
        DataContext = this;
    }


    // TODO: Check out ViewModel
    // TODO: Check out command for event handling, should disable search button while searching

    // Register:
    // Setup AWS Secrets Manager for sensitive config data,
    // Consider using State pattern for search button, but could be over engineered
    // Search button can use a spinning logo while loading

    // Improvements:
    // adding cancel button (cancellation token)
    // loading icon on search
    // bind Enter key to search

    private async void SearchForKeywords(object sender, RoutedEventArgs e)
    {
        ErrorMessage = string.Empty;
        SearchButtonText = "Fetching results...";
        try
        {
            var results = await _searchAndFilterService.SearchAndFilter(Keyword, MatchingUrl, CancellationToken.None);
            var mappedResults = _mapper.Map<IEnumerable<UrlLocation>>(results);
            SearchResults = new(mappedResults);
            NotifyPropertyChanged(nameof(SearchResults));
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        SearchButtonText = "Search";
    }

    private void NotifyPropertyChanged(string propertyUpdated)
    {
        if(PropertyChanged is not null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyUpdated));
        }
    }
}