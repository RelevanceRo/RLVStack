
using Krafter.UI.Web.Client.Features.Auth._Shared;
using Microsoft.AspNetCore.Components.Web;

namespace Krafter.UI.Web.Client.Common.Components.Layout;

public partial class TopRight(IAuthenticationService authenticationService,
    NavigationManager navigationManager
    ) : ComponentBase
{
    [CascadingParameter]
    public bool IsMobileDevice { get; set; }

    [Parameter]
    public bool ShowProfileCard { get; set; }

    private async Task OnMenuItemClick(string action)
    {
        if (action == "Logout")
        {
            await authenticationService.LogoutAsync("OnMenuItemClick Logout");
            NavigateToLogin();
        }
        else if (action == "ChangePassword")
        {
            navigationManager.NavigateTo(
                $"/account/change-password?ReturnUrl={navigationManager.ToBaseRelativePath(navigationManager.Uri)}");
        }
        else if (action == "Appearance")
        {
            navigationManager.NavigateTo("/appearance");
        }
    }

    private void NavigateToLogin()
    {
        navigationManager.NavigateTo("/login");
    }

    private void HandleNavigateToLogin(MouseEventArgs e)
    {
        NavigateToLogin();
    }
}