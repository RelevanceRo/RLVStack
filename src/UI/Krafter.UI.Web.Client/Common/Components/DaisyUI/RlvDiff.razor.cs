using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// DaisyUI Diff component - shows a side-by-side comparison of two items with a draggable resizer
/// Browser support: Chrome 105+, Firefox 110+, Safari 16+
/// </summary>
public partial class RlvDiff
{
    /// <summary>
    /// Content for the first item (left side) - can be image, text, or any HTML content
    /// </summary>
    [Parameter, EditorRequired] public RenderFragment? Item1Content { get; set; }

    /// <summary>
    /// Content for the second item (right side) - can be image, text, or any HTML content
    /// </summary>
    [Parameter, EditorRequired] public RenderFragment? Item2Content { get; set; }

    /// <summary>
    /// Additional CSS classes to apply to the diff container
    /// </summary>
    [Parameter] public string? CssClass { get; set; }

    /// <summary>
    /// Additional attributes to apply to the diff container
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }
}
