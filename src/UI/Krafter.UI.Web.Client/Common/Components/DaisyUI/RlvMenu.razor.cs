using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Menu component for displaying a list of navigation links.
/// Uses a <ul> element and contains RlvMenuItem components.
/// </summary>
public partial class RlvMenu
{
    /// <summary>
    /// Child content - typically contains multiple RlvMenuItem components.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Layout direction of the menu.
    /// </summary>
    [Parameter]
    public MenuLayout Layout { get; set; } = MenuLayout.Vertical;

    /// <summary>
    /// Size of the menu.
    /// </summary>
    [Parameter]
    public MenuSize Size { get; set; } = MenuSize.Medium;

    /// <summary>
    /// Additional CSS classes for the menu container.
    /// </summary>
    [Parameter]
    public string? MenuClass { get; set; }

    /// <summary>
    /// Additional attributes for the ul element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Builds the CSS class string for the menu.
    /// </summary>
    private string CssClass
    {
        get
        {
            var classes = new List<string>();

            // Layout (Vertical is default, no class needed)
            classes.Add(Layout switch
            {
                MenuLayout.Vertical => "", // Default layout, no class
                MenuLayout.Horizontal => "menu-horizontal",
                _ => ""
            });

            // Size (Medium is default, no class needed)
            classes.Add(Size switch
            {
                MenuSize.XSmall => "menu-xs",
                MenuSize.Small => "menu-sm",
                MenuSize.Medium => "", // Default size, no class
                MenuSize.Large => "menu-lg",
                MenuSize.XLarge => "menu-xl",
                _ => ""
            });

            // Custom classes
            if (!string.IsNullOrEmpty(MenuClass))
            {
                classes.Add(MenuClass);
            }

            return string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c)));
        }
    }
}

/// <summary>
/// Layout options for menus.
/// </summary>
public enum MenuLayout
{
    Vertical,
    Horizontal
}

/// <summary>
/// Size variants for menus.
/// </summary>
public enum MenuSize
{
    XSmall,
    Small,
    Medium,
    Large,
    XLarge
}
