using Blazored.SessionStorage;
using Krafter.UI.Web.Client.Infrastructure.Services;
using Krafter.UI.Web.Client.Infrastructure.Storage;
using Krafter.UI.Web.Client.Infrastructure.SignalR;
using Krafter.UI.Web.Client.Features.Auth._Shared;

namespace Krafter.UI.Web.Client;

public static class RegisterUIServices
{
    public static void AddUIServices(this IServiceCollection service, string remoteHostUrl)
    {
        service.AddScoped<ThemeManager>();
        service.AddScoped<SignalRService>();
        service.AddBlazoredSessionStorage();
        service.AddScoped<CommonService>();
        service.AddScoped<MenuService>();
        service.AddScoped<LayoutService>();
        
        service.AddScoped<IAuthenticationService, AuthenticationService>();
        service.AddScoped<HttpService>();
        service.AddScoped<NotificationService>();
        
    }
}