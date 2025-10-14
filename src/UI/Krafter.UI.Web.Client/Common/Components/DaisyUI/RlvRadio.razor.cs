using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Radio button component with DaisyUI styling and Blazor form integration.
/// Each set of radio inputs should have unique name attributes to avoid conflicts.
/// </summary>
public partial class RlvRadio : InputBase<string>
{
    /// <summary>
    /// The name attribute for radio button grouping. Required for proper radio behavior.
    /// All radio buttons with the same name are in the same group (only one can be selected).
    /// </summary>
    [Parameter, EditorRequired]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The value this radio button represents when selected.
    /// </summary>
    [Parameter, EditorRequired]
    public string RadioValue { get; set; } = string.Empty;

    /// <summary>
    /// Optional label text to display next to the radio button.
    /// </summary>
    [Parameter]
    public string? Label { get; set; }

    /// <summary>
    /// Color variant of the radio button.
    /// </summary>
    [Parameter]
    public RadioColor Color { get; set; } = RadioColor.None;

    /// <summary>
    /// Size of the radio button.
    /// </summary>
    [Parameter]
    public RadioSize Size { get; set; } = RadioSize.Medium;

    /// <summary>
    /// Additional CSS classes for the label wrapper (when Label is provided).
    /// </summary>
    [Parameter]
    public string? LabelClass { get; set; }

    /// <summary>
    /// Additional CSS classes for the label text.
    /// </summary>
    [Parameter]
    public string? LabelTextClass { get; set; }

    /// <summary>
    /// Checks if this radio button should be marked as checked.
    /// </summary>
    private bool IsChecked => Value == RadioValue;

    /// <summary>
    /// Builds the CSS class string based on color and size parameters.
    /// </summary>
    private string CssClass
    {
        get
        {
            var classes = new List<string> { "radio" }; // Base class

            // Color
            classes.Add(Color switch
            {
                RadioColor.Neutral => "radio-neutral",
                RadioColor.Primary => "radio-primary",
                RadioColor.Secondary => "radio-secondary",
                RadioColor.Accent => "radio-accent",
                RadioColor.Info => "radio-info",
                RadioColor.Success => "radio-success",
                RadioColor.Warning => "radio-warning",
                RadioColor.Error => "radio-error",
                _ => ""
            });

            // Size
            classes.Add(Size switch
            {
                RadioSize.XSmall => "radio-xs",
                RadioSize.Small => "radio-sm",
                RadioSize.Medium => "radio-md",
                RadioSize.Large => "radio-lg",
                RadioSize.XLarge => "radio-xl",
                _ => ""
            });

            return string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c)));
        }
    }

    /// <summary>
    /// Handles the radio button change event.
    /// </summary>
    private void HandleChange(ChangeEventArgs e)
    {
        CurrentValueAsString = RadioValue;
    }

    /// <inheritdoc />
    protected override bool TryParseValueFromString(string? value, out string result, out string validationErrorMessage)
    {
        result = value ?? string.Empty;
        validationErrorMessage = string.Empty;
        return true;
    }
}

/// <summary>
/// Color variants for radio buttons.
/// </summary>
public enum RadioColor
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
/// Size variants for radio buttons.
/// </summary>
public enum RadioSize
{
    XSmall,
    Small,
    Medium,
    Large,
    XLarge
}
