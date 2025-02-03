using CommunityToolkit.Mvvm.ComponentModel;

namespace Yiski.Frontend.POC.ViewModels;

public partial class HomePageViewModel : ViewModelBase {
    [ObservableProperty]
    private static string _administratorName = "dbg_text($\"Name: {name}\")";

    [ObservableProperty]
    private static string _greetingText = $"Howdy {_administratorName}!";
}