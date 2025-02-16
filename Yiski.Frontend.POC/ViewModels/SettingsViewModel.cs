using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using FluentAvalonia.Styling;

namespace Yiski.Frontend.POC.ViewModels;

public partial class SettingsViewModel : ViewModelBase {
    [ObservableProperty] private string _avaloniaVersion = typeof(AvaloniaObject).Assembly.GetName().Version.ToString();
    [ObservableProperty] private string _fluentAvaloniaVersion = typeof(FluentAvaloniaTheme).Assembly.GetName().Version.ToString();
    [ObservableProperty] private string _communityToolkitMvvmVersion = typeof(CommunityToolkit.Mvvm.ComponentModel.ObservableObject).Assembly.GetName().Version.ToString();
}