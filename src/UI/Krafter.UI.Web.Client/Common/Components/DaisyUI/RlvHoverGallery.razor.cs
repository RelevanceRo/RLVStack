using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// DaisyUI Hover Gallery component - container of images where first image is visible by default
/// and hovering horizontally reveals other images. Supports up to 10 images.
/// Useful for product cards in ecommerce sites, portfolios, or image galleries.
/// </summary>
public partial class RlvHoverGallery
{
    /// <summary>
    /// Content for the gallery (typically multiple img elements)
    /// First image is visible by default, others show on horizontal hover
    /// </summary>
    [Parameter, EditorRequired] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional CSS classes to apply to the gallery container
    /// </summary>
    [Parameter] public string? CssClass { get; set; }

    /// <summary>
    /// Additional attributes to apply to the gallery container
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }
}
