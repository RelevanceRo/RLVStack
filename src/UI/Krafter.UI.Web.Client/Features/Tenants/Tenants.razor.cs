using Krafter.Api.Client;
using Krafter.Api.Client.Models;
using Krafter.UI.Web.Client.Common.Constants;
using Krafter.UI.Web.Client.Common.Permissions;
using Krafter.UI.Web.Client.Common.Models;
using Krafter.UI.Web.Client.Common.Enums;
using Krafter.UI.Web.Client.Infrastructure.Services;
using Krafter.UI.Web.Client.Common.Components.DaisyUI;

namespace Krafter.UI.Web.Client.Features.Tenants;

public partial class Tenants(
    CommonService commonService,
    KrafterClient krafterClient,
    DialogService dialogService
    ) : ComponentBase, IDisposable
{
    public const string RoutePath = KrafterRoute.Tenants;

    private bool IsLoading = true;
    private GetRequestInput requestInput = new();

    [Parameter] public bool? EnableAction { get; set; }

    TenantDtoPaginationResponseResponse? response = new()
    {
        Data = new()
    };

    protected override async Task OnInitializedAsync()
    {
        LocalAppSate.CurrentPageTitle = $"Tenants";

        dialogService.OnClose += Close;
        await Get();
    }

    private async Task Get(bool resetPaginationData = false)
    {
        IsLoading = true;

        if (resetPaginationData)
        {
            requestInput.SkipCount = 0;
        }

        response = await krafterClient.Tenants.GetPath.GetAsync(
            configuration =>
            {
                configuration.QueryParameters.Id = requestInput.Id;
                configuration.QueryParameters.History = requestInput.History;
                configuration.QueryParameters.IsDeleted = requestInput.IsDeleted;
                configuration.QueryParameters.SkipCount = requestInput.SkipCount;
                configuration.QueryParameters.MaxResultCount = requestInput.MaxResultCount;
                configuration.QueryParameters.Filter = requestInput.Filter;
                configuration.QueryParameters.OrderBy = requestInput.OrderBy;
                configuration.QueryParameters.Query = requestInput.Query;
            }, CancellationToken.None
        );

        IsLoading = false;
        await InvokeAsync(StateHasChanged);
    }

    private async Task Add()
    {
        await dialogService.OpenAsync<CreateOrUpdateTenant>($"Add New Tenant",
            new Dictionary<string, object>() { { "TenantInput", new TenantDto() } },
            new DialogOptions()
            {
                Width = "40vw",
                Resizable = true,
                Draggable = true,
                Top = "5vh"
            });
    }

    private async Task Update(TenantDto tenant)
    {
        await dialogService.OpenAsync<CreateOrUpdateTenant>($"Update Tenant {tenant.Name}",
            new Dictionary<string, object>() { { "TenantInput", tenant } },
            new DialogOptions()
            {
                Width = "40vw",
                Resizable = true,
                Draggable = true,
                Top = "5vh"
            });
    }

    private async Task Delete(TenantDto input)
    {
        if (response.Data.Items.Contains(input))
        {
            await commonService.Delete(new DeleteRequestInput()
            {
                Id = input.Id,
                DeleteReason = input.DeleteReason,
                EntityKind = (int)EntityKind.Tenant
            }, $"Delete Tenant {input.Name}");

            // Refresh data after delete
            await Get();
        }
    }

    private async void Close(object? result)
    {
        if (result is not bool) return;

        // Refresh data after dialog closes
        await Get();
    }

    private async Task LoadData(DataGridLoadArgs args)
    {
        IsLoading = true;
        await Task.Yield();

        requestInput.SkipCount = args.Skip;
        requestInput.MaxResultCount = args.Take;

        // Handle sorting
        if (!string.IsNullOrEmpty(args.SortBy))
        {
            var direction = args.SortDirection == SortDirection.Ascending ? "asc" : "desc";
            requestInput.OrderBy = $"{args.SortBy} {direction}";
        }
        else
        {
            requestInput.OrderBy = null;
        }

        // Handle filters - combine all filters into a single filter string
        if (args.Filters.Any())
        {
            requestInput.Filter = string.Join(" and ", args.Filters.Select(f => $"{f.Key} contains '{f.Value}'"));
        }
        else
        {
            requestInput.Filter = null;
        }

        await Get();
    }

    public void Dispose()
    {
        dialogService.OnClose -= Close;
        dialogService.Dispose();
    }
}
