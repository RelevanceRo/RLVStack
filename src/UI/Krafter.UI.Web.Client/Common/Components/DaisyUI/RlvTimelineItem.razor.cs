using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// DaisyUI Timeline Item component - single item in a timeline with start, middle, and end content
/// </summary>
public partial class RlvTimelineItem
{
    /// <summary>
    /// Content at the start position
    /// </summary>
    [Parameter] public RenderFragment? StartContent { get; set; }

    /// <summary>
    /// Content at the middle position (typically an icon or marker)
    /// </summary>
    [Parameter] public RenderFragment? MiddleContent { get; set; }

    /// <summary>
    /// Content at the end position
    /// </summary>
    [Parameter] public RenderFragment? EndContent { get; set; }

    /// <summary>
    /// Shows connector line (hr) before this item
    /// </summary>
    [Parameter] public bool ShowConnectorBefore { get; set; }

    /// <summary>
    /// Shows connector line (hr) after this item
    /// </summary>
    [Parameter] public bool ShowConnectorAfter { get; set; }

    /// <summary>
    /// Applies box style to start and end content
    /// </summary>
    [Parameter] public bool UseBox { get; set; }

    /// <summary>
    /// Additional attributes to apply to the timeline item
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }
}
