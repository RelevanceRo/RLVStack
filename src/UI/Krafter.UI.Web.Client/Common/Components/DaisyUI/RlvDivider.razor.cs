using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Divider component for separating content vertically or horizontally.
/// </summary>
public partial class RlvDivider
{
    /// <summary>
    /// Optional text to display in the divider.
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// Child content for more complex divider content.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Color variant of the divider.
    /// </summary>
    [Parameter]
    public DividerColor Color { get; set; } = DividerColor.None;

    /// <summary>
    /// Direction of the divider. Default is Vertical.
    /// </summary>
    [Parameter]
    public DividerDirection Direction { get; set; } = DividerDirection.Vertical;

    /// <summary>
    /// Text placement (start, center, end). Default is Center.
    /// </summary>
    [Parameter]
    public DividerPlacement Placement { get; set; } = DividerPlacement.Center;

    /// <summary>
    /// Additional attributes to apply to the divider element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Builds the CSS class string based on parameters.
    /// </summary>
    private string CssClass
    {
        get
        {
            var classes = new List<string>();

            // Color
            classes.Add(Color switch
            {
                DividerColor.Neutral => "divider-neutral",
                DividerColor.Primary => "divider-primary",
                DividerColor.Secondary => "divider-secondary",
                DividerColor.Accent => "divider-accent",
                DividerColor.Info => "divider-info",
                DividerColor.Success => "divider-success",
                DividerColor.Warning => "divider-warning",
                DividerColor.Error => "divider-error",
                _ => ""
            });

            // Direction
            if (Direction == DividerDirection.Horizontal)
            {
                classes.Add("divider-horizontal");
            }

            // Placement
            classes.Add(Placement switch
            {
                DividerPlacement.Start => "divider-start",
                DividerPlacement.End => "divider-end",
                _ => ""
            });

            return string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c)));
        }
    }
}

/// <summary>
/// Color variants for dividers.
/// </summary>
public enum DividerColor
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
/// Direction variants for dividers.
/// </summary>
public enum DividerDirection
{
    Vertical,
    Horizontal
}

/// <summary>
/// Placement variants for divider text.
/// </summary>
public enum DividerPlacement
{
    Start,
    Center,
    End
}
