using Application.ViewModel;
using System.Windows;

namespace Application;

public partial class MainWindow: Window
{
    public MainWindowViewModel ViewModel { get; set; }

    public MainWindow(MainWindowViewModel mainWindowViewModel)
    {
        InitializeComponent();
        ViewModel = mainWindowViewModel;
        DataContext = ViewModel;
    }
    // Register:
    // Setup AWS Secrets Manager for sensitive config data,

    // Improvements:
    // adding cancel button (cancellation token)
    // loading icon on search
    // bind Enter key to search
    // Consider using State pattern for search button, but could be over engineered
    // Search button can use a spinning logo while loading
}