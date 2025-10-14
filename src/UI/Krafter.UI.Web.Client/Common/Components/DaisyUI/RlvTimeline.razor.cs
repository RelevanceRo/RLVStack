using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// DaisyUI Timeline component - shows a list of events in chronological order
/// </summary>
public partial class RlvTimeline
{
    /// <summary>
    /// Content for the timeline (typically RlvTimelineItem components)
    /// </summary>
    [Parameter, EditorRequired] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Direction of the timeline
    /// </summary>
    [Parameter] public TimelineDirection Direction { get; set; } = TimelineDirection.Horizontal;

    /// <summary>
    /// Snaps icons to start instead of middle
    /// </summary>
    [Parameter] public bool SnapIcon { get; set; }

    /// <summary>
    /// Forces all items on one side
    /// </summary>
    [Parameter] public bool Compact { get; set; }

    /// <summary>
    /// Additional CSS classes
    /// </summary>
    [Parameter] public string? CssClass { get; set; }

    /// <summary>
    /// Additional attributes to apply to the timeline container
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string GetDirectionClass() => Direction switch
    {
        TimelineDirection.Horizontal => "timeline-horizontal",
        TimelineDirection.Vertical => "timeline-vertical",
        _ => "timeline-horizontal"
    };

    private string GetModifierClass()
    {
        var classes = new List<string>();
        if (SnapIcon) classes.Add("timeline-snap-icon");
        if (Compact) classes.Add("timeline-compact");
        return string.Join(" ", classes);
    }
}

/// <summary>
/// Timeline direction options
/// </summary>
public enum TimelineDirection
{
    /// <summary>Horizontal layout (default)</summary>
    Horizontal,
    /// <summary>Vertical layout</summary>
    Vertical
}
