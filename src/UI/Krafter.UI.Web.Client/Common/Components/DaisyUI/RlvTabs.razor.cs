using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Tabs container component for showing a list of tabs.
/// Contains multiple RlvTab components.
/// </summary>
public partial class RlvTabs
{
    /// <summary>
    /// Child content - typically contains multiple RlvTab components.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Style variant of the tabs.
    /// </summary>
    [Parameter]
    public TabsStyle Style { get; set; } = TabsStyle.None;

    /// <summary>
    /// Size of the tabs.
    /// </summary>
    [Parameter]
    public TabsSize Size { get; set; } = TabsSize.Medium;

    /// <summary>
    /// Placement of tabs relative to content.
    /// </summary>
    [Parameter]
    public TabsPlacement Placement { get; set; } = TabsPlacement.Top;

    /// <summary>
    /// Additional CSS classes for the tabs container.
    /// </summary>
    [Parameter]
    public string? TabsClass { get; set; }

    /// <summary>
    /// Additional attributes for the tabs container element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Builds the CSS class string for the tabs container.
    /// </summary>
    private string CssClass
    {
        get
        {
            var classes = new List<string>();

            // Style
            classes.Add(Style switch
            {
                TabsStyle.Box => "tabs-box",
                TabsStyle.Border => "tabs-border",
                TabsStyle.Lift => "tabs-lift",
                _ => ""
            });

            // Size (Medium is default, no class needed)
            classes.Add(Size switch
            {
                TabsSize.XSmall => "tabs-xs",
                TabsSize.Small => "tabs-sm",
                TabsSize.Medium => "", // Default size, no class
                TabsSize.Large => "tabs-lg",
                TabsSize.XLarge => "tabs-xl",
                _ => ""
            });

            // Placement (Top is default, no class needed)
            classes.Add(Placement switch
            {
                TabsPlacement.Top => "", // Default placement, no class
                TabsPlacement.Bottom => "tabs-bottom",
                _ => ""
            });

            // Custom classes
            if (!string.IsNullOrEmpty(TabsClass))
            {
                classes.Add(TabsClass);
            }

            return string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c)));
        }
    }
}

/// <summary>
/// Style variants for tabs.
/// </summary>
public enum TabsStyle
{
    None,
    Box,
    Border,
    Lift
}

/// <summary>
/// Size variants for tabs.
/// </summary>
public enum TabsSize
{
    XSmall,
    Small,
    Medium,
    Large,
    XLarge
}

/// <summary>
/// Placement options for tabs relative to content.
/// </summary>
public enum TabsPlacement
{
    Top,
    Bottom
}
