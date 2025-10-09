﻿using Krafter.UI.Web.Client.Features.Auth._Shared;
using Krafter.UI.Web.Client.Infrastructure.SignalR;

namespace Krafter.UI.Web.Client.Common.Components.Layout
{
    public partial class InitializeSignalr(
        IAuthenticationService authenticationService,
        SignalRService signalRService
    )
    {
        protected override async Task OnInitializedAsync()
        {
            authenticationService.LoginChange += async name =>
            {
                await signalRService.InitializeAsync();
            };
            await signalRService.InitializeAsync();
        }
    }
}