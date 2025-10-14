using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Steps component for showing a list of steps in a process.
/// Contains multiple RlvStepItem components.
/// </summary>
public partial class RlvSteps
{
    /// <summary>
    /// Child content - typically contains multiple RlvStepItem components or li elements with step class.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Orientation of the steps.
    /// </summary>
    [Parameter]
    public StepsOrientation Orientation { get; set; } = StepsOrientation.Horizontal;

    /// <summary>
    /// Additional CSS classes for the steps container.
    /// </summary>
    [Parameter]
    public string? StepsClass { get; set; }

    /// <summary>
    /// Additional attributes for the ul element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Builds the CSS class string for the steps.
    /// </summary>
    private string CssClass
    {
        get
        {
            var classes = new List<string>();

            // Orientation (Horizontal is default, no class needed)
            if (Orientation == StepsOrientation.Vertical)
            {
                classes.Add("steps-vertical");
            }
            // Horizontal is default, no class needed

            // Custom classes
            if (!string.IsNullOrEmpty(StepsClass))
            {
                classes.Add(StepsClass);
            }

            return string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c)));
        }
    }
}

/// <summary>
/// Orientation options for steps.
/// </summary>
public enum StepsOrientation
{
    Horizontal,
    Vertical
}
