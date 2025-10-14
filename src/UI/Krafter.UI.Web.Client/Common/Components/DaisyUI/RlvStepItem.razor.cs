using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Individual step item for use within RlvSteps component.
/// </summary>
public partial class RlvStepItem
{
    /// <summary>
    /// Child content - the text or content to display in the step.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Custom icon content to display inside the step-icon span.
    /// </summary>
    [Parameter]
    public RenderFragment? Icon { get; set; }

    /// <summary>
    /// Custom content to display as the step indicator (data-content attribute).
    /// Can be any character like ?, !, ✓, ✕, ★, ●, etc.
    /// </summary>
    [Parameter]
    public string? DataContent { get; set; }

    /// <summary>
    /// Color variant for the step.
    /// </summary>
    [Parameter]
    public StepColor Color { get; set; } = StepColor.None;

    /// <summary>
    /// Additional CSS classes for the step.
    /// </summary>
    [Parameter]
    public string? StepClass { get; set; }

    /// <summary>
    /// Additional attributes for the li element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Builds the CSS class string for the step.
    /// </summary>
    private string CssClass
    {
        get
        {
            var classes = new List<string>();

            // Color
            classes.Add(Color switch
            {
                StepColor.Neutral => "step-neutral",
                StepColor.Primary => "step-primary",
                StepColor.Secondary => "step-secondary",
                StepColor.Accent => "step-accent",
                StepColor.Info => "step-info",
                StepColor.Success => "step-success",
                StepColor.Warning => "step-warning",
                StepColor.Error => "step-error",
                _ => ""
            });

            // Custom classes
            if (!string.IsNullOrEmpty(StepClass))
            {
                classes.Add(StepClass);
            }

            return string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c)));
        }
    }
}

/// <summary>
/// Color variants for step items.
/// </summary>
public enum StepColor
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
