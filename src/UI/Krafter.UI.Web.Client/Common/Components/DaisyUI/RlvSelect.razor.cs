using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics.CodeAnalysis;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Select dropdown component with DaisyUI styling and Blazor form integration.
/// Supports binding to string, int, enum, and other value types.
/// </summary>
/// <typeparam name="TValue">The type of the value to bind to</typeparam>
public partial class RlvSelect<TValue> : InputBase<TValue>
{
    /// <summary>
    /// The child content containing option elements.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Color variant of the select dropdown.
    /// </summary>
    [Parameter]
    public SelectColor Color { get; set; } = SelectColor.None;

    /// <summary>
    /// Size of the select dropdown.
    /// </summary>
    [Parameter]
    public SelectSize Size { get; set; } = SelectSize.Medium;

    /// <summary>
    /// Ghost style (minimal background).
    /// </summary>
    [Parameter]
    public bool Ghost { get; set; }

    /// <summary>
    /// Builds the CSS class string based on color, size, and ghost parameters.
    /// </summary>
    private string CssClass
    {
        get
        {
            var classes = new List<string>();

            // Color
            classes.Add(Color switch
            {
                SelectColor.Neutral => "select-neutral",
                SelectColor.Primary => "select-primary",
                SelectColor.Secondary => "select-secondary",
                SelectColor.Accent => "select-accent",
                SelectColor.Info => "select-info",
                SelectColor.Success => "select-success",
                SelectColor.Warning => "select-warning",
                SelectColor.Error => "select-error",
                _ => ""
            });

            // Size
            classes.Add(Size switch
            {
                SelectSize.XSmall => "select-xs",
                SelectSize.Small => "select-sm",
                SelectSize.Medium => "select-md",
                SelectSize.Large => "select-lg",
                SelectSize.XLarge => "select-xl",
                _ => ""
            });

            // Ghost
            if (Ghost)
            {
                classes.Add("select-ghost");
            }

            return string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c)));
        }
    }

    /// <summary>
    /// Handles the select change event.
    /// </summary>
    private void HandleChange(ChangeEventArgs e)
    {
        CurrentValueAsString = e.Value?.ToString();
    }

    /// <inheritdoc />
    protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out TValue result, [NotNullWhen(false)] out string? validationErrorMessage)
    {
        // Use the base implementation's binding converter
        if (BindConverter.TryConvertTo<TValue>(value, System.Globalization.CultureInfo.CurrentCulture, out var parsedValue))
        {
            result = parsedValue;
            validationErrorMessage = null;
            return true;
        }
        else
        {
            result = default;
            validationErrorMessage = $"The value '{value}' is not valid.";
            return false;
        }
    }
}

/// <summary>
/// Color variants for select dropdowns.
/// </summary>
public enum SelectColor
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
/// Size variants for select dropdowns.
/// </summary>
public enum SelectSize
{
    XSmall,
    Small,
    Medium,
    Large,
    XLarge
}
