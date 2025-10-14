using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// DaisyUI Stats container - holds multiple RlvStat components to display statistics
/// </summary>
public partial class RlvStats
{
    /// <summary>
    /// Content for the stats container (typically multiple RlvStat components)
    /// </summary>
    [Parameter, EditorRequired] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Direction of stats layout
    /// </summary>
    [Parameter] public StatsDirection Direction { get; set; } = StatsDirection.Horizontal;

    /// <summary>
    /// Additional CSS classes to apply
    /// </summary>
    [Parameter] public string? CssClass { get; set; }

    /// <summary>
    /// Additional attributes to apply to the stats container
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string GetDirectionClass() => Direction switch
    {
        StatsDirection.Horizontal => "stats-horizontal",
        StatsDirection.Vertical => "stats-vertical",
        _ => "stats-horizontal"
    };
}

/// <summary>
/// Stats direction options
/// </summary>
public enum StatsDirection
{
    /// <summary>Horizontal layout (default)</summary>
    Horizontal,
    /// <summary>Vertical layout</summary>
    Vertical
}
