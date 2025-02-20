using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using CommunityToolkit.Mvvm.ComponentModel;
using FluentAvalonia.Styling;
using FluentAvalonia.UI.Controls;

namespace Yiski.Frontend.POC.ViewModels;

public partial class SettingsViewModel : ViewModelBase {
    [ObservableProperty] private static AvaloniaDictionary<string, string> _botRuntimeVersions;

    [ObservableProperty] private static string _yiskiVersion;
    [ObservableProperty] private static string _aviationVersion;
    [ObservableProperty] private static string _jdaVersion;
    [ObservableProperty] private static string _kotlinVersion;

    [ObservableProperty] private static string _communityToolkitMvvmVersion = typeof(ObservableObject).Assembly.GetName().Version.ToString();
    [ObservableProperty] private static string _fluentAvaloniaVersion = typeof(FluentAvaloniaTheme).Assembly.GetName().Version.ToString();
    [ObservableProperty] private static string _avaloniaVersion = typeof(AvaloniaObject).Assembly.GetName().Version.ToString();

    public Task<string> Versions => GetAsync();

    public SettingsViewModel() {
        Task.Run(new Action(async () => await GetAsync()));
    }

    static async Task<string> GetAsync() {
        using HttpResponseMessage response = await App.httpClient.GetAsync("version");

        response.EnsureSuccessStatusCode().WriteRequestToConsole();

        var jsonResponse = await response.Content.ReadAsStringAsync();

        AvaloniaDictionary<string, string> runtimeVersions =
            JsonSerializer.Deserialize<AvaloniaDictionary<string, string>>(jsonResponse);

        _yiskiVersion = runtimeVersions["yiski"];
        _aviationVersion = runtimeVersions["aviation"];
        _jdaVersion = runtimeVersions["jda"];
        _kotlinVersion = runtimeVersions["kotlin"];

        return jsonResponse;
    }
}