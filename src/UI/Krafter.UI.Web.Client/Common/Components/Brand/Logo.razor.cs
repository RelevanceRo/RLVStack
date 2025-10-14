using Krafter.UI.Web.Client.Infrastructure.Services;

namespace Krafter.UI.Web.Client.Common.Components.Brand
{
    public partial class Logo(ThemeManager themeManager):IDisposable
    {
        [Parameter] public LogoSize Size { get; set; } = LogoSize.Medium;

        [Parameter] public LogoOrientation Orientation { get; set; } = LogoOrientation.Horizontal;

        private string currentLogo =string.Empty;
        protected override void OnInitialized()
        {
           // Current = this;

            themeManager.ThemeChangeRequested += OnThemeChanged;
        }
        public string GetAlt()
        {
            if (Size == LogoSize.ExtraSmall)
            {
                return "extra small logo";
            }
            else if (Size == LogoSize.Small)
            {
                return "small logo";
            }
            else if (Size == LogoSize.Medium)
            {
                return "medium logo";
            }
            else if (Size == LogoSize.Large)
            {
                return "large logo";
            }
            else if (Size == LogoSize.ExtraLarge)
            {
                return "extra large logo";
            }
            else
            {
                return "medium logo";
            }
        }

        private string GetSource()
        {
            var orientationDirection = "horizontal";
            if (Orientation == LogoOrientation.Vertical)
            {
                orientationDirection = "vertical";
            }

            var res = "";
            if (themeManager.CurrentActive == ThemeManager.ThemePreference.Dark)
            {
                res = "light";
            }
            else
            {
                res = "dark";
            }

            
            if (Size == LogoSize.ExtraSmall)
            {
                currentLogo=
                    $"brand/logos/{orientationDirection}/logo-{orientationDirection}-xs-{res}.svg";
            }
            else if (Size == LogoSize.Small)
            {
                currentLogo=
                    $"brand/logos/{orientationDirection}/logo-{orientationDirection}-s-{res}.svg";
            }
            else if (Size == LogoSize.Medium)
            {
                currentLogo=
                    $"brand/logos/{orientationDirection}/logo-{orientationDirection}-m-{res}.svg";
            }
            else if (Size == LogoSize.Large)
            {
                currentLogo=
                    $"brand/logos/{orientationDirection}/logo-{orientationDirection}-l-{res}.svg";
            }
            else if (Size == LogoSize.ExtraLarge)
            {
                currentLogo=
                    $"brand/logos/{orientationDirection}/logo-{orientationDirection}-xl-{res}.svg";
            }
            else
            {
                currentLogo=
                    $"brand/logos/{orientationDirection}/logo-{orientationDirection}-m-{res}.svg";
            }

            return currentLogo;
        }
        private async Task OnThemeChanged(string preference)
        {
            GetSource();
            await InvokeAsync(StateHasChanged);
        }

        private string GetSizeClass()
        {
            return Size switch
            {
                LogoSize.ExtraSmall => "h-8 w-auto object-contain",
                LogoSize.Small => "h-12 w-auto object-contain",
                LogoSize.Medium => "h-16 w-auto object-contain",
                LogoSize.Large => "h-24 w-auto object-contain",
                LogoSize.ExtraLarge => "h-32 w-auto object-contain",
                _ => "h-16 w-auto object-contain"
            };
        }

        public void Dispose()
        {
            themeManager.ThemeChangeRequested -= OnThemeChanged;
        }
    }

    public enum LogoSize
    {
        ExtraSmall = 0,
        Small = 1,
        Medium = 2,
        Large = 3,
        ExtraLarge = 4
    }

    public enum LogoOrientation
    {
        Horizontal = 0,
        Vertical = 1
    }
}