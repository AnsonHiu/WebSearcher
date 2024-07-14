using Application.Commands;
using Application.Models;
using AutoMapper;
using Domain.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Application.ViewModel;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly IMapper _mapper;
    private readonly ISearchAndFilterService _searchAndFilterService;

    public SearchCommand SearchCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public string TbErrorMessage { get; set; } = string.Empty;
    public string TxtKeyword { get; set; } = string.Empty;
    public string TxtMatchingUrl { get; set; } = string.Empty;
    public string BtnSearchButtonText { get; set; } = "Search";
    public bool IsLoading { get; set; } = false;
    public ObservableCollection<UrlLocation> SearchResults { get; set; } = [];

    
    public MainWindowViewModel(ISearchAndFilterService searchAndFilterService, IMapper mapper)
    {
        SearchCommand = new SearchCommand(
            async () => await SearchForKeywords(),
            () => !IsLoading);
        _mapper = mapper;
        _searchAndFilterService = searchAndFilterService;
    }

    public async Task SearchForKeywords()
    {
        IsLoading = true;
        SearchCommand.UpdateCanExecute();

        TbErrorMessage = string.Empty;
        NotifyPropertyChanged(nameof(TbErrorMessage));
        BtnSearchButtonText = "Fetching results...";
        NotifyPropertyChanged(nameof(BtnSearchButtonText));
        try
        {
            var results = await _searchAndFilterService.SearchAndFilter(TxtKeyword, TxtMatchingUrl, CancellationToken.None);
            var mappedResults = _mapper.Map<IEnumerable<UrlLocation>>(results);
            SearchResults = new(mappedResults);
            NotifyPropertyChanged(nameof(SearchResults));
        }
        catch (Exception ex)
        {
            TbErrorMessage = ex.Message;
            NotifyPropertyChanged(nameof(TbErrorMessage));
        }
        BtnSearchButtonText = "Search";
        NotifyPropertyChanged(nameof(BtnSearchButtonText));

        IsLoading = false;
        SearchCommand.UpdateCanExecute();
    }

    private void NotifyPropertyChanged(string propertyUpdated)
    {
        if (PropertyChanged is not null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyUpdated));
        }
    }
}