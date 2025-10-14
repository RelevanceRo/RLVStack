using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Toggle switch component with DaisyUI styling and Blazor form integration.
/// A checkbox styled to look like a switch button.
/// </summary>
public partial class RlvToggle : InputBase<bool>
{
    /// <summary>
    /// Optional label text to display next to the toggle.
    /// </summary>
    [Parameter]
    public string? Label { get; set; }

    /// <summary>
    /// Color variant of the toggle.
    /// </summary>
    [Parameter]
    public ToggleColor Color { get; set; } = ToggleColor.None;

    /// <summary>
    /// Size of the toggle.
    /// </summary>
    [Parameter]
    public ToggleSize Size { get; set; } = ToggleSize.Medium;

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
    /// Builds the CSS class string based on color and size parameters.
    /// </summary>
    private string CssClass
    {
        get
        {
            var classes = new List<string>();

            // Color
            classes.Add(Color switch
            {
                ToggleColor.Primary => "toggle-primary",
                ToggleColor.Secondary => "toggle-secondary",
                ToggleColor.Accent => "toggle-accent",
                ToggleColor.Neutral => "toggle-neutral",
                ToggleColor.Info => "toggle-info",
                ToggleColor.Success => "toggle-success",
                ToggleColor.Warning => "toggle-warning",
                ToggleColor.Error => "toggle-error",
                _ => ""
            });

            // Size
            classes.Add(Size switch
            {
                ToggleSize.XSmall => "toggle-xs",
                ToggleSize.Small => "toggle-sm",
                ToggleSize.Medium => "toggle-md",
                ToggleSize.Large => "toggle-lg",
                ToggleSize.XLarge => "toggle-xl",
                _ => ""
            });

            return string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c)));
        }
    }

    /// <summary>
    /// Handles the toggle change event.
    /// </summary>
    private void HandleChange(ChangeEventArgs e)
    {
        CurrentValue = e.Value is bool value ? value : false;
    }

    /// <inheritdoc />
    protected override bool TryParseValueFromString(string? value, out bool result, out string validationErrorMessage)
    {
        if (bool.TryParse(value, out result))
        {
            validationErrorMessage = string.Empty;
            return true;
        }

        validationErrorMessage = $"The value '{value}' is not valid.";
        return false;
    }
}

/// <summary>
/// Color variants for toggle switches.
/// </summary>
public enum ToggleColor
{
    None,
    Primary,
    Secondary,
    Accent,
    Neutral,
    Info,
    Success,
    Warning,
    Error
}

/// <summary>
/// Size variants for toggle switches.
/// </summary>
public enum ToggleSize
{
    XSmall,
    Small,
    Medium,
    Large,
    XLarge
}
