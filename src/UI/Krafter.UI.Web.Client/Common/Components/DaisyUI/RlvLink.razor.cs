using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Link component that adds underline styling to links.
/// Can render as an anchor tag (with Href) or button (without Href).
/// </summary>
public partial class RlvLink
{
    /// <summary>
    /// Link URL. If provided, renders as an anchor tag. If not, renders as a button.
    /// </summary>
    [Parameter]
    public string? Href { get; set; }

    /// <summary>
    /// Simple text content for the link.
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// Rich content for the link (icons, text, etc.).
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Color variant of the link.
    /// </summary>
    [Parameter]
    public LinkColor Color { get; set; } = LinkColor.None;

    /// <summary>
    /// Whether to show underline only on hover.
    /// Default: false (underline always visible)
    /// </summary>
    [Parameter]
    public bool HoverOnly { get; set; }

    /// <summary>
    /// Click event handler (when rendered as button).
    /// </summary>
    [Parameter]
    public EventCallback OnClick { get; set; }

    /// <summary>
    /// Additional attributes for the link/button element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Builds the CSS class string for the link.
    /// </summary>
    private string CssClass
    {
        get
        {
            var classes = new List<string>();

            // Color
            classes.Add(Color switch
            {
                LinkColor.Neutral => "link-neutral",
                LinkColor.Primary => "link-primary",
                LinkColor.Secondary => "link-secondary",
                LinkColor.Accent => "link-accent",
                LinkColor.Success => "link-success",
                LinkColor.Info => "link-info",
                LinkColor.Warning => "link-warning",
                LinkColor.Error => "link-error",
                _ => ""
            });

            // Hover-only underline
            if (HoverOnly)
            {
                classes.Add("link-hover");
            }

            return string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c)));
        }
    }
}

/// <summary>
/// Color variants for links.
/// </summary>
public enum LinkColor
{
    None,
    Neutral,
    Primary,
    Secondary,
    Accent,
    Success,
    Info,
    Warning,
    Error
}
