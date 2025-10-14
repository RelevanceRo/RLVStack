using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Card component for grouping and displaying content.
/// Flexible container with optional figure (image), body, title, and actions sections.
/// </summary>
public partial class RlvCard
{
    /// <summary>
    /// Optional figure/image content (typically an img element).
    /// </summary>
    [Parameter]
    public RenderFragment? Figure { get; set; }

    /// <summary>
    /// Card title text (simple string).
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// Card title content (for rich content like badges). Takes precedence over Title if both are set.
    /// </summary>
    [Parameter]
    public RenderFragment? TitleContent { get; set; }

    /// <summary>
    /// Main content of the card body.
    /// </summary>
    [Parameter]
    public RenderFragment? Content { get; set; }

    /// <summary>
    /// Child content - provides full flexibility for custom card structure.
    /// When ShowCardBody is false, this is the only content rendered inside the card.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Actions section content (buttons, links, etc.).
    /// </summary>
    [Parameter]
    public RenderFragment? Actions { get; set; }

    /// <summary>
    /// Whether to show the default card-body wrapper.
    /// Set to false for complete custom content control.
    /// Default: true
    /// </summary>
    [Parameter]
    public bool ShowCardBody { get; set; } = true;

    /// <summary>
    /// Size of the card.
    /// </summary>
    [Parameter]
    public CardSize Size { get; set; } = CardSize.Medium;

    /// <summary>
    /// Style variant of the card.
    /// </summary>
    [Parameter]
    public CardStyle Style { get; set; } = CardStyle.None;

    /// <summary>
    /// Whether to display the card with image on the side (horizontal layout).
    /// Default: false
    /// </summary>
    [Parameter]
    public bool CardSide { get; set; }

    /// <summary>
    /// Whether to use the image as a full background overlay.
    /// Default: false
    /// </summary>
    [Parameter]
    public bool ImageFull { get; set; }

    /// <summary>
    /// Additional CSS classes for the card element.
    /// </summary>
    [Parameter]
    public string? CardClass { get; set; }

    /// <summary>
    /// Additional CSS classes for the figure element.
    /// </summary>
    [Parameter]
    public string? FigureClass { get; set; }

    /// <summary>
    /// Additional CSS classes for the card-body element.
    /// </summary>
    [Parameter]
    public string? CardBodyClass { get; set; }

    /// <summary>
    /// Additional CSS classes for the card-actions element.
    /// </summary>
    [Parameter]
    public string? ActionsClass { get; set; }

    /// <summary>
    /// Additional attributes for the card element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Builds the CSS class string for the card.
    /// </summary>
    private string CssClass
    {
        get
        {
            var classes = new List<string>();

            // Size (Medium is default, no class needed)
            classes.Add(Size switch
            {
                CardSize.XSmall => "card-xs",
                CardSize.Small => "card-sm",
                CardSize.Medium => "", // Default size, no class
                CardSize.Large => "card-lg",
                CardSize.XLarge => "card-xl",
                _ => ""
            });

            // Style
            classes.Add(Style switch
            {
                CardStyle.Border => "card-border",
                CardStyle.Dash => "card-dash",
                _ => ""
            });

            // Modifiers
            if (CardSide)
            {
                classes.Add("card-side");
            }

            if (ImageFull)
            {
                classes.Add("image-full");
            }

            // Custom classes
            if (!string.IsNullOrEmpty(CardClass))
            {
                classes.Add(CardClass);
            }

            return string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c)));
        }
    }
}

/// <summary>
/// Size variants for cards.
/// </summary>
public enum CardSize
{
    XSmall,
    Small,
    Medium,
    Large,
    XLarge
}

/// <summary>
/// Style variants for cards.
/// </summary>
public enum CardStyle
{
    None,
    Border,
    Dash
}
