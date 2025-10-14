using Krafter.Api.Client;
using Krafter.Api.Client.Models;
using Krafter.UI.Web.Client.Common.Permissions;
using Mapster;

namespace Krafter.UI.Web.Client.Features.Roles;

public partial class CreateOrUpdateRole(DialogService dialogService, KrafterClient krafterClient, NotificationService notificationService) : ComponentBase
{
    IEnumerable<string> selectedStandards;

    public class GroupPermissionData
    {
        public string Description { get; set; }
        public string Action { get; set; }
        public string Resource { get; set; }
        public string FinalPermission { get; set; }
        public bool IsBasic { get; set; }
        public bool IsRoot { get; set; }

        public bool IsGroup
        {
            get { return Resource != null && string.IsNullOrEmpty(FinalPermission); }
        }
    }

    [Parameter] public RoleDto? UserDetails { get; set; } = new RoleDto();
    CreateOrUpdateRoleRequest CreateUserRequest = new CreateOrUpdateRoleRequest();
    CreateOrUpdateRoleRequest OriginalCreateUserRequest = new CreateOrUpdateRoleRequest();
    public List<KrafterPermission> AllRoles { get; set; }
    IEnumerable<GroupPermissionData> GroupedData = new List<GroupPermissionData>();

    private bool isBusy = false;

    private bool KeyPosition;
    private bool ProcessOwner;

    // Property for multi-select binding
    private string[] SelectedPermissions
    {
        get => CreateUserRequest.Permissions?.ToArray() ?? Array.Empty<string>();
        set
        {
            CreateUserRequest.Permissions = value?.ToList() ?? new List<string>();
            StateHasChanged();
        }
    }

    protected override async Task OnInitializedAsync()
    {
        if (UserDetails is { })
        {
            CreateUserRequest = UserDetails.Adapt<CreateOrUpdateRoleRequest>();
            OriginalCreateUserRequest = UserDetails.Adapt<CreateOrUpdateRoleRequest>();
            if (!string.IsNullOrWhiteSpace(UserDetails.Id))
            {
                // Build grouped data with proper group headers
                var groupedList = new List<GroupPermissionData>();
                var permissionsByResource = KrafterPermissions.All.GroupBy(c => c.Resource);

                foreach (var group in permissionsByResource)
                {
                    // Add group header
                    groupedList.Add(new GroupPermissionData { Resource = group.Key, FinalPermission = "" });

                    // Add permissions in group
                    foreach (var permission in group)
                    {
                        groupedList.Add(new GroupPermissionData
                        {
                            Description = permission.Description,
                            Action = permission.Action,
                            Resource = null, // Clear resource for non-group items
                            IsBasic = permission.IsBasic,
                            IsRoot = permission.IsRoot,
                            FinalPermission = KrafterPermission.NameFor(permission.Action, permission.Resource)
                        });
                    }
                }

                GroupedData = groupedList;

                RoleDto? roleWithPermissions =
                    (await krafterClient.Roles.GetByIdWithPermissions[UserDetails.Id].GetAsync())?.Data;
                CreateUserRequest.Permissions = roleWithPermissions is { Permissions: not null }
                    ? roleWithPermissions.Permissions
                    : new List<string>();
                OriginalCreateUserRequest.Permissions = CreateUserRequest.Permissions;
            }
        }
    }

    async void Submit(CreateOrUpdateRoleRequest input)
    {
        if (UserDetails is not null)
        {
            isBusy = true;
            CreateOrUpdateRoleRequest finalInput = new();
            if (string.IsNullOrWhiteSpace(input.Id))
            {
                finalInput = input;
            }
            else
            {
                finalInput.Id = input.Id;
                if (input.Name != OriginalCreateUserRequest.Name)
                {
                    finalInput.Name = input.Name;
                }

                if (input.Description != OriginalCreateUserRequest.Description)
                {
                    finalInput.Description = input.Description;
                }

                if (!input.Permissions.ToHashSet().SetEquals(OriginalCreateUserRequest.Permissions))
                {
                    finalInput.Permissions = input.Permissions;
                }
            }
            var result = await krafterClient.Roles.CreateOrUpdate.PostAsync(finalInput);
            isBusy = false;
            StateHasChanged();
            if (result is null || result is { IsError: true })
            {
                if (string.IsNullOrWhiteSpace(input.Id))
                {
                    await dialogService.Alert(
                        "If you need to add a backup user and person in charge to this role, first add this role to the user by updating the user. Then come back here again and click on update to choose the backup person and person in charge.",
                        "Information", new AlertOptions() { OkButtonText = "OK", Top = "5vh" });
                }

                dialogService.Close(true);
            }
        }
        else
        {
            dialogService.Close(false);
        }
    }

    void OnChange(object? value)
    {
        if (value is null)
        {
            //CreateUserRequest.BackupUserId = "";
        }
    }

    void Cancel()
    {
        dialogService.Close(false);
    }
}
