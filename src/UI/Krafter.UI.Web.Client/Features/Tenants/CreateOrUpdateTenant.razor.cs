﻿using Krafter.Api.Client;
using Krafter.Api.Client.Models;
using Mapster;

namespace Krafter.UI.Web.Client.Features.Tenants
{
    public partial class CreateOrUpdateTenant(
        DialogService dialogService,
        KrafterClient krafterClient
        ):ComponentBase
    {
        [Parameter] public TenantDto? TenantInput { get; set; } = new();
        CreateOrUpdateTenantRequestInput CreateRequest = new();
        CreateOrUpdateTenantRequestInput OriginalCreateRequest = new();
        private bool isBusy = false;

        public List<TableToCopy> TablesToCopyList { get; set; } = TablesToCopy.Data;
        public List<string> SelectedTables { get; set; } = new ();
        protected override async Task OnInitializedAsync()
        {
            if (TenantInput is { })
            {
                CreateRequest = TenantInput.Adapt<CreateOrUpdateTenantRequestInput>();
                OriginalCreateRequest = TenantInput.Adapt<CreateOrUpdateTenantRequestInput>();
            }
        }

        async void Submit(CreateOrUpdateTenantRequestInput input)
        {
            if (TenantInput is not null)
            {
                isBusy = true;
                CreateOrUpdateTenantRequestInput finalInput = new();
                if (string.IsNullOrWhiteSpace(input.Id))
                {
                    if (string.IsNullOrWhiteSpace(input.Id))
                    {
                        SelectedTables??=new List<string>();
                        input.TablesToCopy = string.Join(",", SelectedTables);;
                    }
                    finalInput = input;
                }
                else
                {
                    finalInput.Id = input.Id;
                    if (input.Name != OriginalCreateRequest.Name)
                    {
                        finalInput.Name = input.Name;
                    }
                    if (input.Identifier != OriginalCreateRequest.Identifier)
                    {
                        finalInput.Identifier = input.Identifier;
                    }
                    if (input.IsActive != OriginalCreateRequest.IsActive)
                    {
                        finalInput.IsActive = input.IsActive;
                    }
                    if (input.ValidUpto != OriginalCreateRequest.ValidUpto)
                    {
                        finalInput.ValidUpto = input.ValidUpto;
                    }
                    //we do not need int he case of update
                    // if (input.TablesToCopy != OriginalCreateRequest.TablesToCopy)
                    // {
                    //     finalInput.TablesToCopy = input.TablesToCopy;
                    // }
                }

                var result = await krafterClient.Tenants.CreateOrUpdate.PostAsync(finalInput); //await tenantService.CreateOrUpdateAsync(finalInput);
                isBusy = false;
                StateHasChanged();
                if (result is{ IsError:false})
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
}