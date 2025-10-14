using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Badge component for displaying labels, statuses, and indicators.
/// Small, versatile component used to highlight information.
/// </summary>
public partial class RlvBadge
{
    /// <summary>
    /// Simple text content for the badge.
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// Rich content for the badge (icons, text, etc.).
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Color variant of the badge.
    /// </summary>
    [Parameter]
    public BadgeColor Color { get; set; } = BadgeColor.None;

    /// <summary>
    /// Style variant of the badge.
    /// </summary>
    [Parameter]
    public BadgeStyle Style { get; set; } = BadgeStyle.None;

    /// <summary>
    /// Size of the badge.
    /// </summary>
    [Parameter]
    public BadgeSize Size { get; set; } = BadgeSize.Medium;

    /// <summary>
    /// Additional attributes for the badge element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Builds the CSS class string for the badge.
    /// </summary>
    private string CssClass
    {
        get
        {
            var classes = new List<string>();

            // Color
            classes.Add(Color switch
            {
                BadgeColor.Neutral => "badge-neutral",
                BadgeColor.Primary => "badge-primary",
                BadgeColor.Secondary => "badge-secondary",
                BadgeColor.Accent => "badge-accent",
                BadgeColor.Info => "badge-info",
                BadgeColor.Success => "badge-success",
                BadgeColor.Warning => "badge-warning",
                BadgeColor.Error => "badge-error",
                _ => ""
            });

            // Style
            classes.Add(Style switch
            {
                BadgeStyle.Outline => "badge-outline",
                BadgeStyle.Dash => "badge-dash",
                BadgeStyle.Soft => "badge-soft",
                BadgeStyle.Ghost => "badge-ghost",
                _ => ""
            });

            // Size - Medium is default (no class needed)
            if (Size != BadgeSize.Medium)
            {
                var sizeClass = Size switch
                {
                    BadgeSize.XSmall => "badge-xs",
                    BadgeSize.Small => "badge-sm",
                    BadgeSize.Large => "badge-lg",
                    BadgeSize.XLarge => "badge-xl",
                    _ => ""
                };
                if (!string.IsNullOrEmpty(sizeClass))
                    classes.Add(sizeClass);
            }

            return string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c)));
        }
    }
}

/// <summary>
/// Color variants for badges.
/// </summary>
public enum BadgeColor
{
    None,
    Neutral,
    Primary,
    Secondary,
    Accent,
    Info,
    Success,
    Warning,
    Error
}

/// <summary>
/// Style variants for badges.
/// </summary>
public enum BadgeStyle
{
    None,
    Outline,
    Dash,
    Soft,
    Ghost
}

/// <summary>
/// Size variants for badges.
/// </summary>
public enum BadgeSize
{
    XSmall,
    Small,
    Medium,
    Large,
    XLarge
}
