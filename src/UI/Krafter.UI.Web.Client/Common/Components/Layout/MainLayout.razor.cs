using Krafter.Api.Client;
using Krafter.Api.Client.Models;
using Krafter.UI.Web.Client.Features.Auth._Shared;
using Krafter.UI.Web.Client.Models;
using Krafter.UI.Web.Client.Infrastructure.Services;
using Krafter.UI.Web.Client.Infrastructure.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components.Web;

namespace Krafter.UI.Web.Client.Common.Components.Layout;

public partial class MainLayout(KrafterClient krafterClient,
    MenuService menuService,
    LayoutService layoutService,
    IAuthenticationService authenticationService,
    ThemeManager themeManager,
    IKrafterLocalStorageService krafterLocalStorageService
    ) : IDisposable
{
    [CascadingParameter]
    public bool IsMobileDevice { get; set; }

    [CascadingParameter]
    private HttpContext HttpContext { get; set; }

    private bool sidebarExpanded = true;
    private bool configSidebarExpanded = false;
    private bool rendered;
    private string searchTerm = string.Empty;

    private IEnumerable<Menu> menus = new List<Menu>();

    private ICollection<string>? cachedPermissionsAsync = new List<string>();
    public StringResponse? AppInfo { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            rendered = true;
        }
    }

    protected override async Task OnInitializedAsync()
    {
        AppInfo = await krafterClient.AppInfo.GetAsync();
        cachedPermissionsAsync = await krafterLocalStorageService.GetCachedPermissionsAsync();
        if (cachedPermissionsAsync is null)
        {
            cachedPermissionsAsync = new List<string>();
        }
        menus = menuService.Menus;
        layoutService.HeadingChanged += this.HeadingChanged;
        authenticationService.LoginChange += async name =>
        {
            cachedPermissionsAsync = await krafterLocalStorageService.GetCachedPermissionsAsync();
            if (cachedPermissionsAsync is null)
            {
                cachedPermissionsAsync = new List<string>();
            }
            menus = menuService.Menus;
        };
    }

    private void FilterPanelMenu(ChangeEventArgs args)
    {
        var term = args.Value.ToString();
        menus = string.IsNullOrEmpty(term) ? menuService.Menus : menuService.Filter(term);
    }

    private void HeadingChanged(object? sender, EventArgs e)
    {
        this.InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        layoutService.HeadingChanged -= this.HeadingChanged;
    }

    private bool HasChildPermission(Menu category)
    {
        var childPermission = category.Children.Select(c => c.Permission);

        return childPermission.Intersect(cachedPermissionsAsync).Count() > 0;
    }

    private bool HasPermission(Menu category)
    {
        if (string.IsNullOrWhiteSpace(category.Permission) && (category.Children is null || category.Children.Count() == 0))
        {
            return true;
        }
        return cachedPermissionsAsync.Contains(category.Permission);
    }

    private void HandleCloseConfig(MouseEventArgs e)
    {
        configSidebarExpanded = false;
    }

    private string themePreferenceStr
    {
        get => themeManager.CurrentPreference.ToString();
        set
        {
            if (Enum.TryParse<ThemeManager.ThemePreference>(value, out var pref))
            {
                themeManager.SetThemePreference(pref).ConfigureAwait(false);
            }
        }
    }

    private ThemeManager.ThemePreference themePreference
    {
        get => themeManager.CurrentPreference;
        set
        {
            themeManager.SetThemePreference(value).ConfigureAwait(false);
        }
    }
}