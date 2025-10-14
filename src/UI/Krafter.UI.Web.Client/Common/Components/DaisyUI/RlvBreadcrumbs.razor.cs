using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Breadcrumbs component for displaying navigation hierarchy.
/// Wraps a list of RlvBreadcrumbItem components.
/// Automatically adds separators between items using CSS.
/// </summary>
public partial class RlvBreadcrumbs
{
    /// <summary>
    /// Child content - typically contains multiple RlvBreadcrumbItem components.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional CSS classes for the breadcrumbs container.
    /// Common: "text-sm", "text-xs", "max-w-xs", etc.
    /// </summary>
    [Parameter]
    public string? BreadcrumbsClass { get; set; }

    /// <summary>
    /// Additional attributes for the breadcrumbs div element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }
}
