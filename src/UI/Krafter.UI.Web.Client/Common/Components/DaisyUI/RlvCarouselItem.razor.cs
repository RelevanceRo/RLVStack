using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// DaisyUI Carousel Item component - individual item within a carousel
/// </summary>
public partial class RlvCarouselItem
{
    /// <summary>
    /// Content to display inside the carousel item
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// ID for the carousel item (used for anchor link navigation)
    /// </summary>
    [Parameter] public string? Id { get; set; }

    /// <summary>
    /// Additional CSS classes to apply
    /// </summary>
    [Parameter] public string? CssClass { get; set; }

    /// <summary>
    /// Additional attributes to apply to the carousel item
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }
}
