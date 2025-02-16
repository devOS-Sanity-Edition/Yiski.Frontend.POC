using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using CommunityToolkit.Mvvm.ComponentModel;
using FluentAvalonia.UI.Controls;
using Yiski.Frontend.POC.Views;

namespace Yiski.Frontend.POC.ViewModels;

public partial class MainViewModel : ViewModelBase {
    private object _selectedItem;
    private Control _currentPage = new HomePageView();
    private object _headerName;

    [ObservableProperty]
    private string _serverName = "devOS: 3.0: Cataclysmic Clownfish";

    public MainViewModel() {
        MenuItems = new List<ItemBase>();

        MenuItems.Add(new Item {
            Name = "Home",
            ViewName = "HomePageView",
            ViewType = typeof(HomePageViewModel),
            Icon = Symbol.Home,
            ToolTip = "Welcome home"
        });
        MenuItems.Add(new Seperator());
        MenuItems.Add(new Header { Name = "Moderation View" });
        MenuItems.Add(new Item {
            Name = "Moderation Actions",
            ViewName = "ModerationActionsView",
            ViewType = typeof(ModerationActionsViewModel),
            Icon = Symbol.Admin,
            ToolTip = "go, do a crime"
        });
        MenuItems.Add(new Item {
            Name = "Moderation Logs",
            ViewName = "ModerationLogsView",
            ViewType = typeof(ModerationLogsViewModel),
            Icon = Symbol.Clipboard,
            ToolTip = "go, become the NSA"
        });
        MenuItems.Add(new Header { Name = "Server" });
        MenuItems.Add(new Item {
            Name = "Logs",
            ViewName = "ServerLogsView",
            ViewType = typeof(ServerLogsViewModel),
            Icon = Symbol.Keyboard
        });

        SelectedItem = MenuItems[0];
    }

    public List<ItemBase> MenuItems { get; }


    public object SelectedItem {
        get => _selectedItem;
        set {
            SetProperty(ref _selectedItem, value);
            SetCurrentPage();
        }
    }

    public Control CurrentPage {
        get => _currentPage;
        set => SetProperty(ref _currentPage, value);
    }

    public object HeaderName {
        get => _headerName;
        set => SetProperty(ref _headerName, value);
    }

    private void SetCurrentPage() {
        if (SelectedItem is Item cat) {
            var pageName = $"Yiski.Frontend.POC.Views.{cat.ViewName}";
            var page = Activator.CreateInstance(Type.GetType(pageName)!)!;
            (page as Control).DataContext = cat.ViewType != null ? Activator.CreateInstance(cat.ViewType)! : this;
            CurrentPage = page as Control;
            HeaderName = cat.Name;
        }

        else if (SelectedItem is NavigationViewItem nvi) {
            var page = Activator.CreateInstance(Type.GetType($"Yiski.Frontend.POC.Views.SettingsView"));
            (page as Control).DataContext = Activator.CreateInstance(typeof(SettingsViewModel))!; // i hate this
            CurrentPage = page as Control;
            HeaderName = "Settings";
        }
    }
}

public abstract class ItemBase { }

public class Item : ItemBase {
    public required string Name { get; set; }
    public required string ViewName { get; set; }
    public required Type ViewType { get; set; }
    public string? ToolTip { get; set; }
    public required Symbol Icon { get; set; }
}

public class Seperator : ItemBase { }

public class Header : ItemBase {
    public string Name { get; set; }
}

public class MenuItemTemplateSelector : DataTemplateSelector {
    [Content]
    public IDataTemplate ItemTemplate { get; set; }
    public IDataTemplate SeperatorTemplate { get; set; }
    public IDataTemplate HeaderTemplate { get; set; }

    protected override IDataTemplate SelectTemplateCore(object item) {
        return item is Seperator ? SeperatorTemplate
            : item is Header ? HeaderTemplate
            : ItemTemplate;
    }
}