using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// Textarea component with DaisyUI styling and Blazor form integration.
/// Allows users to enter text in multiple lines.
/// </summary>
public partial class RlvTextArea : InputBase<string?>
{
    /// <summary>
    /// Placeholder text displayed when the textarea is empty.
    /// </summary>
    [Parameter]
    public string? Placeholder { get; set; }

    /// <summary>
    /// Number of visible text rows. Default is 4.
    /// </summary>
    [Parameter]
    public int Rows { get; set; } = 4;

    /// <summary>
    /// Color variant of the textarea.
    /// </summary>
    [Parameter]
    public TextAreaColor Color { get; set; } = TextAreaColor.None;

    /// <summary>
    /// Size of the textarea.
    /// </summary>
    [Parameter]
    public TextAreaSize Size { get; set; } = TextAreaSize.Medium;

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
                TextAreaColor.Neutral => "textarea-neutral",
                TextAreaColor.Primary => "textarea-primary",
                TextAreaColor.Secondary => "textarea-secondary",
                TextAreaColor.Accent => "textarea-accent",
                TextAreaColor.Info => "textarea-info",
                TextAreaColor.Success => "textarea-success",
                TextAreaColor.Warning => "textarea-warning",
                TextAreaColor.Error => "textarea-error",
                _ => ""
            });

            // Size
            classes.Add(Size switch
            {
                TextAreaSize.XSmall => "textarea-xs",
                TextAreaSize.Small => "textarea-sm",
                TextAreaSize.Medium => "textarea-md",
                TextAreaSize.Large => "textarea-lg",
                TextAreaSize.XLarge => "textarea-xl",
                _ => ""
            });

            // Ghost
            if (Ghost)
            {
                classes.Add("textarea-ghost");
            }

            return string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c)));
        }
    }

    /// <summary>
    /// Handles the textarea input event for real-time updates.
    /// </summary>
    private void HandleInput(ChangeEventArgs e)
    {
        CurrentValueAsString = e.Value?.ToString();
    }

    /// <inheritdoc />
    protected override bool TryParseValueFromString(string? value, out string? result, out string validationErrorMessage)
    {
        result = value;
        validationErrorMessage = string.Empty;
        return true;
    }
}

/// <summary>
/// Color variants for textarea components.
/// </summary>
public enum TextAreaColor
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
/// Size variants for textarea components.
/// </summary>
public enum TextAreaSize
{
    XSmall,
    Small,
    Medium,
    Large,
    XLarge
}
