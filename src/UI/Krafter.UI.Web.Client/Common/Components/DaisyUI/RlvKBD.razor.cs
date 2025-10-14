using Microsoft.AspNetCore.Components;

namespace Krafter.UI.Web.Client.Common.Components.DaisyUI;

/// <summary>
/// KBD (keyboard) component for displaying keyboard shortcuts and keys.
/// Uses the native kbd HTML element with DaisyUI styling.
/// </summary>
public partial class RlvKBD
{
    /// <summary>
    /// Simple text content for the keyboard key (e.g., "Ctrl", "K", "⌘").
    /// </summary>
    [Parameter]
    public string? Key { get; set; }

    /// <summary>
    /// Rich content for the keyboard key.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Size of the kbd element.
    /// </summary>
    [Parameter]
    public KBDSize Size { get; set; } = KBDSize.Medium;

    /// <summary>
    /// Additional attributes for the kbd element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Builds the CSS class string for the kbd element.
    /// </summary>
    private string CssClass
    {
        get
        {
            var classes = new List<string>();

            // Size
            classes.Add(Size switch
            {
                KBDSize.XSmall => "kbd-xs",
                KBDSize.Small => "kbd-sm",
                KBDSize.Medium => "kbd-md",
                KBDSize.Large => "kbd-lg",
                KBDSize.XLarge => "kbd-xl",
                _ => ""
            });

            return string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c)));
        }
    }
}

/// <summary>
/// Size variants for KBD elements.
/// </summary>
public enum KBDSize
{
    XSmall,
    Small,
    Medium,
    Large,
    XLarge
}
