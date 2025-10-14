using Krafter.UI.Web.Client.Infrastructure.SignalR;

namespace Krafter.UI.Web.Client.Common.Components.Layout
{
    public partial class Notifications(SignalRService signalRService) : IDisposable
    {
        private List<string> messages = new List<string>();

        protected override async Task OnInitializedAsync()
        {
            signalRService.MessageReceived += OnMessageReceived;
        }

        private void OnMessageReceived(string user, string message)
        {
            var encodedMsg = $"{user}: {message}";
            messages.Add(encodedMsg);
            InvokeAsync(StateHasChanged);
        }

        private string GetNotificationCount()
        {
            return messages.Count > 0 ? messages.Count.ToString() : string.Empty;
        }

        public void Dispose()
        {
            signalRService.MessageReceived -= OnMessageReceived;
        }
    }
}