using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Globalization;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Range slider component with DaisyUI styling and Blazor form integration.
/// </summary>
public partial class RlvRange : InputBase<double>
{
    /// <summary>
    /// Minimum value of the range slider.
    /// </summary>
    [Parameter]
    public double Min { get; set; } = 0;

    /// <summary>
    /// Maximum value of the range slider.
    /// </summary>
    [Parameter]
    public double Max { get; set; } = 100;

    /// <summary>
    /// Step increment for the slider. Default is 1.
    /// </summary>
    [Parameter]
    public double Step { get; set; } = 1;

    /// <summary>
    /// Color variant of the range slider.
    /// </summary>
    [Parameter]
    public RangeColor Color { get; set; } = RangeColor.None;

    /// <summary>
    /// Size of the range slider.
    /// </summary>
    [Parameter]
    public RangeSize Size { get; set; } = RangeSize.Medium;

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
                RangeColor.Neutral => "range-neutral",
                RangeColor.Primary => "range-primary",
                RangeColor.Secondary => "range-secondary",
                RangeColor.Accent => "range-accent",
                RangeColor.Info => "range-info",
                RangeColor.Success => "range-success",
                RangeColor.Warning => "range-warning",
                RangeColor.Error => "range-error",
                _ => ""
            });

            // Size
            classes.Add(Size switch
            {
                RangeSize.XSmall => "range-xs",
                RangeSize.Small => "range-sm",
                RangeSize.Medium => "range-md",
                RangeSize.Large => "range-lg",
                RangeSize.XLarge => "range-xl",
                _ => ""
            });

            return string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c)));
        }
    }

    /// <summary>
    /// Handles the input event for real-time value updates.
    /// </summary>
    private void HandleInput(ChangeEventArgs e)
    {
        if (e.Value is string stringValue && double.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
        {
            CurrentValue = value;
        }
    }

    /// <inheritdoc />
    protected override bool TryParseValueFromString(string? value, out double result, out string validationErrorMessage)
    {
        if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
        {
            validationErrorMessage = string.Empty;
            return true;
        }

        validationErrorMessage = $"The value '{value}' is not a valid number.";
        return false;
    }
}

/// <summary>
/// Color variants for range sliders.
/// </summary>
public enum RangeColor
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
/// Size variants for range sliders.
/// </summary>
public enum RangeSize
{
    XSmall,
    Small,
    Medium,
    Large,
    XLarge
}
