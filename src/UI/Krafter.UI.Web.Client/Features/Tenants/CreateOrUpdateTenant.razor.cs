using Krafter.Api.Client;
using Krafter.Api.Client.Models;
using Mapster;

namespace Krafter.UI.Web.Client.Features.Tenants;

public partial class CreateOrUpdateTenant(
    DialogService dialogService,
    KrafterClient krafterClient
    ) : ComponentBase
{
    [Parameter] public TenantDto? TenantInput { get; set; } = new();

    CreateOrUpdateTenantRequestInput CreateRequest = new();
    CreateOrUpdateTenantRequestInput OriginalCreateRequest = new();
    private bool isBusy = false;

    public List<TableToCopy> TablesToCopyList { get; set; } = TablesToCopy.Data;
    public List<string> SelectedTables { get; set; } = new();

    // Helper property to convert DateTimeOffset? to DateTime? for InputDate
    private DateTime? ValidUptoDate
    {
        get => CreateRequest.ValidUpto?.DateTime;
        set => CreateRequest.ValidUpto = value.HasValue ? new DateTimeOffset(value.Value, TimeSpan.Zero) : null;
    }

    protected override async Task OnInitializedAsync()
    {
        if (TenantInput is { })
        {
            CreateRequest = TenantInput.Adapt<CreateOrUpdateTenantRequestInput>();
            OriginalCreateRequest = TenantInput.Adapt<CreateOrUpdateTenantRequestInput>();
        }
    }

    async Task Submit()
    {
        if (TenantInput is not null)
        {
            isBusy = true;
            StateHasChanged();

            CreateOrUpdateTenantRequestInput finalInput = new();

            if (string.IsNullOrWhiteSpace(CreateRequest.Id))
            {
                // Creating new tenant
                SelectedTables ??= new List<string>();
                CreateRequest.TablesToCopy = string.Join(",", SelectedTables);
                finalInput = CreateRequest;
            }
            else
            {
                // Updating existing tenant - only send changed fields
                finalInput.Id = CreateRequest.Id;

                if (CreateRequest.Name != OriginalCreateRequest.Name)
                {
                    finalInput.Name = CreateRequest.Name;
                }
                if (CreateRequest.Identifier != OriginalCreateRequest.Identifier)
                {
                    finalInput.Identifier = CreateRequest.Identifier;
                }
                if (CreateRequest.IsActive != OriginalCreateRequest.IsActive)
                {
                    finalInput.IsActive = CreateRequest.IsActive;
                }
                if (CreateRequest.ValidUpto != OriginalCreateRequest.ValidUpto)
                {
                    finalInput.ValidUpto = CreateRequest.ValidUpto;
                }
            }

            var result = await krafterClient.Tenants.CreateOrUpdate.PostAsync(finalInput);
            isBusy = false;
            StateHasChanged();

            if (result is { IsError: false })
            {
                dialogService.Close(true);
            }
        }
        else
        {
            dialogService.Close(false);
        }
    }

    void Cancel()
    {
        dialogService.Close(false);
    }
}
