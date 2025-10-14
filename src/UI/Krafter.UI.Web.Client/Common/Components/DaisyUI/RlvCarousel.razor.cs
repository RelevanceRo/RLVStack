using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// DaisyUI Carousel component - displays images or content in a scrollable area with snap scrolling
/// </summary>
public partial class RlvCarousel
{
    /// <summary>
    /// Content to display inside the carousel (typically RlvCarouselItem components)
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Snap position for carousel items
    /// </summary>
    [Parameter] public CarouselSnap Snap { get; set; } = CarouselSnap.Start;

    /// <summary>
    /// Direction of the carousel (horizontal or vertical)
    /// </summary>
    [Parameter] public CarouselDirection Direction { get; set; } = CarouselDirection.Horizontal;

    /// <summary>
    /// Additional attributes to apply to the carousel container
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string CssClass => string.Join(" ", new[] {
        GetSnapClass(),
        GetDirectionClass()
    }.Where(c => !string.IsNullOrEmpty(c)));

    private string GetSnapClass() => Snap switch
    {
        CarouselSnap.Start => "carousel-start",
        CarouselSnap.Center => "carousel-center",
        CarouselSnap.End => "carousel-end",
        _ => "carousel-start"
    };

    private string GetDirectionClass() => Direction switch
    {
        CarouselDirection.Horizontal => "",
        CarouselDirection.Vertical => "carousel-vertical",
        _ => ""
    };
}

/// <summary>
/// Snap position options for carousel
/// </summary>
public enum CarouselSnap
{
    /// <summary>Snap elements to start (default)</summary>
    Start,
    /// <summary>Snap elements to center</summary>
    Center,
    /// <summary>Snap elements to end</summary>
    End
}

/// <summary>
/// Direction options for carousel
/// </summary>
public enum CarouselDirection
{
    /// <summary>Horizontal layout (default)</summary>
    Horizontal,
    /// <summary>Vertical layout</summary>
    Vertical
}
