using System.Runtime.Versioning;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Browser;
using Avalonia.Media;
using Yiski.Frontend.POC;

internal sealed partial class Program {
    private static Task Main(string[] args) => BuildAvaloniaApp()
        .StartBrowserAppAsync("out");

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .With(new FontManagerOptions() {
                DefaultFamilyName = "avares://Yiski.Frontend.POC/Assets/Fonts#Eudoxus Sans",
            });
}