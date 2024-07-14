using System.Windows.Input;

namespace Application.Commands;

public class SearchCommand : ICommand
{
    private readonly Action _searchAction;
    private readonly Func<bool> _canSearch;

    public SearchCommand(Action searchAction, Func<bool> canSearch)
    {
        _searchAction = searchAction;
        _canSearch = canSearch;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => _canSearch();

    public void Execute(object? parameter) => _searchAction();

    public void UpdateCanExecute() => CanExecuteChanged?.Invoke(this, new EventArgs());
}
