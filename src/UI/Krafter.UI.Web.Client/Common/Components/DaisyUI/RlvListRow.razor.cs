using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// DaisyUI List Row component - horizontal grid layout for a single row in a list.
/// By default, the second child fills the remaining space.
/// Use 'list-col-grow' class on a child to make it fill remaining space instead.
/// Use 'list-col-wrap' class on a child to push it to the next line.
/// </summary>
public partial class RlvListRow
{
    /// <summary>
    /// Content for the list row (direct children can use list-col-grow or list-col-wrap classes)
    /// </summary>
    [Parameter, EditorRequired] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional CSS classes to apply to the list row
    /// </summary>
    [Parameter] public string? CssClass { get; set; }

    /// <summary>
    /// Additional attributes to apply to the list row
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }
}
