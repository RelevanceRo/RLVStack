using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// DaisyUI Dock component (Bottom navigation/Bottom bar) - provides navigation options that stick to the bottom of the screen.
/// NOTE: Requires &lt;meta name="viewport" content="viewport-fit=cover"&gt; for responsiveness in iOS.
/// </summary>
public partial class RlvDock
{
    /// <summary>
    /// Content for the dock (typically button elements with icons and dock-label spans)
    /// Use 'dock-active' class on buttons to mark them as active
    /// Use 'dock-label' class on spans for text labels
    /// </summary>
    [Parameter, EditorRequired] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Size of the dock
    /// </summary>
    [Parameter] public DaisySize Size { get; set; } = DaisySize.Md;

    /// <summary>
    /// Additional CSS classes
    /// </summary>
    [Parameter] public string? CssClass { get; set; }

    /// <summary>
    /// Additional attributes to apply to the dock container
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string GetSizeClass() => Size switch
    {
        DaisySize.Xs => "dock-xs",
        DaisySize.Sm => "dock-sm",
        DaisySize.Md => "", // Default size, no class
        DaisySize.Lg => "dock-lg",
        DaisySize.Xl => "dock-xl",
        _ => "" // Default
    };
}
