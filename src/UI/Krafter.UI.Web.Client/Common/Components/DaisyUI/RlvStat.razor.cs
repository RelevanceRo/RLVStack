using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// DaisyUI Stat component - displays a single statistic with title, value, description, optional figure icon and actions
/// </summary>
public partial class RlvStat
{
    /// <summary>
    /// Title text (alternative to TitleContent)
    /// </summary>
    [Parameter] public string? Title { get; set; }

    /// <summary>
    /// Title content (alternative to Title string)
    /// </summary>
    [Parameter] public RenderFragment? TitleContent { get; set; }

    /// <summary>
    /// Value text (alternative to ValueContent)
    /// </summary>
    [Parameter] public string? Value { get; set; }

    /// <summary>
    /// Value content (alternative to Value string)
    /// </summary>
    [Parameter] public RenderFragment? ValueContent { get; set; }

    /// <summary>
    /// Description text (alternative to DescriptionContent)
    /// </summary>
    [Parameter] public string? Description { get; set; }

    /// <summary>
    /// Description content (alternative to Description string)
    /// </summary>
    [Parameter] public RenderFragment? DescriptionContent { get; set; }

    /// <summary>
    /// Figure content (icon, image, or avatar)
    /// </summary>
    [Parameter] public RenderFragment? FigureContent { get; set; }

    /// <summary>
    /// Actions content (buttons, controls)
    /// </summary>
    [Parameter] public RenderFragment? ActionsContent { get; set; }

    /// <summary>
    /// Additional CSS classes to apply
    /// </summary>
    [Parameter] public string? CssClass { get; set; }

    /// <summary>
    /// Additional attributes to apply to the stat container
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }
}
