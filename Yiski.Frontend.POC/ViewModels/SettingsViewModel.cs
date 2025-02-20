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

    [ObservableProperty] private static string _communityToolkitMvvmVersion = typeof(ObservableObject).Assembly.GetName().Version.ToString();
    [ObservableProperty] private static string _fluentAvaloniaVersion = typeof(FluentAvaloniaTheme).Assembly.GetName().Version.ToString();
    [ObservableProperty] private static string _avaloniaVersion = typeof(AvaloniaObject).Assembly.GetName().Version.ToString();

    private static string _yiskiVersion;
    private static string _aviationVersion;
    private static string _jdaVersion;
    private static string _kotlinVersion;

    public string YiskiVersion
    {
        get => _yiskiVersion;
        set => SetProperty(ref _yiskiVersion, value);
    }
    public string AviationVersion
    {
        get => _aviationVersion;
        set => SetProperty(ref _aviationVersion, value);
    }
    public string JdaVersion {
        get => _jdaVersion;
        set => SetProperty(ref _jdaVersion, value);
    }
    public string KotlinVersion
    {
        get => _kotlinVersion;
        set => SetProperty(ref _kotlinVersion, value);
    }

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