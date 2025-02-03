using Avalonia.Controls;
using FluentAvalonia.UI.Windowing;

namespace Yiski.Frontend.POC.Views;

public partial class MainWindow : AppWindow {
    public MainWindow() {
        TitleBar.ExtendsContentIntoTitleBar = false;
        InitializeComponent();
    }
}