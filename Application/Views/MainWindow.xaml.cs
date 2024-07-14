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
}