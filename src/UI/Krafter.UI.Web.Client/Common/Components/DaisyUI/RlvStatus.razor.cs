using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// DaisyUI Status component - small icon to visually show current status (online, offline, error, etc.)
/// </summary>
public partial class RlvStatus
{
    /// <summary>
    /// Color of the status indicator
    /// </summary>
    [Parameter] public DaisyColor? Color { get; set; }

    /// <summary>
    /// Size of the status indicator
    /// </summary>
    [Parameter] public DaisySize Size { get; set; } = DaisySize.Md;

    /// <summary>
    /// Aria label for accessibility
    /// </summary>
    [Parameter] public string? AriaLabel { get; set; }

    /// <summary>
    /// Additional CSS classes (e.g., 'animate-ping', 'animate-bounce')
    /// </summary>
    [Parameter] public string? CssClass { get; set; }

    /// <summary>
    /// Additional attributes to apply to the status indicator
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string GetColorClass() => Color switch
    {
        DaisyColor.Neutral => "status-neutral",
        DaisyColor.Primary => "status-primary",
        DaisyColor.Secondary => "status-secondary",
        DaisyColor.Accent => "status-accent",
        DaisyColor.Info => "status-info",
        DaisyColor.Success => "status-success",
        DaisyColor.Warning => "status-warning",
        DaisyColor.Error => "status-error",
        _ => ""
    };

    private string GetSizeClass() => Size switch
    {
        DaisySize.Xs => "status-xs",
        DaisySize.Sm => "status-sm",
        DaisySize.Md => "", // Default size, no class
        DaisySize.Lg => "status-lg",
        DaisySize.Xl => "status-xl",
        _ => "" // Default
    };
}
