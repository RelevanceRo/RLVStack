using Microsoft.JSInterop;

namespace Krafter.UI.Web.Client.Infrastructure.Services
{
    public class ThemeManager(IJSRuntime jsRuntime)
    {
        public event Func<string, Task> ThemeChangeRequested;

        public enum ThemePreference
        {
            Auto,
            Dark,
            Light
        }

        public ThemePreference CurrentPreference { get; set; } = ThemePreference.Auto;
        public ThemePreference CurrentActive { get; set; } = ThemePreference.Light;

        public async Task SetThemePreference(ThemePreference preference)
        {
            CurrentPreference = preference;

            string preferenceValue = preference switch
            {
                ThemePreference.Auto => "auto",
                ThemePreference.Dark => "dark",
                ThemePreference.Light => "light",
                _ => "auto"
            };

            await jsRuntime.InvokeVoidAsync("setStoredThemePreference", preferenceValue);

            if (preference == ThemePreference.Auto)
            {
                var systemTheme = await jsRuntime.InvokeAsync<string>("detectSystemTheme");
                CurrentActive = systemTheme == "dark" ? ThemePreference.Dark : ThemePreference.Light;
            }
            else
            {
                CurrentActive = preference;
            }

            // Apply DaisyUI theme via data-theme attribute on html element
            string daisyTheme = CurrentActive == ThemePreference.Dark ? "dark" : "light";
            await jsRuntime.InvokeVoidAsync("eval", $"document.documentElement.setAttribute('data-theme', '{daisyTheme}')");

            await (ThemeChangeRequested?.Invoke(daisyTheme) ?? Task.CompletedTask);
        }

        public async Task OnSystemThemeChanged(string systemTheme)
        {
            var storedPreference = await jsRuntime.InvokeAsync<string>("getStoredThemePreference");

            if (storedPreference == "auto")
            {
                CurrentPreference = ThemePreference.Auto;
                var newActive = systemTheme == "dark" ? ThemePreference.Dark : ThemePreference.Light;

                if (CurrentActive != newActive)
                {
                    CurrentActive = newActive;
                    string daisyTheme = CurrentActive == ThemePreference.Dark ? "dark" : "light";
                    await jsRuntime.InvokeVoidAsync("eval", $"document.documentElement.setAttribute('data-theme', '{daisyTheme}')");
                    await (ThemeChangeRequested?.Invoke(daisyTheme) ?? Task.CompletedTask);
                }
            }
        }

        public async Task SetDifferentTheme(string theme)
        {
            var isDark = theme.Contains("dark", StringComparison.OrdinalIgnoreCase);
            CurrentPreference = isDark ? ThemePreference.Dark : ThemePreference.Light;
            CurrentActive = CurrentPreference;

            string preferenceValue = isDark ? "dark" : "light";
            await jsRuntime.InvokeVoidAsync("setStoredThemePreference", preferenceValue);

            // Apply DaisyUI theme
            string daisyTheme = isDark ? "dark" : "light";
            await jsRuntime.InvokeVoidAsync("eval", $"document.documentElement.setAttribute('data-theme', '{daisyTheme}')");

            await (ThemeChangeRequested?.Invoke(daisyTheme) ?? Task.CompletedTask);
        }
    }
}
