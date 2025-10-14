using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// DaisyUI Countdown component - displays a number with flip animation transition (0-999)
/// </summary>
public partial class RlvCountdown
{
    /// <summary>
    /// The value to display (must be between 0 and 999)
    /// </summary>
    [Parameter] public int Value { get; set; }

    /// <summary>
    /// Minimum number of digits to display (2 or 3). Null uses default behavior.
    /// </summary>
    [Parameter] public int? Digits { get; set; }

    /// <summary>
    /// Additional CSS classes to apply
    /// </summary>
    [Parameter] public string? CssClass { get; set; }

    /// <summary>
    /// Additional attributes to apply to the countdown container
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string GetStyle()
    {
        var styles = new List<string> { $"--value:{Value}" };

        if (Digits.HasValue)
        {
            styles.Add($"--digits:{Digits.Value}");
        }

        return string.Join("; ", styles);
    }
}
