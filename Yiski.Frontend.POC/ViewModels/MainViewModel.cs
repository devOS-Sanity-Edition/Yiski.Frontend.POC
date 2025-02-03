using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Media;
using Avalonia.Metadata;
using CommunityToolkit.Mvvm.ComponentModel;
using FluentAvalonia.UI.Controls;
using Yiski.Frontend.POC.Views;

namespace Yiski.Frontend.POC.ViewModels;

public partial class MainViewModel : ViewModelBase {
    private object _selectedCategory;
    private Control _currentPage = new HomePageView();
    private object _headerName = "Home";


    public MainViewModel() {
        Categories = new List<CategoryBase>();

        Categories.Add(new Header { Name = "devOS 3.0: Cataclysmic Clownfish" });
        Categories.Add(new Category { Name = "Home", ViewName = "HomePageView", Icon = Symbol.Home, ToolTip = "Welcome home" });
        Categories.Add(new Seperator());
        Categories.Add(new Header { Name = "ruh roh raggy" });
        Categories.Add(new Category { Name = "Moderation Actions", ViewName = "ModerationActionsView", Icon = Symbol.Admin, ToolTip = "go, do a crime" });
        Categories.Add(new Category { Name = "Moderation Logs", ViewName = "ModerationLogsView", Icon = Symbol.Clipboard, ToolTip = "go, become the NSA" });

        SelectedCategory = Categories[1];
    }

    public List<CategoryBase> Categories { get; }


    public object SelectedCategory {
        get => _selectedCategory;
        set {
            SetProperty(ref _selectedCategory, value);
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
        if (SelectedCategory is Category cat) {
            var pageName = $"Yiski.Frontend.POC.Views.{cat.ViewName}";
            var page = Activator.CreateInstance(Type.GetType(pageName));
            CurrentPage = page as Control;
            HeaderName = cat.Name;
        }

        else if (SelectedCategory is NavigationViewItem nvi) {
            var page = Activator.CreateInstance(Type.GetType($"Yiski.Frontend.POC.Views.SettingsView"));
            CurrentPage = page as Control;
            HeaderName = "Settings";
        }
    }
}

public abstract class CategoryBase { }

public class Category : CategoryBase {
    public string Name { get; set; }
    public string ViewName { get; set; }
    public string ToolTip { get; set; }
    public Symbol Icon { get; set; }
}

public class Seperator : CategoryBase { }

public class Header : CategoryBase {
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