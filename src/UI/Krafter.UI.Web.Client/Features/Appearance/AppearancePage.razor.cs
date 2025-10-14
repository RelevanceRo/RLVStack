using Krafter.UI.Web.Client.Infrastructure.Services;
using static Krafter.UI.Web.Client.Infrastructure.Services.ThemeManager;

namespace Krafter.UI.Web.Client.Features.Appearance;

public partial class AppearancePage(
        ThemeService themeService,
        CookieThemeService cookieThemeService,
        ThemeManager themeManager
    ) : ComponentBase
{
    private string selectedTheme = string.Empty;
    private bool rightToLeft = false;
    private bool wcag = false;

    protected override void OnInitialized()
    {
        selectedTheme = themeService.Theme ?? Themes.Free.First().Value;
        rightToLeft = themeService.RightToLeft == true;
        wcag = themeService.Wcag == true;
    }

    void ChangeTheme(string value)
    {
        selectedTheme = value;
        themeManager.SetDifferentTheme(value);
    }

    void ChangeRightToLeft(bool value)
    {
        rightToLeft = value;
        themeService.SetRightToLeft(value);
    }

    void ChangeWcag(bool value)
    {
        wcag = value;
        themeService.SetWcag(value);
    }
}
