using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Validator wrapper component that adds DaisyUI validation styling.
/// Changes the color of form elements to error or success based on input's validation rules.
///
/// Note: The child input element must have the "validator" class added for validation styling to work.
/// Use this component to wrap inputs and add hint text that appears when validation fails.
/// </summary>
public partial class RlvValidator
{
    /// <summary>
    /// The input element(s) to apply validation styling to.
    /// Child elements should include the "validator" class.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Hint text that appears below the input when validation fails.
    /// </summary>
    [Parameter]
    public string? Hint { get; set; }

    /// <summary>
    /// Additional CSS classes for the hint element.
    /// Use "hidden" to hide the hint when not visible (no space taken).
    /// By default, hint is invisible but still takes space to prevent layout shift.
    /// </summary>
    [Parameter]
    public string? HintClass { get; set; }

    /// <summary>
    /// Additional CSS classes for the container div.
    /// </summary>
    [Parameter]
    public string? ContainerClass { get; set; }
}
