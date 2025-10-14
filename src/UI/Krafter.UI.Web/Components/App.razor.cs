﻿using Krafter.UI.Web.Client.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using static Krafter.UI.Web.Client.Infrastructure.Services.ThemeManager;

namespace Krafter.UI.Web.Components
{
    public partial class App(ThemeManager themeManager)
    {
        [CascadingParameter]
        private HttpContext HttpContext { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (HttpContext != null)
            {
                var theme = HttpContext.Request.Cookies["KrafterTheme"];

                if (!string.IsNullOrEmpty(theme))
                {
                    themeManager.CurrentActive = theme?.Contains("dark") == true ? ThemePreference.Dark : ThemePreference.Light;
                }
            }
        }
    }
}