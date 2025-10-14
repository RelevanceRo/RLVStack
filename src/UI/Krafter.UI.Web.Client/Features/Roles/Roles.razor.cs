using Krafter.Api.Client;
using Krafter.Api.Client.Models;
using Krafter.UI.Web.Client.Common.Constants;
using Krafter.UI.Web.Client.Common.Permissions;
using Krafter.UI.Web.Client.Common.Models;
using Krafter.UI.Web.Client.Common.Enums;
using Krafter.UI.Web.Client.Common.Extensions;
using Krafter.UI.Web.Client.Common.Components.DaisyUI;
using Krafter.UI.Web.Client.Infrastructure.Services;

namespace Krafter.UI.Web.Client.Features.Roles;

public partial class Roles(CommonService commonService, NavigationManager navigationManager, KrafterClient krafterClient, LayoutService layoutService, DialogService dialogService, NotificationService notificationService) : ComponentBase, IDisposable
{
    public const string RoutePath = KrafterRoute.Roles;
    private bool IsLoading = true;

    private GetRequestInput RequestInput = new GetRequestInput();
    public string IdentifierBasedOnPlacement = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        IdentifierBasedOnPlacement = RequestInput.GetIdentifierBasedOnPlacement(nameof(Roles));

        LocalAppSate.CurrentPageTitle = $"Roles";

        dialogService.OnClose += Close;
        await GetListAsync();
    }

    private RoleDtoPaginationResponseResponse? response = new()
    {
        Data = new ()
    };

    private async Task GetListAsync(bool resetPaginationData = false)
    {
        IsLoading = true;
        if (resetPaginationData)
        {
            RequestInput.SkipCount = 0;
        }

        response = await krafterClient.Roles.GetPath.GetAsync(
            configuration =>
            {
                configuration.QueryParameters.Id = RequestInput.Id;
                configuration.QueryParameters.History = RequestInput.History;
                configuration.QueryParameters.IsDeleted = RequestInput.IsDeleted;
                configuration.QueryParameters.SkipCount = RequestInput.SkipCount;
                configuration.QueryParameters.MaxResultCount = RequestInput.MaxResultCount;
                configuration.QueryParameters.Filter = RequestInput.Filter;
                configuration.QueryParameters.OrderBy = RequestInput.OrderBy;
                configuration.QueryParameters.Query = RequestInput.Query;
            }, CancellationToken.None

        );

        IsLoading = false;
        await InvokeAsync(StateHasChanged);
    }

    private async Task AddRole()
    {
        await dialogService.OpenAsync<CreateOrUpdateRole>($"Add New Role",
            new Dictionary<string, object>() { { "UserDetails", new RoleDto() } },
            new DialogOptions()
            {
                Width = "50vw",
                Resizable = true,
                Draggable = true,
                Top = "5vh"
            });
    }

    private async Task UpdateRole(RoleDto role)
    {
        await dialogService.OpenAsync<CreateOrUpdateRole>($"Update Role {role.Name}",
            new Dictionary<string, object>() { { "UserDetails", role } },
            new DialogOptions()
            {
                Width = "50vw",
                Resizable = true,
                Draggable = true,
                Top = "5vh"
            });
    }

    private async Task DeleteRole(RoleDto roleDto)
    {
        if (response?.Data?.Items?.Contains(roleDto) == true)
        {
            await commonService.Delete(new DeleteRequestInput()
            {
                Id = roleDto.Id,
                DeleteReason = roleDto.DeleteReason,
                EntityKind = (int)EntityKind.KrafterRole
            }, $"Delete Role {roleDto.Name}");
        }
    }

    private async void Close(object? result)
    {
        if (result is not bool) return;

        await GetListAsync();
        StateHasChanged();
    }

    // RlvDataGrid event handlers
    private async Task OnPageChanged(int newPage)
    {
        RequestInput.SkipCount = (newPage - 1) * RequestInput.MaxResultCount;
        await GetListAsync();
    }

    private async Task OnPageSizeChanged(int newPageSize)
    {
        RequestInput.MaxResultCount = newPageSize;
        RequestInput.SkipCount = 0; // Reset to first page
        await GetListAsync();
    }

    private async Task OnSortChanged(string propertyName, SortDirection direction)
    {
        if (direction == SortDirection.None)
        {
            RequestInput.OrderBy = null;
        }
        else
        {
            var sortOrder = direction == SortDirection.Ascending ? "asc" : "desc";
            RequestInput.OrderBy = $"{propertyName} {sortOrder}";
        }
        await GetListAsync();
    }

    private async Task OnFilterChanged(Dictionary<string, string> filters)
    {
        // Build OData filter string
        var filterParts = new List<string>();
        foreach (var filter in filters)
        {
            if (!string.IsNullOrWhiteSpace(filter.Value))
            {
                filterParts.Add($"contains(tolower({filter.Key}), tolower('{filter.Value}'))");
            }
        }

        RequestInput.Filter = filterParts.Count > 0 ? string.Join(" and ", filterParts) : null;
        RequestInput.SkipCount = 0; // Reset to first page when filtering
        await GetListAsync();
    }

    public void Dispose()
    {
        dialogService.OnClose -= Close;
        dialogService.Dispose();
    }
}
